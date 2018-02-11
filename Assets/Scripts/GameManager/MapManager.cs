using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
	public struct Coord
	{
		public int x, y;

		public Coord(int p1, int p2)
		{
			x = p1;
			y = p2;
		}
	}
	public static MapManager Instance;

	void Awake() {
		if (Instance != null) {
			Debug.LogError("There's more than one MapManager in the scene!!");
		}

		Instance = this;
	}

	private Node[,] nodes;
	public GameObject nodePrefab;

	// mapping format: width,height, on the first line followed by a string representing the map
	private string defaultMapping = "6,6," +
									"lsllll" +
									"lrllll" +
									"lrlrre" +
									"lrlrll" +
									"lrlrll" +
									"lrrrll";
	private int width, height;
	private const float tile_offset = 0.5f; // hardcoded for now with tile anchor centered
	private Dictionary<int, Coord> calculatedPathing;
	private Coord[] dir_options = new Coord[] {
		new Coord(0, 1),
		new Coord(0, -1),
		new Coord(-1, 0),
		new Coord(1, 0)
	};

	private const int encodeFactor = 100000;

	void Start()
	{
		LoadMap();
		calculatedPathing = new Dictionary<int, Coord>();
	}

	public Vector2 GetMapCenter()
	{
		return new Vector2((float)nodes.GetLength(1) / 2, (float)nodes.GetLength(0) / 2);
	}

	public int GetWidth()
	{
		return width;
	}

	public int GetHeight()
	{
		return height;
	}

	public Vector2 GetNextPosition(Vector2 curPosition)
	{
		Coord coord = PositionToCoord(curPosition);
		ShowMeDaWay(coord, new Dictionary<int, bool>());
		return CoordToPosition(calculatedPathing[EncodeCoord(coord)]);
	}

	private int EncodeCoord(Coord c)
	{
		return c.x + c.y * encodeFactor;
	}

	private Coord DecodeCoord(int encodedKey) {
		return new Coord(encodedKey % encodeFactor, encodedKey / encodeFactor); 
	}

	private Coord PositionToCoord(Vector2 position)
	{
		return new Coord((int)(position.x - tile_offset), (int)(position.y + tile_offset - height));
	}

	private Vector2 CoordToPosition(Coord c)
	{
		return new Vector2(tile_offset + c.x, height - tile_offset - c.y);
	}

	/* Recursive pathfinding
	 * High level concept: visit neighbours to find exit nodes, when exit node is found, return its coordinates
	 * when the caller receives the coordinates it saves a reference to it from itself and then passes itself backup and so on...
	 * The main idea is that given any visited node, it should know how to get to the closest exit.
	 * Base cases:
	 * 1. Exit node, returns itself
	 * 2. Non-exit node with no unvisited traversible nodes that lead to an exit, return null
	 * 3. Visited node, return null (exit node takes precidence over this, there's a simple example that shows why this is necessary: call this method on 2 nodes adjacent to an exit)
	 * Induction Step:
	 * A) Mark node visited
	 * B) Visit traversable nodes from current node
	 * C) Reduce the results from traversable nodes down to the path that leads to the exit with the shortest estimated movement cost (euclidean distance)
	 * D) Save the path segment (i.e. the coordinate) in the calculatedPathing dictionary
	 */
	private Coord? ShowMeDaWay(Coord coord, Dictionary<int, bool> visited)
	{
		if (nodes[coord.y, coord.x].isExit)
		{
			return coord;
		}
		// account for already calculated node
		int encodedKey = EncodeCoord(coord);
		if (visited[encodedKey])
		{
			return null;
		}

		visited[encodedKey] = true;

		float minDistance = float.PositiveInfinity;
		Coord? closestExit = null;
		float cost;

		Coord? exitLocation = null;
		Coord neighbourCoord;

		// really wish there's a reduce function
		foreach (Coord dir in dir_options)
		{
			neighbourCoord = new Coord(coord.x + dir.x, coord.y + dir.y);
			exitLocation = ShowMeDaWay(neighbourCoord, visited);

			if (exitLocation.HasValue)
			{
				cost = EstimateMovementCost(exitLocation.Value, coord);
				if (cost < minDistance)
				{
					minDistance = cost;
					calculatedPathing[encodedKey] = neighbourCoord;
					closestExit = exitLocation.Value;
				}
			}
		}

		if (closestExit.HasValue)
		{
			return closestExit;
		}
		else
		{
			return null;
		}
	}

	private float EstimateMovementCost(Coord a, Coord b)
	{
		return Mathf.Sqrt(Mathf.Pow((a.x - b.x), 2) + Mathf.Pow((a.y - b.y), 2));
	}

	void LoadMap() {
		char[] delimitters = new char[] { ',' };
		string[] mapSpec = defaultMapping.Split(delimitters);
		width = int.Parse(mapSpec[0]);
		height = int.Parse(mapSpec[1]);
		string stringmap = mapSpec[2];
		nodes = new Node[height,width];  // width corresponds the column index, not the row index

		int x, y;
		for(int i = 0; i < stringmap.Length; i++) {
			x = i % width; // x is dim 1 index which represents 'x axis' i.e. column index
			y = i / height; // y is dim 0 index which represents 'y axis' i.e. row index
			nodes[y,x] = Instantiate(nodePrefab, CoordToPosition(new Coord(x, y)), Quaternion.identity).GetComponent<Node>();
			LoadTile (stringmap [i], x, y);
		}
	}

	void LoadTile(char c, int x, int y) {
		Tile tile = null;

		switch(c) {
		case 'l':
			tile = Land.Instance;
			break;
		case 'r':
			tile = Road.Instance;
			break;
		case 's':
			tile = Land.Instance;
			nodes [y, x].Build(BuildManager.Instance.GetSelectedSpawner());
			break;
		case 'e':
			tile = Land.Instance;
			nodes [y, x].Build(BuildManager.Instance.GetExit());
			break;
		default:
			Debug.LogError("does not recognize tile type in map generator");
			break;
		}

		nodes[y, x].AssignTile (tile);
	}

	public void ShowNodeIndicators() {
		// Probably wanna change this into a delegate instead in the future
		for(int i = 0; i < nodes.GetLength(0); i++) {
			for(int j = 0; j < nodes.GetLength(1); j++) {
				nodes[i,j].ShowIndicator();
			}
		}
	}

	public void HideNodeIndicators() {
		// Probably wanna change this into a delegate instead in the future
		for(int i = 0; i < nodes.GetLength(0); i++) {
			for(int j = 0; j < nodes.GetLength(1); j++) {
				nodes[i,j].HideIndicator();
			}
		}
	}
}

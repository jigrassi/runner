using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	public static MapManager instance;

	void Awake() {
		if (instance != null) {
			Debug.LogError("There's more than one MapManager in the scene!!");
		}

		instance = this;
		LoadMap();
	}

	private Node[,] nodes;
	public GameObject nodePrefab;

	// mapping format: width,height, on the first line followed by a string representing the map
	private string defaultMapping = "6,6," +
									"llllll" +
									"llllll" +
									"llllll" +
									"llllll" +
									"llllll" +
									"llllll";
	private int width, height;

	void LoadMap() {
		char[] delimitters = new char[] { ',' };
		string[] mapSpec = defaultMapping.Split(delimitters);
		width = int.Parse(mapSpec[0]);
		height = int.Parse(mapSpec[1]);
		string stringmap = mapSpec[2];
		nodes = new Node[height,width];  // width corresponds the column index, not the row index

		float tile_offset = 0.5f; // hardcoded for now with tile anchor centered

		int x, y;
		for(int i = 0; i < stringmap.Length; i++) {
			x = i % width;
			y = i / height;
			nodes[y,x] = Instantiate(nodePrefab, new Vector2(tile_offset + x, tile_offset + y), Quaternion.identity).GetComponent<Node>();
		}
	}

	public Vector2 GetMapCenter() {
		return new Vector2((float)nodes.GetLength(1) / 2, (float)nodes.GetLength(0) / 2);
	}

	public int GetWidth() {
		return width;
	}

	public int GetHeight() {
		return height;
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

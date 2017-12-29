using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	private struct Node {
		public bool traversible;
	}

	private float width = 20f;
	private float height = 20f;
	private const float unitLength = 1f;
	// http://www.jgallant.com/nodal-pathfinding-in-unity-2d-with-a-in-non-grid-based-games/

	private Node[] nodes;

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere (new Vector3 (0, 0, 0), 0.2f);
	}

	void Start() {
		
	}
}

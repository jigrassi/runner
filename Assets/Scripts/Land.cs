using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITile {

	public static GameObject prefab;

	public bool Buildable() {
		return true;
	}

	public bool Traversible() {
		return false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, ITile {

	public bool Buildable() {
		return false;
	}

	public bool Traversible() {
		return true;
	}
}

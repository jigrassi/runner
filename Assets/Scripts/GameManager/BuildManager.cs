using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;
	private MapManager mm;

	void Awake() {
		if (instance != null) {
			Debug.LogError("There's more than one BuildManager in the scene!!");
		}

		instance = this;

		mm = GetComponent<MapManager>();

		if (!mm) {
			Debug.LogError("Could not find map manager on the GameManager");
		}
	}

	private bool buildEnabled;

	private GameObject selectedStructure;
	public GameObject defaultStructure;

	void Start() {
		buildEnabled = false;
		selectedStructure = defaultStructure;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.B)) {
			buildEnabled = !buildEnabled;

			if (buildEnabled) {
				mm.ShowNodeIndicators();
			} else {
				mm.HideNodeIndicators();
			}
		}
	}

	public GameObject GetSelectedStructure() {
		return selectedStructure;
	}
}

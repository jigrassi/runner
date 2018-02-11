using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager Instance;
	private MapManager mm;

	void Awake() {
		if (Instance != null) {
			Debug.LogError("There's more than one BuildManager in the scene!!");
		}

		Instance = this;

		mm = MapManager.Instance;

		if (!mm) {
			Debug.LogError("Could not find map manager on the GameManager");
		}
	}

	private bool buildEnabled;

	private GameObject selectedStructure;
	public GameObject defaultStructure;

	public GameObject defaultSpawner;
	public GameObject defaultExit;

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

	public bool GetBuildEnabled() {
		return buildEnabled;
	}

	public GameObject GetSelectedStructure() {
		return selectedStructure;
	}

	public GameObject GetSelectedSpawner() {
		return defaultSpawner;
	}

	public GameObject GetExit()
	{
		return defaultExit;
	}
}

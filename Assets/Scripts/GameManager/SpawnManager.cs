using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager Instance;

	public delegate void Spawn();
	public static event Spawn OnSpawn;

	public float enemyCount = 1f;
	public float spawnDelaySeconds = 0.2f;
	public GameObject runner;

	void Awake() {
		if (Instance != null) {
			Debug.LogError ("More than one SpawnManager in scene!");
			return;
		}
		Instance = this;
	}

	public void StartSpawn() {
		StartCoroutine (SpawnWave ());
	}

	private IEnumerator SpawnWave() {
		for (int i = 0; i < enemyCount; i++) {
			if (OnSpawn != null)
				OnSpawn();
			yield return new WaitForSeconds (spawnDelaySeconds);
		}
	}
}

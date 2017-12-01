using UnityEngine;
using System.Collections;

public class SpawnOnPress : MonoBehaviour {

	public static SpawnOnPress instance;

	public Transform runnerPrefab;
	public Transform spawnPoint;

	public float enemyCount = 5f;
	public float spawnDelaySeconds = 0.2f;

	void Awake() {
		if (instance != null) {
			Debug.LogError ("More than one SpawnOnPress in scene!");
			return;
		}
		instance = this;
	}

	public void spawn() {
		StartCoroutine (SpawnWave ());
	}

	IEnumerator SpawnWave() {
		for (int i = 0; i < enemyCount; i++) {
			Instantiate (runnerPrefab, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds (spawnDelaySeconds);
		}
	}
}

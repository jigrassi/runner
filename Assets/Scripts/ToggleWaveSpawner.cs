using UnityEngine;
using System.Collections;

public class ToggleWaveSpawner : MonoBehaviour {

	public Transform runnerPrefab;
	public Transform spawnPoint;

	// spawn attributes
	public float secondsBetweenWaves = 5f;
	public float spawnDelaySeconds = 0.5f;
	public float enemyCount = 5f;

	// toggleable spawn flag
	public static bool spawn = false;

	private float secondsUntilSpawn = 0;

	void Update() {
		if (!spawn)
			return;
		
		if (secondsUntilSpawn <= 0) {
			StartCoroutine (SpawnWave ());
			secondsUntilSpawn = secondsBetweenWaves;
		}
		secondsUntilSpawn -= Time.deltaTime;
	}

	IEnumerator SpawnWave() {
		for (int i = 0; i < enemyCount; i++) {
			Instantiate (runnerPrefab, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds (spawnDelaySeconds);
		}
	}

}

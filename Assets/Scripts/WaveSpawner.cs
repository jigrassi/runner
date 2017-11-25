using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

	public Transform runnerPrefab;
	public Transform spawnPoint;

	public static float countdown = 2f;
	private float secondsUntilSpawn = countdown;
	public float secondsBetweenWaves = 5f;

	public float spawnDelaySeconds = 0.5f;

	public float enemyCount = 5f;

	void Update() {
		if (secondsUntilSpawn <= 0f) {
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

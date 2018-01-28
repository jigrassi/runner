using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager instance;

	public delegate void Spawn();
	public static event Spawn OnSpawn;

	public float enemyCount = 1f;
	public float spawnDelaySeconds = 0.2f;

	void Awake() {
		if (instance != null) {
			Debug.LogError ("More than one SpawnManager in scene!");
			return;
		}
		instance = this;
	}

	public void StartSpawn() {
		StartCoroutine (SpawnWave ());
	}

	IEnumerator SpawnWave() {
		for (int i = 0; i < enemyCount; i++) {
			Spawn ();
			yield return new WaitForSeconds (spawnDelaySeconds);
		}
	}
}

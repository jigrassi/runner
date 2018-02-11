using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, IStructure {

	void Awake()
	{
		SpawnManager.OnSpawn += SpawnUnit;
	}

	void SpawnUnit()
	{
		Instantiate(SpawnManager.Instance.runner, transform);
	}

	public int GetCost()
	{
		return 0;
	}
}

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Collider2D))]
public class Runner : MonoBehaviour {

	public float baseSpeed = 1f;
	public float speedMultiplier = 1f;
	public float maxHealth = 10f;
	public float currentHealth = 0f;
	public Transform end;

	public Transform healthPrefab;
	private Transform healthDisplay;

	private Rigidbody2D rigidBody;

	void Awake() {
		currentHealth = maxHealth;

		healthDisplay = (Transform)Instantiate (healthPrefab, transform.position, transform.rotation);
		healthDisplay.transform.SetParent(transform);
		healthDisplay.GetComponent<HealthDisplay>().health_ratio = 1;

		// size and position the health bar
		RectTransform rt = healthDisplay.GetComponent<RectTransform>();
		rt.localScale = new Vector2(1, 1);
		rt.localPosition = new Vector2(0, 3);

		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		// always use velocity, translate does not animate well
		Vector2 dir = (Vector2)end.position - (Vector2)transform.position;
		rigidBody.MovePosition (rigidBody.position + dir.normalized * baseSpeed * speedMultiplier * Time.fixedDeltaTime);

		if (Vector2.Distance((Vector2)transform.position, (Vector2)end.position) < 0.3f) {
			Destroy (gameObject);
		}
	}

	void Hit(TowerAttributes.GetModifiers getModifiers) {
		float pendingSpeedMultiplier = 1f;
		foreach (TowerAttributes.Modifier mod in getModifiers()) {
			switch (mod.type) {
			case TowerAttributes.ModifierType.Damage:
				currentHealth -= mod.value;
				break;
			case TowerAttributes.ModifierType.Slow:
				pendingSpeedMultiplier *= mod.value;
				break;
			}
		}
		speedMultiplier = pendingSpeedMultiplier; // prevent compounding slow

		if (currentHealth <= 0) {
			Destroy (gameObject);
		}

		UpdateHealthDisplay();
	}

	void UpdateHealthDisplay() {
		healthDisplay.GetComponent<HealthDisplay>().health_ratio = currentHealth / maxHealth;
		healthDisplay.gameObject.SetActive (true);
	}
}

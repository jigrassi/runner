using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Runner : MonoBehaviour {

	public float speed = 5f;
	public float maxHealth = 10f;
	public float currentHealth = 0f;
	public Transform end;

	public Transform healthPrefab;
	private Transform healthDisplay;

	void Awake() {
		currentHealth = maxHealth;

		healthDisplay = (Transform)Instantiate (healthPrefab, transform.position, transform.rotation);
		healthDisplay.transform.SetParent(transform);
		healthDisplay.GetComponent<HealthDisplay>().health_ratio = 1;

		// size and position the health bar
		RectTransform rt = healthDisplay.GetComponent<RectTransform>();
		rt.localScale = new Vector2(1, 1);
		rt.localPosition = new Vector2(0, 3);
	}

	void Update() {
		Vector2 dir = (Vector2)end.position - (Vector2)transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector2.Distance((Vector2)transform.position, (Vector2)end.position) < 0.3f) {
			Destroy (gameObject);
		}
	}

	void Hit(List<TowerAttributes.ModifierType> modifiers) {
		TowerAttributes.Modifier m;
		foreach (TowerAttributes.ModifierType mod in modifiers) {
			m = TowerAttributes.Instance.GetModifier (mod);
			currentHealth -= m.addDamage;
			speed *= m.speedMultiplier;
		}

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

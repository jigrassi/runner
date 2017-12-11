using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Runner : MonoBehaviour {

	public float speed = 5f;
	public float max_health = 10f;
	public float current_health = 0f;
	public Transform end;

	public Transform health_prefab;
	private Transform health_display;

	void Awake() {
		current_health = max_health;

		health_display = (Transform)Instantiate (health_prefab, transform.position, transform.rotation);
		health_display.transform.SetParent(transform);
		health_display.GetComponent<HealthDisplay>().health_ratio = 1;

		// size and position the health bar
		RectTransform rt = health_display.GetComponent<RectTransform>();
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

	void AddDamage(float dmg) {
		current_health -= dmg;
		if (current_health <= 0) {
			Destroy (gameObject);
		}

		UpdateHealth();
	}

	void UpdateHealth() {
		health_display.GetComponent<HealthDisplay>().health_ratio = current_health / max_health;
	}
}

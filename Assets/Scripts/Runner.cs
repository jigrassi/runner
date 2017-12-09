using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Runner : MonoBehaviour {

	public float speed = 5f;
	public float health = 10f;
	public Transform end;

	public Transform canvas_prefab;
	private Transform health_display;

	void Awake() {
		health_display = (Transform)Instantiate (canvas_prefab, transform.position, transform.rotation);
		health_display.transform.SetParent(transform);
		health_display.GetComponent<Text> ().text = health.ToString ();
		RectTransform rt = health_display.GetComponent<RectTransform> ();
		rt.localPosition = Vector2.zero;
	}

	void Update() {
		DrawHealth ();
		Vector2 dir = (Vector2)end.position - (Vector2)transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector2.Distance((Vector2)transform.position, (Vector2)end.position) < 0.3f) {
			Destroy (gameObject);
		}
	}

	void DrawHealth() {
		
	}

	void AddDamage(float dmg) {
		health -= dmg;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}

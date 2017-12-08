using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {

	public float speed = 5f;
	public float health = 10f;
	public Transform end;

	void Update() {
		Vector2 dir = (Vector2)end.position - (Vector2)transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector2.Distance((Vector2)transform.position, (Vector2)end.position) < 0.3f) {
			Destroy (gameObject);
		}
	}

	void AddDamage(float dmg) {
		health -= dmg;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}

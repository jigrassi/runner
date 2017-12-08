using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 1f;
	public Vector2 target;

	private float COLLISION_RADIUS = 0.2f;

	void Start() {
		Vector2 dir = (Vector2)target - (Vector2)transform.position;
		Rigidbody2D rb = transform.GetComponent<Rigidbody2D> ();
		rb.velocity = dir * speed;
	}
		
	void FixedUpdate() {
		float dist = Vector2.Distance ((Vector2)target, (Vector2)transform.position);
		if (dist < COLLISION_RADIUS) {
			DestroyObject(gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class Projectile : MonoBehaviour {
	public float speed = 1f;
	public Vector2 target;
	public float effectRadius = 1f;
	public TowerAttributes.GetModifiers getModifiers;

	private const float CollisionRadius = 0.2f;
	public Collider2D[] hitCollider;
	public Transform explosionPrefab;

	void Start() {
		Vector2 dir = (Vector2)target - (Vector2)transform.position;
		Rigidbody2D rb = transform.GetComponent<Rigidbody2D> ();
		rb.velocity = dir * speed;
	}

	void FixedUpdate() {
		float dist = Vector2.Distance ((Vector2)target, (Vector2)transform.position);
		if (dist < CollisionRadius) {
			DestroyObject(gameObject);
			hitCollider = Physics2D.OverlapCircleAll (transform.position, effectRadius, 1 << LayerMask.NameToLayer("Live Units"));

			for (int i = 0; i < hitCollider.Length; i++) {
				hitCollider [i].SendMessage ("Hit", getModifiers);
			}
			Transform exp = Instantiate (explosionPrefab, transform.position, transform.rotation);
			exp.SendMessage("SetSize", effectRadius * 2);
		}
	}
}

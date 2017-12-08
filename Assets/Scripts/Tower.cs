using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public Transform projectile_prefab;
	public Transform target;

	public float attack_range = 3f;
	public float attack_speed = 1f;
	private float attack_delay = 1f;
	public Collider2D[] hitColliders;

	void Start() {
		attack_delay = 1f / attack_speed;
	}

	// draw attack range indicator
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, attack_range);
	}
	void FixedUpdate () {
		DetectEnemy ();
	}
		
	private void DetectEnemy() {
		if (target != null) {
			return;
		}

		hitColliders = Physics2D.OverlapCircleAll ((Vector2)transform.position, attack_range);

		if (hitColliders.Length > 0) {
			target = hitColliders [0].gameObject.transform;
			StartCoroutine (BeginFiring());
		}
	}

	private IEnumerator BeginFiring() {
		while (target != null) {
			FireProjectile ();
			yield return new WaitForSeconds(attack_delay);
			if (Vector2.Distance((Vector2)transform.position, (Vector2)target.position) > attack_range) {
				target = null;
			}
		}
				
	}

	private void FireProjectile() {
		Transform projectile = (Transform) Instantiate (projectile_prefab, transform.position, transform.rotation);
		Projectile proj_script = projectile.GetComponent<Projectile> ();
		proj_script.target = target.position;
	}
}

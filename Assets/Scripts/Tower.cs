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
		StartCoroutine (BeginFiring());
	}

	// draw attack range indicator
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, attack_range);
	}
	void Update () {
		DetectEnemy ();
	}
		
	private void DetectEnemy() {
		if (target != null) {
			return;
		}

		hitColliders = Physics2D.OverlapCircleAll ((Vector2)transform.position, attack_range);

		if (hitColliders.Length > 0) {
			target = hitColliders [0].gameObject.transform;

		}
	}

	private IEnumerator BeginFiring() {
		while (true) {
			if (target != null) {
				FireProjectile ();
				if (Vector2.Distance ((Vector2)transform.position, (Vector2)target.position) > attack_range) {
					target = null;
				}
			}
			yield return new WaitForSeconds(attack_delay);
		}
	}

	private void FireProjectile() {
		Transform projectile = (Transform) Instantiate (projectile_prefab, transform.position, transform.rotation);
		Projectile proj_script = projectile.GetComponent<Projectile> ();
		proj_script.target = target.position;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

	public Transform projectilePrefab;
	public List<TowerAttributes.ModifierType> modifiers; // special attributes that modify the tower's effects

	public float attackRange = 3f;
	public float attackSpeed = 1f;
	private float AttackDelay = 1f;

	private Transform target;
	private Collider2D[] hitColliders;


	void Start() {
		modifiers = new List<TowerAttributes.ModifierType>();
		modifiers.Add (TowerAttributes.ModifierType.Damage1);
		modifiers.Add (TowerAttributes.ModifierType.Slow1);
		AttackDelay = 1f / attackSpeed;
		StartCoroutine (BeginFiring());
	}

	// draw attack range indicator
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, attackRange);
	}

	void Update () {
		DetectEnemy ();
	}
		
	private void DetectEnemy() {
		if (target != null) {
			return;
		}

		hitColliders = Physics2D.OverlapCircleAll ((Vector2)transform.position, attackRange);

		if (hitColliders.Length > 0) {
			target = hitColliders [0].gameObject.transform;

		}
	}

	private IEnumerator BeginFiring() {
		while (true) {
			if (target != null) {
				FireProjectile ();
				if (Vector2.Distance ((Vector2)transform.position, (Vector2)target.position) > attackRange) {
					target = null;
				}
			}
			yield return new WaitForSeconds(AttackDelay);
		}
	}

	private void FireProjectile() {
		Transform projectile = (Transform) Instantiate (projectilePrefab, transform.position, transform.rotation);
		Projectile projScript = projectile.GetComponent<Projectile> ();
		projScript.target = target.position;
		projScript.modifiers = modifiers;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	private SpriteRenderer sr;

	void Start() {
		sr = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		Color c = sr.color;
		c.a -= 3 * Time.deltaTime;
		sr.color = c;

		if (sr.color.a <= 0) {
			Destroy (gameObject);
		}
	}

	public void SetSize(float size) {
		transform.localScale *= size;
	}
}

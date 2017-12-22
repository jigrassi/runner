using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	public float health_ratio = 0f;

	private RectTransform foreground = null;
	private const int BackgroundIndex = 0;
	private const int ForegroundIndex = 1;

	void Start() {
		gameObject.SetActive (false);
		foreground = transform.GetChild(ForegroundIndex).GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void LateUpdate () {
		foreground.localScale = new Vector2(health_ratio, 1);
	}
}

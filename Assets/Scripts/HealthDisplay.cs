using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	public float health_ratio = 0f;

	private RectTransform background = null;
	private RectTransform foreground = null;
	private int BACKGROUND_INDEX = 0;
	private int FOREGROUND_INDEX = 1;

	void Start() {
		background = transform.GetChild(BACKGROUND_INDEX).GetComponent<RectTransform>();
		foreground = transform.GetChild(FOREGROUND_INDEX).GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void LateUpdate () {
		foreground.localScale = new Vector2(health_ratio, 1);
	}
}

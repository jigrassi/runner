using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	private ITile tile;
	private IStructure structure;
	private SpriteRenderer rend;

	private const float hoverScaleRatio = 1.5f;
	private const float baseOpacity = 0.3f;

	void Start() {
		rend = GetComponent<SpriteRenderer>();
		HideIndicator();
	}

	void OnMouseDown() {
		if (structure != null) {
			Debug.Log("There's already a building there!");
			return;
		}

		if (!tile.Buildable()) {
			Debug.Log("Cannot build on that type of tile!");
			return;
		}


//		GameObject s = (GameObject)Instantiate(BuildManager.GetSelectedStructure(), transform, Quaternion.identity);
//		structure = s.GetComponent<IStructur
	}

	void OnMouseEnter() {
		HoverIndicator();
	}

	void OnMouseExit() {
		UnHoverIndicator();
	}

	public void HideIndicator() {
		SetIndicatorOpacity(0f);
	}

	public void ShowIndicator() {
		SetIndicatorOpacity(baseOpacity);
	}

	void HoverIndicator() {
		ScaleIndicatorOpacity(hoverScaleRatio);
	}

	void UnHoverIndicator() {
		if (rend.color.a != baseOpacity) {
			ScaleIndicatorOpacity(1 / hoverScaleRatio);
		}
	}

	void SetIndicatorOpacity(float opacity) {
		Color c = rend.color;
		c.a = opacity;
		rend.color = c;
	}

	void ScaleIndicatorOpacity(float percentage) {
		Color c = rend.color;
		c.a *= percentage;
		rend.color = c;
	}
}

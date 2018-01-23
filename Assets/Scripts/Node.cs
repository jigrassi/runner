using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	private Tile tile;
	private IStructure structure;
	private SpriteRenderer rend;

	private Color buildActiveColor = new Color (0, 255, 255);
	private Color buildHoverColor = new Color (255, 90, 209);
	private const float hoverScaleRatio = 1.5f;
	private const float baseOpacity = 0.3f;

	void Awake() {
		rend = GetComponent<SpriteRenderer>();
		HideIndicator();
	}
		
	void OnMouseDown() {
		if (!BuildManager.Instance.GetBuildEnabled()) {
			return;
		}

		if (structure != null) {
			Debug.Log("There's already a building there!");
			return;
		}

		if (!tile.buildable) {
			Debug.Log("Cannot build on that type of tile!");
			return;
		}

		GameObject prefab = BuildManager.Instance.GetSelectedStructure ();
		GameObject s = (GameObject)Instantiate (prefab, transform);
		structure = s.GetComponent<IStructure> ();

	}

	void OnMouseEnter() {
		if (!BuildManager.Instance.GetBuildEnabled()) {
			return;
		}
		HoverIndicator ();
	}

	void OnMouseExit() {
		if (!BuildManager.Instance.GetBuildEnabled()) {
			return;	
		}

		UnHoverIndicator ();
	}

	public void AssignTile(Tile t) {
		tile = t;

		rend.sprite = t.sprite;
	}

	public void HideIndicator() {
		rend.color = Color.white;
	}

	public void ShowIndicator() {
		rend.color = buildActiveColor;
	}

	void HoverIndicator() {
		rend.color = buildHoverColor;
	}

	void UnHoverIndicator() {
		rend.color = buildActiveColor;
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

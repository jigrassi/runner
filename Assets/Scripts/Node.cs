using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	private Tile tile;
	private IStructure structure;
	private SpriteRenderer rend;

	private static Color buildActiveColor = new Color (0, 255, 255);
	private static Color buildInactiveColor = new Color (165, 0, 0);
	private static Color buildHoverColor = new Color (255, 90, 209);
	private const float hoverScaleRatio = 1.5f;
	private const float baseOpacity = 0.3f;

	public bool isExit = false;

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
		HoverIndicator ();
	}

	void OnMouseExit() {
		UnHoverIndicator ();
	}

	public void AssignTile(Tile t) {
		tile = t;
		rend.sprite = t.sprite;
	}

	public void Build(GameObject building) {
		structure = Instantiate (building, transform).GetComponent<IStructure>();
	}

	public void HideIndicator() {
		rend.color = Color.white;
	}

	public void ShowIndicator() {
		if (!BuildManager.Instance.GetBuildEnabled ()) {
			return;
		}

		if (tile.buildable) {
			rend.color = buildActiveColor;
		} else {
			rend.color = buildInactiveColor;
		}
	}

	void HoverIndicator() {
		if (!BuildManager.Instance.GetBuildEnabled() || !tile.buildable) {
			return;
		}
		rend.color = buildHoverColor;
	}

	void UnHoverIndicator() {
		if (!BuildManager.Instance.GetBuildEnabled() || !tile.buildable) {
			return;	
		}
		rend.color = buildActiveColor;
	}
}

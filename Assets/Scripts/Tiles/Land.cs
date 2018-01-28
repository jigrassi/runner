using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : Tile {
	private static Land instance;

	private Land() {}

	public static Land Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new Land();
			}
			return instance;
		}
	}

	public override bool buildable {
		get {
			return true;
		}
	}

	public override bool traversible {
		get {
			return false;
		}
	}

	private const string sprite_name = "Land";

	public override Sprite sprite {
		get {
			return ResourceManager.instance.LoadSprite(sprite_name);
		}
	}
}

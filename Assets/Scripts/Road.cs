using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Tile {
	private static Road instance;

	private Road() {}

	public static Road Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new Road();
			}
			return instance;
		}
	}

	public override bool buildable {
		get {
			return false;
		}
	}

	public override bool traversible {
		get {
			return true;
		}
	}

	private const string sprite_name = "Road";

	public override Sprite sprite {
		get {
			return ResourceManager.instance.LoadSprite(sprite_name);
		}
	}
}

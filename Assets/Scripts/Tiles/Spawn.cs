using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : Tile {
	private static Spawn instance;

	private Spawn() {}

	public static Spawn Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new Spawn();
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
			return false;
		}
	}

	private const string sprite_name = "Spawn";

	public override Sprite sprite {
		get {
			return ResourceManager.instance.LoadSprite(sprite_name);
		}
	}
}

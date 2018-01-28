using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Tile {
	private static Exit instance;

	private Exit() {}

	public static Exit Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new Exit();
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

	private const string sprite_name = "Exit";

	public override Sprite sprite {
		get {
			return ResourceManager.instance.LoadSprite(sprite_name);
		}
	}
}

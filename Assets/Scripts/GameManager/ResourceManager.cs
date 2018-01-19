using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

	public static ResourceManager instance;

	private Dictionary<string, Sprite> sprite_cache = new Dictionary<string, Sprite>();

	void Awake () {
		if (instance != null) {
			Debug.LogError("More than one ResourceManager in scene");
			return;
		}
		instance = this;
	}

	private const string sprite_base_path = "Sprites/";

	public Sprite LoadSprite(string name) {
		if (!sprite_cache.ContainsKey(name)) {
			sprite_cache[name] = Resources.Load<Sprite>(sprite_base_path + name);
			if (sprite_cache == null) {
				Debug.LogErrorFormat("Could not find sprite: {0}!", name);
			}
		}

		return sprite_cache[name];
	}
}

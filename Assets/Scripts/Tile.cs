using UnityEngine;

public abstract class Tile {
	public abstract bool buildable { get; }

	public abstract bool traversible { get; }

	public abstract Sprite sprite { get; }
}

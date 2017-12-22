using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttributes : MonoBehaviour {
	public struct Modifier {
		public float addDamage;
		public float speedMultiplier;

		public Modifier(float addDamageP, float speedMultiplierP) {
			addDamage = addDamageP;
			speedMultiplier = speedMultiplierP;
		}
	}

	#region Singleton Class
	private static TowerAttributes instance;
	public static TowerAttributes Instance {
		get {
			if (instance == null) Debug.LogError("No Instance of Definitions in the Scene!");
			return instance;
		}
	}
	#endregion

	public Dictionary<ModifierType, Modifier> ht;

	public enum ModifierType { Slow1, Damage1 };

	// register modifiers
	void Awake() {
		#region Init Singleton
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError ("Another instance of TowerAttributes exists!!");
		}
		#endregion

		ht = new Dictionary<ModifierType, Modifier>();
		ht.Add (ModifierType.Slow1, new Modifier (0f, 0.7f));
		ht.Add (ModifierType.Damage1, new Modifier (4f, 1f));
	}

	public Modifier GetModifier(ModifierType name) {
		return ht [name];
	}
}

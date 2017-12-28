using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttributes : ScriptableObject {
	public enum ModifierType { Slow, Damage };
	public struct Modifier {
		public ModifierType type;
		public float value;

		public Modifier(ModifierType _type, float _value) {
			type = _type;
			value = _value;
		}
	}
	public delegate IList GetModifiers();
}

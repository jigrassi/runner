using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttributes {
	public enum ModifierType { Slow, Damage };
	public struct Modifier {
		public ModifierType type;
		public float value;

		public Modifier(ModifierType _type, float _value) {
			type = _type;
			value = _value;
		}
	}

	/* essentially used like a function pointer
	 * we want each projectile to hold a reference to the function that provides
	 * modifiers instead each projectile holding a copy of the list of modifiers
	 */
	public delegate IList GetModifiers();
}		

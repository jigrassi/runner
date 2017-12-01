using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListener : MonoBehaviour {
	void OnGUI() {
		Event e = Event.current;
		switch (e.keyCode) {
		case KeyCode.S:
			ToggleWaveSpawner.instance.spawn = !ToggleWaveSpawner.instance.spawn;
			break;
		default:
			var s = "{e.KeyCode} pressed";
			Debug.Log (s);
			break;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListener : MonoBehaviour {
	void OnGUI() {
		Event e = Event.current;
		if (!e.isKey || e.type != UnityEngine.EventType.KeyDown) {
			return;
		}
//		Debug.Log ("Type: " + e.type);
		switch (e.keyCode) {
		case KeyCode.S:
			SpawnOnPress.instance.spawn();
			break;
		default:
			Debug.Log (e.keyCode + " pressed");
			break;
		}
	}
}

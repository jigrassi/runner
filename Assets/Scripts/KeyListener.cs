using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListener : MonoBehaviour {
	void OnGUI() {
		Event e = Event.current;
		switch (e.keyCode) {
			case KeyCode.S:
				Debug.Log("S key pressed");
				
				if (ToggleWaveSpawner.spawn) {
					ToggleWaveSpawner.spawn = !ToggleWaveSpawner.spawn;
				}
				break;
		}
	}
}

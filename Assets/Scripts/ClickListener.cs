using UnityEngine;
using System.Collections;

public class ClickListener : MonoBehaviour {

	void OnGUI() {
		Event e = Event.current;

		if (!e.isMouse || e.type == UnityEngine.EventType.MouseDown) {
			return;
		}

		Debug.Log ("Mouse Click!");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
	//Assign this Camera in the Inspector
	private Camera cam;
	//These are the positions and dimensions of the Camera view in the Game view
	float m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight;

	void Start()
	{
		cam = GetComponent<Camera>();
		Vector2 mapCenter = MapManager.Instance.GetMapCenter();
		// the camera needs to be away from the 2d plane to interact with the plane
		transform.position = new Vector3(mapCenter.x, mapCenter.y, -1); 
		//This sets the Camera view rectangle to be in the bottom corner of the screen
		m_ViewPositionX = 0;
		m_ViewPositionY = 0;
		m_ViewWidth = 1f;
		m_ViewHeight = 1f;

		cam.enabled = true;

		if (cam)
		{
			//This enables the orthographic mode
			cam.orthographic = true;
			cam.orthographicSize = MapManager.Instance.GetHeight() / 2;
			//Set the orthographic Camera Viewport size and position
			cam.rect = new Rect(m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight);
		}
	}
}

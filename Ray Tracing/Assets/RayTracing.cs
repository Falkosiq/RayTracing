using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracing : MonoBehaviour
{
	private int screenWidth;
	private int screenHeight;

	[SerializeField]
	private bool realTime = false;

	[SerializeField]
	private float rayDistance = 1000;

	[SerializeField]
	private float resolution = 1;

	Camera myCamera;
	Texture2D outTex;

	void Start ()
	{
		myCamera = GetComponent<Camera> ();
		if (outTex) {
			Destroy (outTex);
		}

		screenWidth = Screen.width;
		screenHeight = Screen.height;

		outTex = new Texture2D ((int)(screenWidth * resolution), (int)(screenHeight * resolution));

		if (!realTime) {
			RayTrace ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (realTime) {
			RayTrace ();
		}
	}

	void OnGUI ()
	{
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), outTex);
	}

	void RayTrace ()
	{

		for (int y = 0; y < outTex.height; y++) {
			for (int x = 0; x < outTex.width; x++) {
				outTex.SetPixel (x, y, TracePixel (x, y));
			}
		}
		outTex.Apply ();

	}

	Color TracePixel (int x, int y)
	{
		Ray ray = myCamera.ScreenPointToRay (new Vector3 (x/resolution, y/resolution, 0));
		if (Physics.Raycast (ray, rayDistance)) {
			return Color.white;
		}
		return Color.black;
	}

}

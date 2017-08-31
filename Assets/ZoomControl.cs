using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomControl : MonoBehaviour
{
	public UnityEngine.UI.Text zoomText;
	public Camera zoomMe;
	public float zoomIncrement = 10f;

	private void UpdateText()
	{
		zoomText.text = "Zoom: " + zoomMe.fieldOfView;
	}

	public void UpZoom()
	{
		zoomMe.fieldOfView = zoomMe.fieldOfView + zoomIncrement;
		UpdateText ();
	}

	public void DownZoom()
	{
		zoomMe.fieldOfView = zoomMe.fieldOfView - zoomIncrement;
		UpdateText ();
	}

}

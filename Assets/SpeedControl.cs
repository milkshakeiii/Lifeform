using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedControl : MonoBehaviour
{

	public UnityEngine.UI.Text speedText;
	public float speedIncrement = 0.25f;

	private void UpdateText()
	{
		speedText.text = "Speed: " + Time.timeScale.ToString();
	}

	public void UpSpeed()
	{
		Time.timeScale = Time.timeScale + speedIncrement;
		UpdateText ();
	}

	public void DownSpeed()
	{
		Time.timeScale = Time.timeScale - speedIncrement;
		UpdateText ();
	}

}
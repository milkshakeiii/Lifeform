using UnityEngine;
using System.Collections;

public class Placer : MonoBehaviour 
{
	//round to the nearest multiple of roundingFactor when placing
	public float roundingFactor = 1f;

	private GameObject placeMe = null;

	private GameObject indicater;

	public void StartPlacement(GameObject newPlaceMe)
	{
		if (indicater != null)
			Destroy (indicater);
		placeMe = newPlaceMe;
		indicater = Instantiate (placeMe) as GameObject;
	}

	public void StopPlacement()
	{
		placeMe = null;
		if (indicater != null)
			Destroy (indicater);
		indicater = null;
	}

	private void PlacementClick()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x,
		            	Input.mousePosition.y,
		            	-Camera.main.transform.position.z));

		//round to the nearest socket according to roundingFactor
		Vector3 nearestRoundVector = new Vector3 (Round (mousePosition.x),
		                                          Round (mousePosition.y),
		                                          Round (mousePosition.z));

		//do not place if there is something there
		if (Physics2D.OverlapPointAll (nearestRoundVector).Length != 1)
			return;

		//make placement
		GameObject placedObject = Instantiate (placeMe,
		                                       nearestRoundVector,
		                                       new Quaternion (0f, 0f, 0f, 0f)) as GameObject;
	}

	private float Round(float roundMe)
	{
		return Mathf.Round (roundMe / roundingFactor) * roundingFactor;
	}

	private void UpdateIndicator()
	{
		if (placeMe != null)
		{
			if (indicater != null)
				indicater.transform.position = Camera.main.ScreenToWorldPoint(
					new Vector3(Input.mousePosition.x,
				            Input.mousePosition.y,
				            -Camera.main.transform.position.z));
		}
	}

	void Update ()
	{
		UpdateIndicator ();

		if (Input.GetKeyDown(KeyCode.Escape))
		    StopPlacement();

		if (Input.GetMouseButtonDown(0) && placeMe != null)
		{
			PlacementClick();
		}
	}
}
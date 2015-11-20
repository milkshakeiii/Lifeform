using UnityEngine;
using System.Collections;

public class PlacementSelector : MonoBehaviour 
{
	public GameObject selectMe;
	public Placer placer;

	public void MakeSelection()
	{
		placer.StartPlacement (selectMe);
	}
}
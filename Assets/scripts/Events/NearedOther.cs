using UnityEngine;
using System.Collections;

public class NearedOther : CreatureEvent
{

	private float nearness;

	public void InitializeNearedOther(float newNearness)
	{
		nearness = newNearness;
	}

	void Update()
	{
		if (Physics2D.OverlapCircleAll(gameObject.transform.position, nearness).Length > 1)
			Occured ();
	}
}

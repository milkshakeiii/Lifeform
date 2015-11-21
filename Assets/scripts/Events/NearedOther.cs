using UnityEngine;
using System.Collections;

public class NearedOther : CreatureEvent
{
	private float leftNearness;
	private float rightNearness;
	private float upNearness;
	private float downNearness;

	public void InitializeNearedOther(float newLeftNearness,
	                                  float newRightNearness,
	                                  float newUpNearness,
	                                  float newDownNearness)
	{
		if (newDownNearness == 0f)
			newDownNearness = 0.1f;
		if (newRightNearness == 0f)
			newRightNearness = 0.1f;
		if (newUpNearness == 0f)
			newUpNearness = 0.1f;
		if (newLeftNearness == 0f)
			newLeftNearness = 0.1f;

		leftNearness = newLeftNearness;
		rightNearness = newRightNearness;
		upNearness = newUpNearness;
		downNearness = newDownNearness;
	}

	void Update()
	{
		bool nearSomething = 
			Physics2D.OverlapAreaAll (
				new Vector2 (gameObject.transform.position.x - leftNearness, gameObject.transform.position.y + upNearness),
				new Vector2 (gameObject.transform.position.x + rightNearness, gameObject.transform.position.y - downNearness)
			).Length > 1;
			
		if (nearSomething)
		{
			Occured ();
		}
	}
}

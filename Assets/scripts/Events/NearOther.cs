using UnityEngine;
using System.Collections;
using System;

public class NearOther : CreatureEvent
{
	private float leftNearness;
	private float rightNearness;
	private float upNearness;
	private float downNearness;
    private float frequency;

    private float occurences = 0f;

	public void InitializeNearedOther(float newLeftNearness,
	                                  float newRightNearness,
	                                  float newUpNearness,
	                                  float newDownNearness,
                                      float newFrequency)
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

        frequency = newFrequency;
	}

	void Update()
	{
		bool nearSomething = 
			Physics2D.OverlapAreaAll (
				new Vector2 (gameObject.transform.position.x - leftNearness, gameObject.transform.position.y + upNearness),
				new Vector2 (gameObject.transform.position.x + rightNearness, gameObject.transform.position.y - downNearness)
			).Length > 1;

        float timePerTrigger = 1 / frequency;
        
		if (nearSomething)
		{
            occurences = occurences + Time.deltaTime / timePerTrigger;
		}

        while (occurences > 1)
        {
            occurences = occurences - 1;
            Occured();
        }
    }

    public override string GetEventName()
    {
        return "Near";
    }
}

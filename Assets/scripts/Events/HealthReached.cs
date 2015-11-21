using UnityEngine;
using System.Collections;

public class HealthReached : CreatureEvent
{
	private float threshold;
	private bool goingUp;

	private float previousHealth = 0.5f;

	public void InitializeHealthReached(float newThreshold,
	                                    bool newGoingUp)
	{
		threshold = newThreshold;
		goingUp = newGoingUp;
	}
	
	void Update()
	{
		float currentHealth = gameObject.GetComponent<Creature> ().GetHealth ();
		bool crossedThreshold = false;
		if (goingUp)
			crossedThreshold = (previousHealth < threshold && currentHealth >= threshold);
		else
			crossedThreshold = (previousHealth > threshold && currentHealth <= threshold);
		previousHealth = currentHealth;

		if (crossedThreshold)
			Occured ();
	}

    public override string GetEventName()
    {
        return "Health";
    }
}

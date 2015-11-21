using UnityEngine;
using System.Collections;

public class Reproduce : CreatureAction 
{
	private Vector3 offspringPosition;
	private float successCost;
	private float failureCost;
	
	public Reproduce(Vector2 newOffspringPosition,
	                 float newSuccessCost,
	                 float newFailureCost,
	                 Creature newMyCreature) : base(newMyCreature)
	{
		offspringPosition = new Vector3(newOffspringPosition.x, newOffspringPosition.y, 0f);
		successCost = newSuccessCost;
		failureCost = newFailureCost;
	}
	
	public override void Do()
	{
		if (GetCreature ().GetHealth () >= 1f)
		{
			GameObject offspring = Object.Instantiate (GetCreature ().gameObject,
                                                       GetCreature ().transform.position + offspringPosition,
			                                           GetCreature ().transform.rotation) as GameObject;

			GetCreature().ChangeHealth(-successCost);
		}
		else
		{
			GetCreature().ChangeHealth(-failureCost);
		}
	}
}
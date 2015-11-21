using UnityEngine;
using System.Collections;

public class Jump : CreatureAction 
{
	private Vector2 jumpVector;
	private float healthCost;

	public Jump(Vector2 newJumpVector,
	            float newHealthCost,
	            Creature newMyCreature) : base(newMyCreature)
	{
		jumpVector = newJumpVector;
		healthCost = newHealthCost;
	}

	public override void Do()
	{
		GetCreature ().gameObject.GetComponent<Rigidbody2D> ().AddForce (jumpVector);
		GetCreature ().ChangeHealth (-healthCost);
	}
}
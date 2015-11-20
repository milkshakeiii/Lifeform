using UnityEngine;
using System.Collections;

public class Jump : CreatureAction 
{
	private Vector2 jumpVector;

	public Jump(Vector2 newJumpVector, Creature myNewCreature) : base(myNewCreature)
	{
		jumpVector = newJumpVector;
	}

	public override void Do()
	{
		GetCreature ().gameObject.GetComponent<Rigidbody2D> ().AddForce (jumpVector);
	}
}
using UnityEngine;
using System.Collections;

public class Cooldown : CreatureCondition
{

	private float seconds;

	private float lastTrue = float.MinValue;

	public Cooldown(float cooldownSeconds, Creature myNewCreature) : base(myNewCreature)
	{
		seconds = cooldownSeconds;
	}
	
	public override bool Satisfied ()
	{
		if (lastTrue + seconds < Time.time)
		{
			lastTrue = Time.time;
			return true;
		}
		return false;
	}
	
}

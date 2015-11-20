using UnityEngine;
using System.Collections;

public abstract class CreatureAction
{
	private Creature myCreature;

	public CreatureAction(Creature creature)
	{
		myCreature = creature;
	}

	public Creature GetCreature()
	{
		return myCreature;
	}

	public abstract void Do();
}

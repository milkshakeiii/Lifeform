using UnityEngine;
using System.Collections;

public abstract class CreatureCondition
{
	private Creature myCreature;

	public CreatureCondition(Creature myNewCreature)
	{
		myCreature = myNewCreature;
	}

	public Creature GetCreature()
	{
		return myCreature;
	}

	public abstract bool Satisfied();

}
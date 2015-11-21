﻿using UnityEngine;
using System.Collections;

public class CreatureEvent : MonoBehaviour
{

	private CreatureCondition[] conditions;
	private CreatureAction[] actions;

	public void Initialize(CreatureCondition[] eventConditions, CreatureAction[] eventActions)
	{
		conditions = eventConditions;
		actions = eventActions;
	}

	public void Occured()
	{
		bool allConditionsTrue = true;

		if (conditions != null)
		{
			foreach (CreatureCondition condition in conditions)
			{
				if (!condition.Satisfied())
				{
					allConditionsTrue = false;
					break;
				}
			}
		}

		if (allConditionsTrue && actions != null)
		{
			foreach (CreatureAction action in actions)
			{
				action.Do();
			}
		}
	}
}
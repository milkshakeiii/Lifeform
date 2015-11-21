using UnityEngine;
using System.Collections;

public abstract class CreatureEvent : MonoBehaviour
{

	private int[] conditions;
	private int[] actions;

	public void Initialize(int[] eventConditions, int[] eventActions)
	{
		conditions = eventConditions;
		actions = eventActions;
	}

    public abstract string GetEventName();

    public Creature GetCreature()
    {
        return gameObject.GetComponent<Creature>();
    }

    public string GetDebugString()
    {
        string debugString = GetEventName() + ":";

        foreach(int index in conditions)
        {
            debugString += index.ToString() + ", ";
        }

        debugString += "-> ";

        foreach (int index in actions)
        {
            debugString += index.ToString() + ", ";
        }

        return debugString;
    }

    public void Rewire(int conditionCount, int actionCount)
    {
        bool rewireCondition = (Random.Range(0, 1f) > 0.5f);
        if (rewireCondition && conditions.Length > 0)
        {
            int rewiredCondition = Random.Range(0, conditions.Length - 1);
            conditions[rewiredCondition] = Random.Range(0, conditionCount - 1);
        }
        else
        {
            int rewiredAction = Random.Range(0, actions.Length - 1);
            actions[rewiredAction] = Random.Range(0, actionCount - 1);
        }

    }

	public void Occured()
	{
		bool allConditionsTrue = true;

		if (conditions != null)
		{
			foreach (int conditionIndex in conditions)
			{
                CreatureCondition condition = GetCreature().GetKnownCondition(conditionIndex);
				if (!condition.Satisfied())
				{
					allConditionsTrue = false;
					break;
				}
			}
		}

		if (allConditionsTrue && actions != null)
		{
			foreach (int actionIndex in actions)
			{
                CreatureAction action = GetCreature().GetKnownAction(actionIndex);
				action.Do();
			}
		}
	}
}
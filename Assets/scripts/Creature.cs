using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour 
{


	// Use this for initialization
	void Start ()
	{
		AddNearedOtherEvent(4.1f,
		                    new CreatureCondition[] {new NearLayer("Terrain", 4.1f, this),
						 						  	 new NearTag("green", 4.1f, this)},
							new CreatureAction[] {new Jump(new Vector2(0f, 10f), this)});
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void AddNearedOtherEvent(float nearness,
	                             CreatureCondition[] creatureConditions,
	                             CreatureAction[] creatureActions)
	{
		Debug.Log (gameObject.ToString () + " is getting a neared other event.");
		NearedOther newNearedOther = gameObject.AddComponent<NearedOther> ();
		newNearedOther.InitializeNearedOther (nearness);

		InitializeCreatureEvent (newNearedOther, creatureConditions, creatureActions);
	}

	private void InitializeCreatureEvent(CreatureEvent newEvent,
	                                     CreatureCondition[] creatureConditions,
	                                     CreatureAction[] creatureActions)
	{
		newEvent.Initialize (creatureConditions, creatureActions);
	}

}
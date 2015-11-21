using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour 
{
	//a creature's health ranges from 0 (dead) to 1 (perfectly healthy)
	//a creature can reproduce when its health reaches 1
	//after reproducing, both parent and child will have health 0.5
	private float health = 0.5f;

	public string GetName()
	{
		return this.gameObject.ToString ();
	}

	public float GetHealth()
	{
		return health;
	}

	public void ChangeHealth(float amount)
	{
		health = health + amount;
		if (health > 1)
			health = 1;
		if (health < 0)
			Die ();
	}

	private void Die()
	{
		Destroy (gameObject);
	}

	public float GetSize()
	{
		return (gameObject.transform.localScale.x +
				gameObject.transform.localScale.y +
				gameObject.transform.localScale.z) / 3f;
	}

















	public void AddHealthReachedEvent(float threshold,
	                                  bool goingUp,
	                                  CreatureCondition[] creatureConditions,
	                                  CreatureAction[] creatureActions)
	{
		HealthReached newHealthReached = gameObject.AddComponent<HealthReached> ();
		newHealthReached.InitializeHealthReached (threshold, goingUp);


		InitializeCreatureEvent (newHealthReached, creatureConditions, creatureActions);
	}

	public void AddNearedOtherEvent(float leftNearness,
	                                float rightNearness,
	                                float upNearness,
	                                float downNearness,
	                                CreatureCondition[] creatureConditions,
	                                CreatureAction[] creatureActions)
	{
		NearedOther newNearedOther = gameObject.AddComponent<NearedOther> ();
		newNearedOther.InitializeNearedOther (leftNearness, rightNearness, upNearness, downNearness);

		InitializeCreatureEvent (newNearedOther, creatureConditions, creatureActions);
	}

	private void InitializeCreatureEvent(CreatureEvent newEvent,
	                                     CreatureCondition[] creatureConditions,
	                                     CreatureAction[] creatureActions)
	{
		newEvent.Initialize (creatureConditions, creatureActions);
	}




















	// Use this for initialization
	void Start ()
	{
		AddNearedOtherEvent(4.1f, 0f, 0f, 0f,
			new CreatureCondition[] {new NearLayer("Terrain", 4.1f, this),
									 new NearTag("blue", 4.1f, this),
									 new Cooldown(2f, this)},
			new CreatureAction[] {new Jump(new Vector2(0f, 300f), 0.1f, this)});

		AddHealthReachedEvent (0.15f, false,
							   new CreatureCondition[0],
		                       new CreatureAction[] {new Jump (new Vector2 (0f, 1000f), 0.1f, this)});

		AddHealthReachedEvent (1f, true,
		                       new CreatureCondition[0],
		                       new CreatureAction[] {new Reproduce(new Vector2(0f, 12f),
			                                    	 0.5f,
			                                    	 0.1f,
			                                    	 this)});

		AddNearedOtherEvent (0f, 0f, 0f, 4.1f,
		                     new CreatureCondition[] {new NearTag ("green", 4.1f, this)},
							 new CreatureAction[] {new Graze (new Vector2 (0f, -4.1f),
			                              					  0.2f,
			                                				  0.1f,
			                                				  "green",
			                                				  this)});
	}
		
		// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnMouseOver()
	{
		CreatureInfoDisplay.GetSingletonInstance ().SetCreature (this);
	}
	
	
}
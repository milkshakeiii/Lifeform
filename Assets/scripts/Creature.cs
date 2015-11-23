using UnityEngine;
using System.Collections.Generic;

public class Creature : MonoBehaviour 
{
	//a creature's health ranges from 0 (dead) to 1 (perfectly healthy)
	private float health = 0.5f;

    //high mutation multiplier means crazier mutations
    private float mutationMultiplier = 1f;

    //a creature's behavior is dictated by its events
    private List<CreatureEvent> myEvents = new List<CreatureEvent>();

    private List<CreatureCondition> knownConditions = new List<CreatureCondition>();
    private List<CreatureAction> knownActions = new List<CreatureAction>();
	private int generation;

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

	public int GetGeneration()
	{
		return generation;
	}

    public CreatureAction GetKnownAction(int index)
    {
        return knownActions[index];
    }

    public CreatureCondition GetKnownCondition(int index)
    {
        return knownConditions[index];
    }

    public List<CreatureEvent> GetEvents()
    {
        return myEvents;
    }

    public void Mutate()
    {
        float rewireChance = Random.Range(0, 100);
        if (rewireChance > 10 * mutationMultiplier)
            Rewire();

        Tint();

    }

    private void Rewire()
    {
        if (myEvents.Count == 0)
            throw new UnityException("I cannot rewire because there are no events yet added.");

        int randomEventIndex = Random.Range(0, myEvents.Count - 1);
        myEvents[randomEventIndex].Rewire(knownConditions.Count, knownActions.Count);
    }

    private void Tint()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r + Random.Range(-0.1f, 0.1f),
                                         spriteRenderer.color.g + Random.Range(-0.1f, 0.1f),
                                         spriteRenderer.color.b + Random.Range(-0.1f, 0.1f));

    }














	public void AddHealthReachedEvent(float threshold,
	                                  bool goingUp,
	                                  int[] creatureConditions,
	                                  int[] creatureActions)
	{
		HealthReached newHealthReached = gameObject.AddComponent<HealthReached> ();
		newHealthReached.InitializeHealthReached (threshold, goingUp);


		InitializeCreatureEvent (newHealthReached, creatureConditions, creatureActions);
	}

	public void AddNearedOtherEvent(float leftNearness,
	                                float rightNearness,
	                                float upNearness,
	                                float downNearness,
	                                int[] creatureConditions,
	                                int[] creatureActions)
	{
		NearOther newNearedOther = gameObject.AddComponent<NearOther> ();
		newNearedOther.InitializeNearedOther (leftNearness, rightNearness, upNearness, downNearness, 120);

		InitializeCreatureEvent (newNearedOther, creatureConditions, creatureActions);
	}

	private void InitializeCreatureEvent(CreatureEvent newEvent,
	                                     int[] creatureConditions,
	                                     int[] creatureActions)
	{
        myEvents.Add(newEvent);
		newEvent.Initialize (creatureConditions, creatureActions);
	}

    public void AddKnownCondition(CreatureCondition knownCondition)
    {
        knownConditions.Add(knownCondition);

    }

    public void AddKnownAction(CreatureAction knownAction)
    {
        knownActions.Add(knownAction);

    }















	private void AddTestEvents()
	{
		AddKnownCondition(new NearLayer("Terrain", 4.1f, this));
		AddKnownCondition(new NearTag("blue", 4.1f, this));
		AddKnownCondition(new Cooldown(2f, this));
		AddKnownCondition(new NearTag("green", 4.1f, this));
		
		AddKnownAction(new Jump(new Vector2(0f, 300f), 0.1f, this));
		AddKnownAction(new Jump(new Vector2(0f, 1000f), 0.1f, this));
		AddKnownAction(new Reproduce(new Vector2(0f, 12f), 0.5f, 0.1f, this));
		AddKnownAction(new Graze(new Vector2(0f, -4.1f), 0.002f, 0.001f, "green", this));
		
		AddNearedOtherEvent(4.1f, 0f, 0f, 0f,
		                    new int[] {0, 1, 2},
		new int[] { 0 });
		
		AddHealthReachedEvent (0.15f, false,
		                       new int[0],
		                       new int[] {1});
		
		AddHealthReachedEvent (1f, true,
		                       new int[0],
		                       new int[] {2});
		
		AddNearedOtherEvent (0f, 0f, 0f, 4.1f,
		                     new int[] { 3 },
		new int[] { 3 });
	}

	private void Randomize()
	{

	}

    // Use this for initialization
    void Start ()
	{
		generation = generation + 1;
		if (generation == 1)
		{
			Randomize();
			AddTestEvents();
		}
		Debug.Log (generation);

        Mutate();
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
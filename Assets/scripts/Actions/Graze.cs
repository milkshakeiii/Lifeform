using UnityEngine;
using System.Collections;

public class Graze : CreatureAction 
{
	private Vector3 targetGrazePoint;
	private float healthGain;
	private float healthCost;
	private string terrainTag;
	
	public Graze(Vector2 newTargetGrazePoint,
	             float newHealthGain,
	             float newHealthCost,
	             string newTerrainTag,
	             Creature newMyCreature) : base(newMyCreature)
	{
		targetGrazePoint = new Vector3 (newTargetGrazePoint.x, newTargetGrazePoint.y, 0f);;
		healthGain = newHealthGain;
		healthCost = newHealthCost;
		terrainTag = newTerrainTag;
	}
	
	public override void Do()
	{
		Collider2D[] objectsAtGrazePoint = Physics2D.OverlapPointAll (GetCreature ().gameObject.transform.position + targetGrazePoint);
		bool success = false;
		foreach(Collider2D collider in objectsAtGrazePoint)
		{
			if (collider.tag == terrainTag)
				success = true;
		}

		float healthChange = 0f;
		if (success)
		{
			healthChange += (healthGain);
		}

		healthChange += (-healthCost);
		GetCreature ().ChangeHealth (healthChange);
	}
}
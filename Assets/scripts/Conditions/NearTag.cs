using UnityEngine;
using System.Collections;

public class NearTag : CreatureCondition
{
	
	private string tag;
	private float nearness;
	
	public NearTag(string tagName, float newNearness, Creature myNewCreature) : base(myNewCreature)
	{
		tag = tagName; 
		nearness = newNearness;
	}
	
	public override bool Satisfied ()
	{
		foreach(Collider2D nearObject in Physics2D.OverlapCircleAll(GetCreature().transform.position, nearness))
		{
			if (nearObject.gameObject.tag == tag)
				return true;
		}
		return false;
	}
	
}

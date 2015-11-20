using UnityEngine;
using System.Collections;

public class NearLayer : CreatureCondition
{

	private int layer;
	private float nearness;

	public NearLayer(string layerName, float newNearness, Creature myNewCreature) : base(myNewCreature)
	{
		layer = LayerMask.NameToLayer (layerName);
		nearness = newNearness;
	}

	public override bool Satisfied ()
	{
		foreach(Collider2D nearObject in Physics2D.OverlapCircleAll(GetCreature().transform.position, nearness))
		{
			if (nearObject.gameObject.layer == layer)
				return true;
		}
		return false;
	}

}

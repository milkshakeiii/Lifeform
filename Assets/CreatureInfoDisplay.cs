using UnityEngine;
using System.Collections;

public class CreatureInfoDisplay : MonoBehaviour 
{

	public GameObject backpanel;
	public GameObject healthBar;
	public UnityEngine.UI.Text creatureName;

	private static CreatureInfoDisplay singletonInstance;
	private Creature currentCreature;

	public static CreatureInfoDisplay GetSingletonInstance ()
	{
		if (singletonInstance == null)
			singletonInstance = FindObjectOfType<CreatureInfoDisplay> ();

		return singletonInstance;
	}


	public void SetCreature(Creature creature)
	{
		currentCreature = creature;
	}

	public void TurnOn()
	{
		backpanel.SetActive (true);
	}

	public void TurnOff()
	{
		backpanel.SetActive (false);
	}









	void Update()
	{
		if (currentCreature == null)
		{
			TurnOff();
			return;
		}
		else
		{
			TurnOn();
			creatureName.text = currentCreature.GetName ();
			healthBar.transform.localScale = new Vector3(currentCreature.GetHealth (), 1f, 1f);
		}
	}
}

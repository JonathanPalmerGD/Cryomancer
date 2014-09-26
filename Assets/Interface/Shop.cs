using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

	public GameObject player;
	public bool visible;
	public Rect shopRect = new Rect(350, 50, 150, 450);
	public PlayerStats stats;
	public Cryomancer runner;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		stats = player.GetComponent<PlayerStats>();
		runner = player.GetComponent<Cryomancer>();
	}
	void OnGUI()
	{
		if (visible)
		{
			GUI.Box(shopRect, "Welcome to the shop");
			GUI.Label(new Rect(360, 260, 130, 30), "Money: " + stats.money);
			Rect hpRect = new Rect(360, 80, 130, 30);
			if (GUI.Button(hpRect, "Health: " + stats.health))
			{
				if (stats.money >= 10)
				{
					stats.healPlayer(5); //stats.health += 5;
					stats.money -= 10;
				}
			}
			if (GUI.Button(new Rect(360, 115, 130, 30), "Max Health: " + stats.maxHealth))
			{
				if (stats.money >= 100)
				{
					stats.maxHealth += 10;
					stats.healPlayer(10);
					stats.money -= 100;

				}
			}
			if (GUI.Button(new Rect(360, 150, 130, 30), "Infinite Ice: " + runner.resourceBased))
			{
				if (stats.money >= 1000)
				{
					runner.resourceBased = false;
					stats.money -= 1000;
				}
			}

			//For the number items the player has (that are sellable)
				//Display them + the price the shopkeeper would buy them at.
				//If the player clicks that button
					//Add the item to the shopkeepers inventory with its price
					//Remove the item from the player, give them the gold for it.

		}
	}
	// Update is called once per frame
	void Update () 
	{
		float distanceBetween = Vector3.Distance(player.transform.position, transform.position);
		if (transform.localScale.x / 2 > distanceBetween)
		{
			visible = true;
		}
		else
		{
			visible = false;
		}
	}
}

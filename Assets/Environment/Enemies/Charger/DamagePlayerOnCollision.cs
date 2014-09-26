using UnityEngine;
using System.Collections;

public class DamagePlayerOnCollision : MonoBehaviour {
	public int damage = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.tag == "Player")
		{
			PlayerStats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
			stats.health = stats.health - damage;
		}
	}
}

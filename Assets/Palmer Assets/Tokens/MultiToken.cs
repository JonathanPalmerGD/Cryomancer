using UnityEngine;
using System.Collections;

public class MultiToken : MonoBehaviour 
{
	//Bunch of references to stats and such. We could trim this down honestly.
	private GameObject player;
	private GameObject boss;
	private BossStats bStats;
	private PlayerStats pStats;
	private Cryomancer runner;

	//What the token does. This class is more complex, but it's easier than having it inherit into anywhere from 3-10 different tokens. May get broken up later
	public bool damageBoss = true;
	public bool healPlayer = true;
	public bool restoreIce = true;
	public bool increaseMaxIce = true;
	
	//Some audio info
	public bool playOnPickup = true;
	public AudioClip acquireClip;

	//Collision and variable info
	public float collisionRadius = 3.0f;
	public int damage = 5;
	public int heal = 10;
	public int iceGain = 10;
	public int maxIceGain = 5;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		boss = GameObject.FindGameObjectWithTag("Boss");
		if (boss != null)
		{
			bStats = boss.GetComponent<BossStats>();
		}
		if (player != null)
		{
			pStats = player.GetComponent<PlayerStats>();
			runner = player.GetComponent<Cryomancer>();
		}
	}
	
	// This should become volume triggers instead of sphere point
	void Update () 
	{
		float distanceBetween = Vector3.Distance(player.transform.position, transform.position);
		if (collisionRadius > distanceBetween)
		{
			//All the things we can do. Do them if we're supposed to.
			if(bStats != null && damageBoss)
			{
				bStats.DamageBoss(damage, 0.25f, true);
			}
			if (healPlayer)
			{
				pStats.healPlayer(heal);
			}
			if (restoreIce)
			{
				runner.restoreIce(iceGain);
			}
			if (increaseMaxIce)
			{
				runner.maxIce += maxIceGain;
			}
			if (playOnPickup)
			{

				player.audio.clip = acquireClip;
				player.audio.Play();
			}
			if (light != null)
			{
				light.enabled = false;
			}
			enabled = false;
			renderer.enabled = false;
			particleSystem.enableEmission = false;
		}
	}
}

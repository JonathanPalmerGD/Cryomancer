using UnityEngine;
using System.Collections;

public class EvilToken : MonoBehaviour 
{
	private GameObject player;
	private Cryomancer runner;

	public AudioClip hurtAudio;

	public int difficulty = 1;
	public float[] damage = { 5, 25, 30 };
	public bool meltIce = false;
	public float[] iceLoss = { .2f, .4f, 1 };
	public float collisionRadius = 3.5f;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if (meltIce)
		{
			runner = player.GetComponent<Cryomancer>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		float distanceBetween = Vector3.Distance(player.transform.position, transform.position);

		if (collisionRadius > distanceBetween)
		{
			PlayerStats stats = player.GetComponent<PlayerStats>();
			stats.health = stats.health - damage[difficulty];

			if (meltIce && runner != null)
			{
				if (runner.ice > iceLoss[difficulty])
				{
					runner.ice -= iceLoss[difficulty];
				}
				else
				{
					runner.ice = 0;
				}

				runner.refreshTime = 0;
			}


			if (player.GetComponent<ScreenFlash>() != null)
			{
				player.GetComponent<ScreenFlash>().FlashScreen(hurtAudio);
			}
			gameObject.SetActive(false);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Burning : MonoBehaviour
{
	#region References to relevant things
	private GameObject player;
	private Cryomancer runner;
	private PlayerStats stats;
	#endregion
	
	public GameObject fire;
	public GameObject burnIndicator;
	public AudioClip burnAudio;

	#region Burn and Damage Info
	public float counter = 0.0f;
	public float burnEvery = 0.75f;
	public float burnEngageDelay = 2.5f;
	public int difficulty = 1;
	public float[] damage = { 2, 5, 10 };
	public float[] iceLoss = { 1, 3, 10 };
	#endregion

	public float collisionRadius = 15.0f;
	public bool onFire = false;
	public bool useIndicator = true;

	public GameStats gameStats;

	// Use this for initialization
	void Start () 
	{
		//Find out info about the world
		gameStats = GameObject.FindGameObjectWithTag("Properties").GetComponent<GameStats>();
		difficulty = gameStats.difficulty;
		player = GameObject.FindGameObjectWithTag("Player");

		//If we have a display to show where the fire will be.
		if (useIndicator)
		{
			burnIndicator = transform.FindChild("BurnIndicator").gameObject;
		}
		runner = player.GetComponent<Cryomancer>();
		stats = player.GetComponent<PlayerStats>();
		fire.SetActive(true);
	}

	#region Fire Toggle Functions
	/// <summary>
	/// For turning the fire off.
	/// </summary>
	public void Disable()
	{
		fire.SetActive(false);
		if (useIndicator)
		{
			burnIndicator.SetActive(false);
		}
		onFire = false;
	}

	/// <summary>
	/// For turning the fire on. We turn on indicator and count up before turning on the flames. Gives the player warning.
	/// </summary>
	public void Enable()
	{
		onFire = true;
		counter = 0;

		//If we don't use an indicator, go straight to fire.
		if (!useIndicator)
		{
			fire.SetActive(true);
		}
		else
		{
			//turn on indicator
			burnIndicator.SetActive(true);
		}
	}
	#endregion

	// Update is called once per frame
	void Update () 
	{
		counter += Time.deltaTime;
		//If we haven't turned on the fire
		if (onFire && useIndicator && !fire.activeInHierarchy)
		{
			//And we have passed enough time
			if(counter > burnEngageDelay)
			{
				counter = 0;
				//Burn baby burn
				fire.SetActive(true);
			}
		}
		if (onFire && fire.activeInHierarchy)
		{
			if (counter >= burnEvery)
			{
				if (player.transform.position.y + 2.0f > transform.position.y)
				{
					//This could become a trigger volume, but it's fine as a sphere point check.
					float distanceBetween = Vector3.Distance(player.transform.position, transform.position);

					if (collisionRadius > distanceBetween)
					{
						stats.health = stats.health - damage[difficulty];
						if (runner.ice > iceLoss[difficulty])
						{
							runner.ice -= iceLoss[difficulty];
						}
						else
						{
							runner.ice = 0;
						}

						runner.refreshTime = 0;
						counter = 0.0f;

						//Show the player that they're hurt.
						if (player.GetComponent<ScreenFlash>() != null)
						{
							player.GetComponent<ScreenFlash>().FlashScreen(burnAudio);
						}
					}
				}
			}
		}
		else
		{
			//Turn off fire if it shouldn't be on.
			fire.SetActive(false);
		}
	}
}

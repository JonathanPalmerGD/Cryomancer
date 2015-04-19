using UnityEngine;
using System.Collections;

public class BossStats : MonoBehaviour 
{
	public enum BossPhase { Primary, Transition, Secondary, Morph, Final, Dying };
	public BossPhase phase = BossPhase.Primary;

	//The token spawners
	public GameObject tokenGroup;

	public GameObject chargerSpawner;
	public GameObject projectileSpawner;

	//The shields to toggle. Unused
	public GameObject shield;

	//Lights to tweak
	public Light[] worldDirLights;

	//Change the environment
	public GameObject[] telePlanes;

	//Turn on the victory sphere
	public GameObject victorySphere;

	//Which teleplane is in use
	public int activeTelePlane = 0;

	//Environment Control
	public Igniter platformIgniter;

	#region Audio and Transitions
	public GameObject deathExplosion;
	public GameObject deathDialogue;
	public AudioClip lastWords;
	public AudioClip abyssBeckons;
	public AudioClip newAge;
	#endregion

	public bool visible = true;

	#region Weapon Info
	//The charger spawner's actual spawner
	private Spawner spawner;
	private FireAtPlayer gunSpark;
	#endregion
	#region State Information
	public float stateCounter = 0.0f;
	public float transDuration = 3.0f;
	public float morphDuration = 5.0f;
	public float dyingDuration = 7.0f;
	#endregion

	#region Health Variables
	public float shieldHealth = 25;
	public int health = 150;

	//Our damage flicker counter
	public float flickerCounter = 0.0f;
	#endregion

	public Color[] atmosphereLights = { Color.white, new Color(.8f, .4f, .2f, 10f), new Color(.5f, .1f, .5f) };
	public float lightLevel = .05f;

	// When the boss scene starts
	void Start()
	{
		ChangeTokens(false);
		//Get our spawner and spark gun
		spawner = chargerSpawner.GetComponent<Spawner>();
		gunSpark = projectileSpawner.GetComponent<FireAtPlayer>();
		
		//Make sure things are in their proper start state
		platformIgniter.enabled = false;

		spawner.enabled = false;
		shield.SetActive(false);
		victorySphere.SetActive(false);

		//Set the lights
		ChangeAtmoLight(atmosphereLights[0], .001f, .1f);

		//Pick our gun.
		gunSpark.PickNextGunType();
		
		//Get the right plane active.
		if (telePlanes != null && telePlanes.Length > 0)
		{
			telePlanes[activeTelePlane].SetActive(true);
		}
	}

	void OnGUI()
	{
		//Debug controls
		if (visible)
		{
			Rect boxInfo = new Rect(Screen.width / 2 - 250, 10, 500, 60);
			string hpString = "";
			//Detect the phase and which GUI we need to display
			if (phase != BossPhase.Secondary && phase != BossPhase.Transition)
			{
				hpString = "Dark Tyrant\n[";
				for (int i = 0; i < (int)health / 2; i++)
				{
					hpString += "I";
				}
				hpString += "]";
			}
			else
			{
				//Phase 2, the fire shield display
				hpString = "Dark Tyrant's Fire Shield\n[ ";
				for (int i = 0; i < (int)shieldHealth / 5; i++)
				{
					hpString += " <> ";
				}
				hpString += " ]";
			}

			GUIStyle style = new GUIStyle(GUI.skin.box);
			style.fontSize = 20;
			GUI.Box(boxInfo, hpString, style);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		//Update our counters.
		stateCounter += Time.deltaTime;
		flickerCounter -= Time.deltaTime;

		#region Change State Check
		CheckState();
		#endregion

		if (flickerCounter < 0)
		{
			GetComponent<Renderer>().material.color = Color.black;
		}

		#region Phases
		/*
		#region Primary
		if (phase == BossPhase.Primary)
		{
			UpdatePrimary();
		}
		#endregion
		#region Transition
		if (phase == BossPhase.Transition)
		{
			UpdateTransition();
		}
		#endregion
		#region Secondary
		if (phase == BossPhase.Secondary)
		{
			UpdateSecondary();
		}
		#endregion
		#region Morph
		if (phase == BossPhase.Morph)
		{
			UpdateMorph();
		}
		#endregion
		#region Final
		if (phase == BossPhase.Final)
		{
			UpdateFinal();
		}
		#endregion
		#region Dying
		if (phase == BossPhase.Dying)
		{
			UpdateDying();
		}
		#endregion
		*/
		#endregion
	}

	void CheckState()
	{
		if (health <= 85 && phase == BossPhase.Primary)
		{
			ChangeState(BossPhase.Transition);
		}
		if (stateCounter > transDuration && phase == BossPhase.Transition)
		{
			ChangeState(BossPhase.Secondary);
		}
		if (shieldHealth <= 0 && phase == BossPhase.Secondary)
		{
			ChangeState(BossPhase.Morph);
		}
		if (stateCounter > morphDuration && phase == BossPhase.Morph)
		{
			ChangeState(BossPhase.Final);
		}
		if (health <= 0 && phase == BossPhase.Final)
		{
			ChangeState(BossPhase.Dying);
		}
		if (health <= 0 && stateCounter >= dyingDuration && phase == BossPhase.Dying)
		{
			GameObject.FindGameObjectWithTag("Player").transform.FindChild("AudioBus").GetComponent<AudioSource>().Stop();

			deathExplosion.GetComponent<AudioSource>().Play();
			deathExplosion.GetComponent<Detonator>().Explode();
			victorySphere.SetActive(true);
			gameObject.SetActive(false);
		}
	}

	void ChangeState(BossPhase targPhase)
	{
		#region Phases
		#region Primary
		if (phase == BossPhase.Primary)
		{
			gunSpark.ResetSignal();
			gunSpark.enabled = false;
			gunSpark.counter = -3;
			GetComponent<AudioSource>().clip = newAge;
			GetComponent<AudioSource>().Play();
			ChangeTokens(true);
			shield.SetActive(true);
			ChangeAtmoLight(atmosphereLights[1], transDuration, .10f);
			platformIgniter.enabled = true;
			platformIgniter.ignited = Igniter.PlatformGroup.Inner;
		}
		#endregion
		#region Transition
		if (phase == BossPhase.Transition)
		{
			NextTelePlane();
			spawner.enabled = true;
			platformIgniter.enabled = true;
		}
		#endregion
		#region Secondary
		if (phase == BossPhase.Secondary)
		{
			platformIgniter.DisableAll();
			platformIgniter.enabled = false;
			gunSpark.enabled = false;
			gunSpark.ResetSignal();
			gunSpark.counter = -3;
			spawner.counter = -5;
			spawner.enabled = false;
			ChangeTokens(false);
			GetComponent<AudioSource>().clip = abyssBeckons;
			GetComponent<AudioSource>().Play();
			ChangeAtmoLight(atmosphereLights[2], morphDuration, .10f);
		}
		#endregion
		#region Morph
		if (phase == BossPhase.Morph)
		{
			NextTelePlane();
			platformIgniter.enabled = true;
			gunSpark.enabled = true;
			spawner.enabled = true;
			shield.SetActive(false);
		}
		#endregion
		#region Final
		if (phase == BossPhase.Final)
		{
			platformIgniter.DisableAll();
			platformIgniter.enabled = false;
			gunSpark.enabled = false;
			gunSpark.ResetSignal();
			spawner.enabled = false;
			GetComponent<AudioSource>().clip = lastWords;
			GetComponent<AudioSource>().Play();
			ChangeAtmoLight(atmosphereLights[0], dyingDuration, .1f);
			ResetTelePlane();
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.transform.position = player.GetComponent<TeleTarget>().teleTarget.transform.position;
			player.transform.rotation = player.GetComponent<TeleTarget>().teleTarget.transform.rotation;
			player.GetComponent<PlayerStats>().healPlayer(1000);
			player.GetComponent<CharacterMotor>().SetVelocity(new Vector3());
		}
		#endregion
		#region Dying
		//if (phase == BossPhase.Dying)
		//{ //This code can't ever happen. ChangeState is never called when the boss is dying.
		//    audio.Play();
		//}
		#endregion
		#endregion

		//Debug.Log("Changed State from " + phase + " to " + targPhase);
		
		stateCounter = 0.0f;
		phase = targPhase;
	}

	/// <summary>
	/// For resetting the teleplane back to the initial one.
	/// </summary>
	void ResetTelePlane()
	{
		if (telePlanes != null)
		{
			for (int i = 1; i < telePlanes.Length; i++)
			{
				telePlanes[i].SetActive(false);
			}
			telePlanes[0].SetActive(true);
		}
	}

	/// <summary>
	/// Advances to the next teleplane in the array. It only goes one way. Also turns off previous teleplane.
	/// </summary>
	void NextTelePlane()
	{
		if (activeTelePlane < telePlanes.Length)
		{
			telePlanes[activeTelePlane].SetActive(false);
			activeTelePlane++;
			telePlanes[activeTelePlane].SetActive(true);
		}
	}

	/// <summary>
	/// Toggle the token spawner's as active or inactive
	/// </summary>
	/// <param name="newActive"></param>
	void ChangeTokens(bool newActive)
	{
		tokenGroup.SetActive(newActive);
	}

	/// <summary>
	/// Change all lights within the scene
	/// </summary>
	/// <param name="newColor">Color we want the dir lights to be</param>
	/// <param name="changeDur">How long the change will take</param>
	/// <param name="newIntensity">How bright we want the dir lights to be</param>
	void ChangeAtmoLight(Color newColor, float changeDur, float newIntensity)
	{
		foreach (Light dirLight in worldDirLights)
		{
			dirLight.GetComponent<LerpColor>().ChangeColor(newColor, changeDur, newIntensity);
			dirLight.intensity = newIntensity;
			dirLight.color = newColor;
		}
	}

	/// <summary>
	/// Apply damage to the boss
	/// </summary>
	/// <param name="damageDealt">How badly hurt</param>
	/// <param name="flickerDuration">How long he's red for</param>
	/// <param name="hurtShield">Does it hurt the shield?</param>
	public void DamageBoss(int damageDealt, float flickerDuration, bool hurtShield)
	{
		if (hurtShield)
		{
			shieldHealth -= damageDealt;
		}
		else
		{
			health -= damageDealt;
		}
		flickerCounter = flickerDuration;
		GetComponent<Renderer>().material.color = Color.red;
	}

	//Unneeded functions. Intended to use them but all these computations got done elsewhere in triggers.
	void UpdatePrimary()
	{

	}
	void UpdateTransition()
	{

	}
	void UpdateSecondary()
	{

	}
	void UpdateMorph()
	{

	}
	void UpdateFinal()
	{

	}
	void UpdateDying()
	{

	}
}

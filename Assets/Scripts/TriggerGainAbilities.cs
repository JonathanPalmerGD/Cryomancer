using UnityEngine;
using System.Collections;

public class TriggerGainAbilities : MonoBehaviour
{
	//What abilities this trigger will grant
	public bool declineUnlocked = false;
	public bool inclineUnlocked = false;
	public bool shieldUnlocked = false;
	public bool platformUnlocked = false;

	//Only work once?
	public bool turnOff = false;

	//Turn off so we don't clip the particles with the player
	public bool deactivateWhileIn = true;

	//Noise we make when we trigger collision
	public AudioClip acquireClip;

	//Refill the player's ice?
	public bool restoreIce = true;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter(Collider collider)
	{
		//Only when we hit the player
		if (collider.tag == "Player")
		{
			//Get the player's IRP
			Cryomancer runner = GameObject.FindGameObjectWithTag("Player").GetComponent<Cryomancer>();
			
			//If they had that ability or we are giving it, set it to true.
			runner.declineUnlocked = runner.declineUnlocked || declineUnlocked;
			runner.inclineUnlocked = runner.inclineUnlocked || inclineUnlocked;
			runner.shieldUnlocked = runner.shieldUnlocked || shieldUnlocked;
			runner.platformUnlocked = runner.platformUnlocked || platformUnlocked;

			//If we unlocked something, play a clip. We run this check because this script is used also to just restore the player's ice.
			if (platformUnlocked || shieldUnlocked || inclineUnlocked || declineUnlocked)
			{
				
				collider.gameObject.audio.clip = acquireClip;
				collider.gameObject.audio.Play();
			}

			if (deactivateWhileIn)
			{
				//Turn off the particle system, kill its particles and stop renderer
				particleSystem.Clear();
				particleSystem.Stop();
				renderer.enabled = false;
			}

			if (turnOff)
			{
				//Deactivate ourselves
				gameObject.SetActive(false);
			}
			if (restoreIce)
			{
				runner.ice = runner.maxIce;
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Player")
		{
			//When they leave, turn back on
			if (deactivateWhileIn)
			{
				renderer.enabled = true;
				particleSystem.Play();
			}
		}
	}

}

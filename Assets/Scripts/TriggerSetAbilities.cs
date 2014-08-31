using UnityEngine;
using System.Collections;

public class TriggerSetAbilities : MonoBehaviour 
{
	//What abilities we want the player to have.
	public bool declineUnlocked = true;
	public bool inclineUnlocked = true;
	public bool shieldUnlocked = true;
	public bool platformUnlocked = true;

	//Contact noise
	public AudioClip contactClipToPlay;
	public bool setMaxIce = false;
	public float newMaxIce = 25;
	public bool drainAllIce = true;

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
		if (collider.tag == "Player")
		{
			Cryomancer runner = collider.gameObject.GetComponent<Cryomancer>(); 
			
			//If we take a power away from the player
			if ((runner.declineUnlocked || runner.inclineUnlocked || runner.shieldUnlocked || runner.platformUnlocked) && contactClipToPlay != null)
			{
				//Play the audio
				collider.audio.clip = contactClipToPlay;
				collider.audio.Play();
			}

			//If we let them keep a power, they can keep it, otherwise remove it
			runner.declineUnlocked = declineUnlocked;
			runner.inclineUnlocked = inclineUnlocked;
			runner.shieldUnlocked = shieldUnlocked;
			runner.platformUnlocked = platformUnlocked;
			
			//Reset their ice (If used as a world bounding volume)
			if (drainAllIce)
			{
				runner.ice = 0;
				runner.refreshTime = 0;
			}

			//Reset any ice they have gained?
			if (setMaxIce)
			{
				runner.changeMaxIce(newMaxIce);
			}
		}
	}
}

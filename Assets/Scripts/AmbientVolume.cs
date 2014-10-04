using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class AmbientVolume : MonoBehaviour
{
	[SerializeField]
	public List<AmbientAudio> ambAudios;

	public GameObject player;
	public Mesh mesh;
	private float counter = 0f;
	private float threshold = 0f;

	public void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		mesh = this.GetComponent<MeshFilter>().mesh;
	}

	public void Update()
	{
		counter += Time.deltaTime;
		
		if(collider.bounds.Contains(player.transform.position))
		{
			Debug.Log("Inside Mesh\n");
			foreach (AmbientAudio aud in ambAudios)
			{
				if (aud.sleeping)
				{
					if (aud.sleepCounter > 0)
					{
						aud.sleepCounter -= Time.deltaTime;
					}
					if (aud.sleepCounter < 0)
					{
						aud.sleepCounter = 0;
						aud.sleeping = false;
					}
				}
			}

			if (counter >= 3 + threshold)
			{
				counter -= 3.0f;
				threshold += .2f;

				RollForAmbient();
			}

		}
		//while the player is inside
		//Increase the counter of each ambient audio.
		//If we exceed the counter, play that audio.
	}

	private void RollForAmbient()
	{
		for (int i = 0; i < 3; i++)
		{
			int playIndex = UnityEngine.Random.Range(0, ambAudios.Count);
			Debug.Log("Rolling for ambient noise: " + playIndex + " out of " + ambAudios.Count + "\n");
			if (ambAudios[playIndex].minFreq > counter && !ambAudios[playIndex].sleeping)
			{
				float randRange = UnityEngine.Random.Range(0.0f, 1.0f);
				Debug.Log("Rolled: " + randRange + " out of " + ambAudios[playIndex].playChance + "\n");
				if (randRange < ambAudios[playIndex].playChance)
				{
					PlayAudio(ambAudios[playIndex]);
				}
			}
		}
	}

	private void PlayAudio(AmbientAudio ambientToPlay)
	{
		AudioSource audio = player.AddComponent<AudioSource>();
		int randIndex = UnityEngine.Random.Range(0, ambientToPlay.clips.Count);
		Debug.Log("Playing Audio: " + ambientToPlay.clips[randIndex].name);
		audio.PlayOneShot(ambientToPlay.clips[randIndex]);
		ambientToPlay.GotPlayed();
	}
}

[Serializable]
public class AmbientAudio
{
	public string name;
	public float volume;
	[SerializeField]
	public List<AudioClip> clips;

	public float playChance;

	public float minFreq;

	//If it is sleeping and for how long
	public float sleepDuration;
	public float sleepCounter;
	public bool sleeping;

	public AmbientAudio()
	{
		name = "New Ambient Sound";
		volume = 1.0f;
		minFreq = 10f;
		sleepDuration = 10f;
		sleepCounter = 3;
		sleeping = true;
		clips = new List<AudioClip>();

	}

	public void GotPlayed()
	{
		sleeping = true;
		sleepCounter = sleepDuration;
	}
}

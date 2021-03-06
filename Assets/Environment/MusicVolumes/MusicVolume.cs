﻿using UnityEngine;
using System.Collections;

public class MusicVolume : MonoBehaviour 
{
	public bool triggered = false;
	public AudioClip music;

	void Start()
	{
		GetComponent<Renderer>().enabled = false;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (music != null)
		{
			if (!triggered)// && clipName.Length > 0)
			{
				if (collider.gameObject.tag == "Player")
				{
					triggered = true;

					GameObject child = collider.gameObject.transform.FindChild("AudioBus").gameObject;

					child.GetComponent<AudioSource>().clip = music;
					child.GetComponent<AudioSource>().Play();

				}
			}
		}
	}
}

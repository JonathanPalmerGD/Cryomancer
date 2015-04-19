using UnityEngine;
using System.Collections;

public class ScreenFlash : MonoBehaviour 
{
	public Texture splashTexture;
	public AudioClip hurtNoise;
	public float counter;
	public float painDuration;
	public bool displaySplash = true;

	void OnGUI()
	{
		//Draw the pain indicator
		if (displaySplash)
		{
			//Draw the squares on the corner of the screen Top left, bottom right, top right, bottom left
			GUI.DrawTexture(new Rect(0, 0, splashTexture.width, splashTexture.height), splashTexture);
			GUI.DrawTexture(new Rect(Screen.width - splashTexture.width, Screen.height - splashTexture.height, splashTexture.width, splashTexture.height), splashTexture);
			GUI.DrawTexture(new Rect(Screen.width - splashTexture.width, 0, splashTexture.width, splashTexture.height), splashTexture);
			GUI.DrawTexture(new Rect(0, Screen.height - splashTexture.height, splashTexture.width, splashTexture.height), splashTexture);
		}
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Only display for a short period. Count down till we hit that time
		if (displaySplash)
		{
			counter = counter + Time.deltaTime;
			if (counter > painDuration)
			{
				displaySplash = false;
				counter = 0.0f;
			}
		}
	}

	//Sets the display to true and will play an audio if the player got hurt by something with a non-default noise. I haven't finished getting different hurt noises.
	public void FlashScreen(AudioClip playAudio)
	{
		displaySplash = true;
		if (playAudio != null)
		{
			GetComponent<AudioSource>().clip = playAudio;
			GetComponent<AudioSource>().Play();
		}
		else
		{
			GetComponent<AudioSource>().clip = hurtNoise;
			GetComponent<AudioSource>().Play();
		}
	}
}

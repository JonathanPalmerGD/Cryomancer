using UnityEngine;
using System.Collections;

public class TelePlaneDamage : MonoBehaviour 
{
	//This class wants to go onto a flat plane with a texture on it. If the player falls below this plane, it will teleport the player back to the player's TeleTarget.
	//TeleTarget can be automatically updated when the player reaches Checkpoints.

	//Don't forget to turn off your plane collider.

	//So the plane can store a reference to the player.
	private GameObject player;
	private PlayerStats stats;

	public int difficulty = 1;

	public AudioClip fallAudio;

	//How much damage the player takes.
	public float[] damageOnReset = { 10, 5, 25 };

	public bool sendToStart = false;

	public Vector3 startPos;
	public Quaternion startRot;

	public GameStats gameStats;

	// Use this for initialization
	void Start () 
	{
		gameStats = GameObject.FindGameObjectWithTag("Properties").GetComponent<GameStats>();
		difficulty = gameStats.difficulty;
		player = GameObject.FindGameObjectWithTag("Player");
		if (player.GetComponent<PlayerStats>() != null)
		{
			stats = player.GetComponent<PlayerStats>();
		}
		else
		{
			player.AddComponent<PlayerStats>();
		}

		if (player.GetComponent<TeleTarget>() == null)
		{
			player.AddComponent<TeleTarget>();
		}
		startPos = player.transform.position;
		startRot = player.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If the player is below the plane
		if(transform.position.y > player.transform.position.y)
		{
			//If the target object exists
			if (player.GetComponent<TeleTarget>() != null && player.GetComponent<TeleTarget>().teleTarget != null)
			{
				//Put the player there.
				player.transform.position = player.GetComponent<TeleTarget>().teleTarget.transform.position;
				player.transform.rotation = player.GetComponent<TeleTarget>().teleTarget.transform.rotation;
			}
			else
			{
				//Put the player back at their start.
				player.transform.position = startPos;
				player.transform.rotation = startRot;
			}

			CharacterMotor charMotor = player.GetComponent<CharacterMotor>();
			charMotor.SetVelocity(new Vector3(0, 0, 0));

			//Damage the player
			Debug.Log(damageOnReset.Length);
			stats.health = stats.health - damageOnReset[difficulty];
			
			

			if (player.GetComponent<ScreenFlash>() != null)
			{
				player.GetComponent<ScreenFlash>().FlashScreen(fallAudio);
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour 
{
	//How much it hurts the player when it hits the player
	public int damage = 25;

	//Our time counter
	public float counter = 0.0f;
	//How long between 'effect on player'
	public float resetTime = 0.80f;
	//Can it hurt or influence the player?
	public bool canHurt = true;
	//Turn on rigid bodies?
	public bool enableGravityOnHit = true;
	//Should we knock the player back
	public bool knockBack = true;
	//How much
	public float knockBackAmount = 50.0f;

	public AudioClip hurtAudio;

	private GameObject player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Count upward until we hit our reset.
		if (counter < resetTime)
		{
			counter += Time.deltaTime;
		}
		else
		{
			//Say we can hurt the player when we exceed the time.
			canHurt = true;
		}

		//Find the distance to the player.
		float distanceBetween = Vector3.Distance(player.transform.position, transform.position);

		//If we can hurt the player AND they are within a certain distance of the object
		if (canHurt && (transform.localScale.x / 2) + 2.0f > distanceBetween)
		{
			//If we knock the player back
			if (knockBack)
			{
				//Punch them in the face
				PlayerStats stats = player.GetComponent<PlayerStats>();
				stats.health = stats.health - damage;

				if (player.GetComponent<ScreenFlash>() != null)
				{
					player.GetComponent<ScreenFlash>().FlashScreen(hurtAudio);
				}

				//Say we can't hurt them, reset our counter.
				canHurt = false;
				counter = 0.0f;

				//Knock the player back.
				CharacterMotor cMotor = player.GetComponent<CharacterMotor>();
				Vector3 knockBackV = GetComponent<Charger>().dirToPlayer.normalized * knockBackAmount;
				cMotor.SetVelocity(knockBackV);
			}
		}
	}

	void OnCollisionEnter(Collision c)
	{
		//If when we hit something and it has a rigid body
		if (enableGravityOnHit && c.gameObject.rigidbody != null)
		{
			//Say that thing now respects gravity.
			c.gameObject.rigidbody.useGravity = true;
		}

		/*//If the orb hasn't hurt the player recently. and the thing we hit was the player
		if (c.gameObject.tag == "Player")
		{
			//Reset the counter and the 'canhit'
			counter = 0.0f;
			canHurt = false;
			player.GetComponent<HealthGUI>().health--;
			//Application.LoadLevel(Application.loadedLevel);
			if (knockBack)
			{
				CharacterMotor cMotor = c.gameObject.GetComponent<CharacterMotor>();
				//Debug.DrawRay(transform.position, GetComponent<Charger>().dirToPlayer, Color.black, 3.0f);
				cMotor.SetVelocity(GetComponent<Charger>().dirToPlayer.normalized * knockBackAmount);
			}
		}

		*/
	}
}

using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
	//Public means anyone can access this. Private means only this class can.
	//Player is private because we're automatically populating it with the player.
	private GameObject player;

	//Some info about how big the checkpoint's collision is.
	public float collisionRadius = 3.0f;

	//Don't forget to face the checkpoint

	// Use this for initialization
	void Start () 
	{
		//Get the player, put it in our private 'player' shoebox.
		player = GameObject.FindGameObjectWithTag("Player");

		if (player.GetComponent<TeleTarget>() == null)
		{
			player.AddComponent<TeleTarget>();
		}

		//Turn off the particle system. We'll turn it back on when the player collides with it.
		ParticleSystem particlesystem = (ParticleSystem)this.GetComponent("ParticleSystem");
		if (particlesystem != null)
		{
			particlesystem.enableEmission = false;
		}
	}
	
	// This should get updated to be trigger specific
	void Update () 
	{
		//We toggle the particle system to active if we're the active checkpoint
		ParticleSystem particlesystem = (ParticleSystem)this.GetComponent("ParticleSystem");

		if (player.GetComponent<TeleTarget>().teleTarget != null)
		{
			//If this checkpoint ISN'T the player's teleTarget. If it is, we don't need to check any of this.
			if (player.GetComponent<TeleTarget>().teleTarget.transform.position != this.transform.position)
			{
				//Make sure the particle system is off.
				if (particlesystem != null)
				{
					particlesystem.enableEmission = false;
				}

				//Find the distance between the player and the checkpoint.
				float distanceBetween = Vector3.Distance(player.transform.position, transform.position);

				//If the two radii are greater than the distance between, we're colliding.
				if (collisionRadius > distanceBetween)
				{
					//Turn on the particles to display this is an active checkpoint.
					if (particlesystem != null)
					{
						particlesystem.enableEmission = true;
					}

					//Set the player's teletarget
					player.GetComponent<TeleTarget>().teleTarget = this.gameObject;
				}
			}
			else
			{
				//Have the particle system on.
				if (particlesystem != null)
				{
					particlesystem.enableEmission = true;
				}
			}
		}
		else
		{
			//Find the distance between the player and the checkpoint.
			float distanceBetween = Vector3.Distance(player.transform.position, transform.position);

			//If the two radii are greater than the distance between, we're colliding.
			if (collisionRadius > distanceBetween)
			{
				//Turn on the particles to display this is an active checkpoint.
				if (particlesystem != null)
				{
					particlesystem.enableEmission = true;
				}

				//Set the player's teletarget
				player.GetComponent<TeleTarget>().teleTarget = this.gameObject;
			}
		}
	}
}

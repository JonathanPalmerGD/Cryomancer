using UnityEngine;
using System.Collections;

public class FireAtPlayer : MonoBehaviour 
{
	private GameObject player;

	//The difficulty that is set on scene load. Controls projectile creation difficulty
	public int difficulty = 1;

	#region Projectile Prefabs
	public GameObject streamProjectile;
	public GameObject laserProjectile;
	public GameObject shotgunProjectile;
	public GameObject homingProjectile;
	#endregion

	//Our display so we can send signals to the player.
	public ParticleSystem partSys;
	public Light lightSignal;

	//Where the player is so we know where to fire.
	public Vector3 dirToPlayer;
	public GameStats gameStats;
	public float counter = 0;
	#region Gun and Bullet Information
	public int reloadCount = 0;
	public int buckCount = 15;
	public float scatterAmount = .5f;
	public float rapidFireCount = 4;
	public float laserFireCount = 20;
	public float fireRate = 0.40f;
	public float laserFireRate = 0.05f;
	public float waitDuration = 5.0f;
	public float streamVelocity = 110.0f;
	public float laserVelocity = 110.0f;
	public float buckVelocity = 50.0f;
	public float streamBulletLifeTime = 3.0f;
	public float laserBulletLifeTime = 2.0f;
	public float buckLifeTime = 3.0f;
	public float homingLifeTime = 10.0f;
	public float percentagePlayerVelLeading = .70f;
	#endregion
	public Color[] colorOfSignal;

	public int fireOnly = 0;

	#region State control
	public enum GunType { Shotgun, Stream, Homing, Laser};
	public GunType gunType = GunType.Stream;

	public enum FireState { Waiting, Firing };
	public FireState fireState = FireState.Waiting;
	#endregion

	// Use this for initialization
	void Start () 
	{
		gameStats = GameObject.FindGameObjectWithTag("Properties").GetComponent<GameStats>();
		difficulty = gameStats.difficulty;
		counter = -3.0f;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	/// <summary>
	/// Change display colors based on attack
	/// </summary>
	void SetLightColor()
	{
		if (counter > waitDuration - 1.0f)
		{
			lightSignal.enabled = true;
			if (gunType == GunType.Stream)
			{
				lightSignal.color = colorOfSignal[0];
				partSys.startColor = colorOfSignal[0];
				//partSys.startColor = new Color(1, 0, .4f, .3f);
			}
			else if (gunType == GunType.Shotgun)
			{
				lightSignal.color = colorOfSignal[1];
				partSys.startColor = colorOfSignal[1];
				//partSys.startColor = new Color(1, 0, 0, .3f);
			}
			else if (gunType == GunType.Homing)
			{
				lightSignal.color = colorOfSignal[2];
				partSys.startColor = colorOfSignal[2];
				//partSys.startColor = new Color(.4f, 0, .7f, .3f);
			}
			else if (gunType == GunType.Laser)
			{
				lightSignal.color = colorOfSignal[3];
				partSys.startColor = colorOfSignal[3];
				//partSys.startColor = new Color(.5f, 0, .5f, .3f);
			}
		}
		else
		{
			lightSignal.color = new Color(0, 0, 0, 1);
			lightSignal.enabled = false;
			partSys.startColor = new Color(0, 0, 0, .3f);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		//Update our timer
		counter += Time.deltaTime;
		CharacterMotor charMotor = player.GetComponent<CharacterMotor>();

		//Find the player
		dirToPlayer = (player.transform.position - transform.position) + charMotor.movement.velocity * percentagePlayerVelLeading;

		//Manage our color
		SetLightColor();

		FireOnlyCheck();

		//Depending on what we're firing, call an appropriate method - Yay state architecture
		#region Gun Fire Callls
		if (gunType == GunType.Stream)
		{
			FireStream();
		}
		else if (gunType == GunType.Shotgun)
		{
			FireShotgun();
		}
		else if (gunType == GunType.Homing)
		{
			FireHoming();
		}
		else if (gunType == GunType.Laser)
		{
			FireLaser();
		}
		#endregion
	}

	public void ResetSignal()
	{
		lightSignal.color = new Color(0, 0, 0, 1);
		lightSignal.enabled = false;
		partSys.startColor = new Color(0, 0, 0, .3f);
	}
	
	/// <summary>
	/// Checks if we are only firing one weapon type. Good for debug and possible boss setups
	/// </summary>
	public void FireOnlyCheck()
	{
		if (fireOnly != 0)
		{
			if (fireOnly == 1)
			{
				gunType = GunType.Stream;
			}
			if (fireOnly == 2)
			{
				gunType = GunType.Shotgun;
			}
			if (fireOnly == 3)
			{
				gunType = GunType.Homing;
			}
			if (fireOnly == 4)
			{
				gunType = GunType.Laser;
			}
		}
	}

	/// <summary>
	/// Alternates between random gun and stream shot
	/// </summary>
	public void PickNextGunType()
	{
		//If he fired stream last time, fire random this time.
		if (gunType == GunType.Stream)
		{
			switch (Random.Range(0, 3))
			{
				case 0:
					gunType = GunType.Shotgun;
					break;
				case 1:
					gunType = GunType.Laser;
					break;
				case 2:
					gunType = GunType.Homing;
					break;
			}
		}
		else
		{
			gunType = GunType.Stream;
		}
	}

	#region Weapon Fire Types
	//Mose of these methods are fairly similar. Most do velocity setting and difficulty scaling. They fire at different rates and create diffferent projectiles.
	//The homing projectile auto-homes so we just need to set and forget it.
	public void FireShotgun()
	{
		if (fireState == FireState.Waiting)
		{
			if (counter > waitDuration)
			{
				fireState = FireState.Firing;
				counter -= waitDuration;
			}
		}
		else if (fireState == FireState.Firing)
		{
			for (int i = 0; i < buckCount; i++)
			{
				Vector3 randV = new Vector3(1.0f * Random.Range(-scatterAmount, scatterAmount), 1.0f * Random.Range(-scatterAmount, scatterAmount), 1.0f * Random.Range(-scatterAmount, scatterAmount)) ;
				GameObject bullet = (GameObject)Instantiate(shotgunProjectile, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), new Quaternion());
				
				bullet.GetComponent<EvilToken>().difficulty = difficulty;
				
				bullet.GetComponent<Rigidbody>().AddForce((dirToPlayer + randV) * buckVelocity * bullet.GetComponent<Rigidbody>().mass);

				Destroy(bullet, buckLifeTime);
			}

			fireState = FireState.Waiting;
			counter = 0;
			PickNextGunType();
			reloadCount = 0;
		}
	}

	public void FireStream()
	{
		if (fireState == FireState.Waiting)
		{
			if (counter > waitDuration)
			{
				fireState = FireState.Firing;
				counter -= waitDuration;
			}
		}
		else if (fireState == FireState.Firing)
		{
			if (reloadCount < rapidFireCount)
			{
				if (counter > fireRate)
				{
					counter -= fireRate;
					GameObject bullet = (GameObject)Instantiate(streamProjectile, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), new Quaternion());

					bullet.GetComponent<EvilToken>().difficulty = difficulty;

					bullet.GetComponent<Rigidbody>().AddForce(dirToPlayer * streamVelocity * bullet.GetComponent<Rigidbody>().mass);

					reloadCount++;

					Destroy(bullet, streamBulletLifeTime);
				}
			}
			else
			{
				fireState = FireState.Waiting;
				reloadCount = 0;
				PickNextGunType();
			}
		}
	}
	
	public void FireLaser()
	{
		if (fireState == FireState.Waiting)
		{
			if (counter > waitDuration)
			{
				fireState = FireState.Firing;
				counter -= waitDuration;
			}
		}
		else if (fireState == FireState.Firing)
		{
			if (reloadCount < laserFireCount)
			{
				if (counter > laserFireRate)
				{
					counter -= laserFireRate;
					GameObject bullet = (GameObject)Instantiate(laserProjectile, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), new Quaternion());

					bullet.GetComponent<EvilToken>().difficulty = difficulty;

					bullet.GetComponent<Rigidbody>().AddForce(dirToPlayer * laserVelocity * bullet.GetComponent<Rigidbody>().mass);

					reloadCount++;

					Destroy(bullet, laserBulletLifeTime);
				}
			}
			else
			{
				fireState = FireState.Waiting;
				reloadCount = 0;
				PickNextGunType();
			}
		}
	}

	public void FireHoming()
	{
		if (fireState == FireState.Waiting)
		{
			if (counter > waitDuration)
			{
				fireState = FireState.Firing;
				counter -= waitDuration;
			}
		}
		else if (fireState == FireState.Firing)
		{
			GameObject bullet = (GameObject)Instantiate(homingProjectile, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), new Quaternion());

			bullet.GetComponent<EvilToken>().difficulty = difficulty;

			Destroy(bullet, homingLifeTime);
			
			fireState = FireState.Waiting;
			reloadCount = 0;
			counter = 0;
			PickNextGunType();
		}
	}
	#endregion
}

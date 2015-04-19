using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour 
{
	private GameObject player;
	public float approachSpeed = 7.5f;
	public float distAbovePlayer = 0;
	public GameObject[] bodyParts;

	public enum DragonState { Shotgun, Stream, Homing, Laser };
	public DragonState curState = DragonState.Stream;

	public enum FireState { Waiting, Firing };
	public FireState fireState = FireState.Waiting;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (curState == DragonState.Stream)
		{
			FireStream();
		}
		else if (curState == DragonState.Shotgun)
		{
			FireShotgun();
		}
		else if (curState == DragonState.Homing)
		{
			FireHoming();
		}
		else if (curState == DragonState.Laser)
		{
			FireLaser();
		}


		Vector3 dirToPlayer = player.transform.position - transform.position;
		dirToPlayer += Vector3.up * distAbovePlayer;
		GetComponent<Rigidbody>().velocity = dirToPlayer.normalized * approachSpeed;

		//float diffX = player.transform.position.x - transform.position.x;
		//float diffZ = player.transform.position.z - transform.position.z;

		Vector3 netForward = new Vector3();
		foreach (GameObject g in bodyParts)
		{
			netForward += g.transform.right * 3.0f;
			//Debug.DrawLine(g.transform.position + Vector3.up * 1.5f, g.transform.position + Vector3.up * 1.5f + Vector3.forward * 3.0f, Color.green);
		}
		Debug.DrawLine(transform.position, transform.position + netForward, Color.red, 3.0f);
		Debug.DrawLine(transform.position, player.transform.position, Color.green);
		Debug.DrawLine(transform.position + netForward, player.transform.position, Color.blue);

		Vector3 blueLine = player.transform.position - (transform.position + netForward);
		Vector3 greenLine = player.transform.position - (transform.position);

		Debug.DrawLine(player.transform.position, Vector3.Cross(blueLine, greenLine), Color.cyan);
		//transform.rotat(transform.up, .1f);
		//transform.LookAt(player.transform, Vector3.up);
	}


	void FireShotgun()
	{

	}

	void FireStream()
	{

	}
	
	void FireHoming()
	{

	}

	void FireLaser()
	{

	}
}

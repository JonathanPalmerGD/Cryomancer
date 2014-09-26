using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour 
{
	private GameObject player;
	public float approachSpeed = 7.5f;
	public float distAbovePlayer = 0;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Move in the direction of our target at our approach speed
		Vector3 dirToPlayer = player.transform.position - transform.position;
		dirToPlayer += Vector3.up * distAbovePlayer;
		rigidbody.velocity = dirToPlayer.normalized * approachSpeed;
	}
}

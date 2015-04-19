using UnityEngine;
using System.Collections;

public class Token : MonoBehaviour 
{
	private GameObject player;
	public float collisionRadius = 3.0f;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () 
	{
		float distanceBetween = Vector3.Distance(player.transform.position, transform.position);
		if (collisionRadius > distanceBetween)
		{
			enabled = false;
			GetComponent<Renderer>().enabled = false;
			GetComponent<ParticleSystem>().enableEmission = false;
		}
	}
}

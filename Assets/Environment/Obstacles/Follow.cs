using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour 
{
	private GameObject player;
	public float approachSpeed = 240f;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 temp = Vector3.zero;
		temp.x = (transform.position.x - player.transform.position.x) / approachSpeed;
		temp.y = (transform.position.y - player.transform.position.y) / approachSpeed;
		temp.z = (transform.position.z - player.transform.position.z) / approachSpeed;

		transform.position -= temp;
	}
}

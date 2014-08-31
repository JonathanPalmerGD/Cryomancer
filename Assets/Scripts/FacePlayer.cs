using UnityEngine;
using System.Collections;

public class FacePlayer : MonoBehaviour 
{
	private GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt(player.transform);
		//transform.RotateAround(Vector3.up, -80.0f);
		transform.Rotate(Vector3.up, -80.0f);
	}
}

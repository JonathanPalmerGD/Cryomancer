using UnityEngine;
using System.Collections;

public class RisingTide : MonoBehaviour 
{
	private Vector3 baseRate = new Vector3(0, 1f, 0);
	private float distance = 150;
	private Vector3 fastRate = new Vector3(0, 15f, 0);
	private GameObject player;
	public AnimationCurve curve;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update ()
	{
		//What is our distance to the player

		//Debug.Log("Zero: " + curve.Evaluate(0) + "\n");
		//Debug.Log("Half: " + curve.Evaluate(0.5f) + "\n");
		//Debug.Log("One: " + curve.Evaluate(1.0f) + "\n");
		//Debug.Log("MOAR: " + curve.Evaluate(1.5f) + "\n");

		Vector3 speed = baseRate + fastRate * curve.Evaluate((player.transform.position.y - transform.position.y) / distance);
		Debug.Log("Speed: " + speed + "\n");
		transform.position = transform.position + (speed * Time.deltaTime); 
		
		/*

		if (player.transform.position.y > transform.position.y + distance)
		{
			Debug.Log("Fast\n");
			transform.position = transform.position + (fastRate * Time.deltaTime);
		}
		else
		{
			Debug.Log("Slow\n");
			transform.position = transform.position + (slowRate * Time.deltaTime);
		}*/
	}
}

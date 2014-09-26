using UnityEngine;
using System.Collections;

public class RisingTide : MonoBehaviour 
{
	private Vector3 baseRate = new Vector3(0, 1f, 0);
	private float distance = 150;
	private Vector3 fastRate = new Vector3(0, 15f, 0);
	private GameObject player;
	public AnimationCurve curve;
	public float counter;
	public float drainDuration;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update ()
	{
		if (drainDuration > 0)
		{
			drainDuration -= Time.deltaTime;
			Vector3 speed = baseRate;// +fastRate * curve.Evaluate(1 - (player.transform.position.y - transform.position.y) / distance);
			transform.position -= (speed * Time.deltaTime);
		}
		else
		{
			Vector3 speed = baseRate + fastRate * curve.Evaluate((player.transform.position.y - transform.position.y) / distance);
			transform.position += (speed * Time.deltaTime);
		}
	}
}

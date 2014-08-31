using UnityEngine;
using System.Collections;

public class TeleCollide : MonoBehaviour 
{
	public float counter = 5.0f;
	//Make a bool for which position this wants to be
	public Vector3 targetPosition;
	public Vector3 initial;

	// Use this for initialization
	void Start()
	{
		initial = transform.position;
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.contacts.Length);
		//ContactPoint contact = collision.contacts[0];
		//Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		if (collision.relativeVelocity.magnitude > 0)
		{
			transform.position = targetPosition;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class MovingCollide : MonoBehaviour {

	public float counter = 0.0f;
	public float moveTime = 5.0f;
	//Make a bool for which position this wants to be
	public Vector3 targetPosition;
	public Vector3 initial;
	public bool moveToTarget = false;

	// Use this for initialization
	void Start()
	{
		counter = 0.0f;
		initial = transform.position;
	}

	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log(collision.contacts.Length);
		//ContactPoint contact = collision.contacts[0];
		//Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		if (collision.relativeVelocity.magnitude > 0)
		{
			moveToTarget = true;
		}
	}


	// Update is called once per frame
	void Update()
	{
		if (moveToTarget)
		{
			counter += Time.deltaTime;

			float xPos = Mathf.Lerp(transform.position.x, targetPosition.x, counter / moveTime);
			float yPos = Mathf.Lerp(transform.position.y, targetPosition.y, counter / moveTime);
			float zPos = Mathf.Lerp(transform.position.z, targetPosition.z, counter / moveTime);

			transform.position = new Vector3(xPos, yPos, zPos);
		}
	}

}
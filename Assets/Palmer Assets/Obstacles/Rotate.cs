using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
	//How fast we rotate
	public float rotationSpeed = 0.01f;
	//Around what?
	public Vector3 rotationAxis = Vector3.up;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Old - Different objects can't rotate differently
		//transform.Rotate(Vector3.up, 1.0f);
		//New - Different  objects can be different speeds and more
		transform.Rotate(rotationAxis, rotationSpeed);
	}
}

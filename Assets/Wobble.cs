using UnityEngine;
using System.Collections;

public class Wobble : MonoBehaviour 
{
	//This isn't finished. It would be for something to wobble back and forth between two points. The idea was to use it for a complex rotation piston. It'd be better to model something in Maya than use this code.

	public GameObject axisObject;
	public float rotationSpeed = 0.01f;
	public Vector3 rotationAxis = Vector3.up;
	public Vector3 amp = new Vector3(4.0f, 4.0f, 4.0f);
	public Vector3 freq = new Vector3(10.0f, 10.0f, 10.0f);
	//private Quaternion initial;
	public bool clockwise = true;
	public float rotMin;
	public float rotMax;

	// Use this for initialization
	void Start()
	{
		rotMax = Mathf.Deg2Rad * rotMax;
		rotMin = Mathf.Deg2Rad * rotMin;
		//initial = transform.rotation;
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (clockwise)
		{
			Debug.Log("Clockwise");
			transform.RotateAround(axisObject.transform.position, rotationAxis, rotationSpeed);
		}
		else
		{
			Debug.Log("Counter clockwise");
			transform.RotateAround(axisObject.transform.position, rotationAxis, -rotationSpeed);
		}

		float checkRot = 0;
		if (rotationAxis == new Vector3(0,1,0))
		{
			checkRot = transform.rotation.y;
		}
		else if (rotationAxis == new Vector3(1, 0, 0))
		{
			checkRot = transform.rotation.x;
		}
		else if (rotationAxis == new Vector3(0, 0, 1))
		{
			checkRot = transform.rotation.z;
		}

		if (checkRot > rotMax)
		{
			clockwise = false;
		}
		else if (checkRot < rotMin)
		{
			clockwise = true;
		}
	}
}

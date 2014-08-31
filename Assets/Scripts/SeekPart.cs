using UnityEngine;
using System.Collections;

public class SeekPart : MonoBehaviour 
{
	public GameObject curTarg;
	public Vector3 posWithOffset;
	public Vector3 targWithOffset;
	public Vector3 myOffsetAmt;
	public Vector3 targOffsetAmt;
	public float baseDistFromNextTail;
	public float slowDistStart;

	//maximum speed of vehicle
	public float maxSpeed = 50.0f;
	// maximum force allowed
	public float maxForce = 100.0f;

	//movement variables - updated by this component
	//current speed of vehicle
	private float speed = 0.0f;
	//change in position per second
	private Vector3 velocity;
	//steering variable
	private Vector3 steeringForce;
	private Vector3 moveDirection;

	public Vector3 Velocity
	{
		get { return velocity; }
		set { velocity = value; }
	}

	public float Speed
	{
		get { return speed; }
		set { speed = Mathf.Clamp(value, 0, maxSpeed); }
	}

	// Use this for initialization
	void Start () 
	{
		slowDistStart = .3f;
		myOffsetAmt = transform.forward * renderer.bounds.extents.x;
		if (curTarg.renderer != null)
		{
			targOffsetAmt = curTarg.transform.forward * -1 * curTarg.renderer.bounds.extents.x;
		}
		else
		{
			targOffsetAmt = curTarg.transform.forward * -1;
		}

		posWithOffset = transform.position + myOffsetAmt;
		targWithOffset = curTarg.transform.position + targOffsetAmt;
		baseDistFromNextTail = Vector3.Distance(posWithOffset, targWithOffset);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//posWithOffset = transform.position + myOffsetAmt;
		//targWithOffset = curTarg.transform.position + targOffsetAmt;
		Debug.DrawLine(posWithOffset, targWithOffset, Color.white);

		CalcSteeringForce();
		ClampSteeringForce();

		Vector3 combinedVectors = (transform.forward + steeringForce.normalized).normalized;
		
		//if (combinedVectors != Vector3.zero)
		//	transform.right = (transform.right + combinedVectors).normalized;
		//transform.RotateAround(transform.up, .1f);
		transform.LookAt(targWithOffset);

		float curDistFromNextTail = Vector3.Distance(posWithOffset, targWithOffset);

		//Speed scale based on distance. If we're behind speed up
		if (curDistFromNextTail < (baseDistFromNextTail + slowDistStart))
		{
			renderer.material.color = Color.gray;
			combinedVectors = combinedVectors * (curDistFromNextTail / (baseDistFromNextTail + slowDistStart));
		}
		else
		{
			combinedVectors = combinedVectors * 1.5f * (curDistFromNextTail / (baseDistFromNextTail + slowDistStart));
			renderer.material.color = Color.red;

		}
		rigidbody.velocity = combinedVectors;


		posWithOffset = transform.position + myOffsetAmt;
		targWithOffset = curTarg.transform.position + targOffsetAmt;
	}

	void CalcSteeringForce()
	{
		steeringForce = Vector3.zero;
		steeringForce += Seek(targWithOffset);
	}

	void ClampSteeringForce()
	{
		if (steeringForce.magnitude > maxForce)
		{
			steeringForce.Normalize();
			steeringForce *= maxForce;
		}
	}

	public Vector3 Seek(Vector3 pos)
	{
		// find dv, the desired velocity
		Vector3 dv = pos - posWithOffset;
		dv.y = 0; //only steer in the x/z plane
		dv = dv.normalized * maxSpeed; //scale by maxSpeed
		dv -= transform.right * speed; //subtract velocity to get vector in that direction
		return dv;
	}

	//// same as seek pos above, but parameter is game object
	//public Vector3 Seek(GameObject gO)
	//{
	//    return Seek(gO.transform.position);
	//}
}

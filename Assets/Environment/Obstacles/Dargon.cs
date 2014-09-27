using UnityEngine;
using System.Collections;

public class Dargon : MonoBehaviour 
{
	//movement variables - exposed in inspector panel
	//maximum speed of vehicle
	public float maxSpeed = 50.0f;
	// maximum force allowed
	public float maxForce = 100.0f;

	//movement variables - updated by this component
	//current speed of vehicle
	public float speed = 0.0f;
	//steering variable
	private Vector3 steeringForce;
	private Vector3 moveDirection;

	private GameObject player;
	public float distAbovePlayer = 0;
	public GameObject[] bodyParts;
	public GameObject curTarg;

	public enum MovementState { Standard, Chase, Stop };
	public MovementState moveState;

	public enum DragonState { Shotgun, Stream, Homing, Laser };
	public DragonState curState = DragonState.Stream;

	public enum FireState { Waiting, Firing };
	public FireState fireState = FireState.Waiting;

	public float Speed
	{
		get { return speed; }
		set { speed = Mathf.Clamp(value, 0, maxSpeed); }
	}

	// Use this for initialization
	void Start ()
	{
		Speed = 10.0f;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		#region State Checking
		if (curState == DragonState.Stream)
		{
			FireStream();
		}
		else if (curState == DragonState.Shotgun)
		{
			FireShotgun();
		}
		else if (curState == DragonState.Homing)
		{
			FireHoming();
		}
		else if (curState == DragonState.Laser)
		{
			FireLaser();
		}
		#endregion

		
		CalcSteeringForce();
		ClampSteeringForce();

		Vector3 combinedVectors = ((transform.forward) + steeringForce.normalized).normalized;

		moveDirection = transform.forward * Speed;
		// movedirection equals velocity
		//add acceleration
		moveDirection += steeringForce * Time.deltaTime;
		//update speed
		Speed = moveDirection.magnitude;
		
		if (Speed != moveDirection.magnitude)
		{
			moveDirection = moveDirection.normalized * Speed;
		}

		//orient transform
		if (moveDirection != Vector3.zero)
			transform.forward = moveDirection;

		//Debug.Log((moveDirection * Time.deltaTime).magnitude);

		//rigidbody.velocity = moveDirection;// *Time.deltaTime;
		rigidbody.velocity = combinedVectors;

		//Debug.DrawLine(transform.position, transform.position + moveDirection, Color.white);
		// the CharacterController moves us subject to physical constraints
		//characterController.Move(moveDirection * Time.deltaTime);

		#region Dead Code
		//Vector3 dirToPlayer = player.transform.position - transform.position;
		//dirToPlayer += Vector3.up * distAbovePlayer;
		//rigidbody.velocity = dirToPlayer.normalized * approachSpeed;

		//float diffX = player.transform.position.x - transform.position.x;
		//float diffZ = player.transform.position.z - transform.position.z;

		
		/*Vector3 netForward = new Vector3();
		//foreach (GameObject g in bodyParts)
		//{
		//    netForward += g.transform.right * 3.0f;
		//    //Debug.DrawLine(g.transform.position + Vector3.up * 1.5f, g.transform.position + Vector3.up * 1.5f + Vector3.forward * 3.0f, Color.green);
		//}
		////Debug.Log(netForward
		//Debug.DrawLine(transform.position, transform.position + netForward, Color.red, 3.0f);
		//Debug.DrawLine(transform.position, player.transform.position, Color.green);
		//Debug.DrawLine(transform.position + netForward, player.transform.position, Color.blue);

		//Vector3 blueLine = player.transform.position - (transform.position + netForward);
		//Vector3 greenLine = player.transform.position - (transform.position);

		//Debug.DrawLine(player.transform.position, Vector3.Cross(blueLine, greenLine), Color.cyan);
		////transform.rotat(transform.up, .1f);
		////transform.LookAt(player.transform, Vector3.up);*/
		#endregion
	}

	void CalcSteeringForce()
	{
		steeringForce = Vector3.zero;
		Vector3 tempTargPos = curTarg.transform.position + Vector3.up * distAbovePlayer;
		//Debug.DrawLine(transform.position, tempTargPos, Color.white);
		steeringForce += Seek(tempTargPos);
	}

	void ClampSteeringForce()
	{
		if (steeringForce.magnitude > maxForce)
		{
			steeringForce.Normalize();
			steeringForce *= maxForce;
		}
	}


	void FireShotgun()
	{

	}

	void FireStream()
	{

	}
	
	void FireHoming()
	{

	}

	void FireLaser()
	{

	}

	// improve this so we only do it once
	public float Radius
	{
		get
		{
			float x = renderer.bounds.extents.x;
			float z = renderer.bounds.extents.z;
			return Mathf.Sqrt(x * x + z * z);
		}
	}

	public Vector3 Seek(Vector3 pos)
	{
		// find dv, the desired velocity
		Vector3 dv = pos - transform.position;
		dv.y = 0; //only steer in the x/z plane
		dv = dv.normalized * maxSpeed; //scale by maxSpeed
		//Debug.DrawLine(transform.position, dv, Color.cyan);
		dv -= transform.forward * speed; //subtract velocity to get vector in that direction
		//Debug.Log(dv);
		//Debug.DrawLine(transform.position, dv, Color.black);
		return dv;
	}

	// same as seek pos above, but parameter is game object
	public Vector3 Seek(GameObject gO)
	{
		return Seek(gO.transform.position);
	}

	public Vector3 Flee(Vector3 pos)
	{
		Vector3 dv = transform.position - pos;//opposite direction from seek 
		dv.y = 0;
		dv = dv.normalized * maxSpeed;
		dv -= transform.forward * speed;
		return dv;
	}

	public Vector3 Flee(GameObject go)
	{
		Vector3 targetPos = go.transform.position;
		targetPos.y = transform.position.y;
		Vector3 dv = transform.position - targetPos;
		dv = dv.normalized * maxSpeed;
		return dv - transform.forward * speed;
	}

	public Vector3 AlignTo(Vector3 direction)
	{
		// useful for aligning with flock direction
		Vector3 dv = direction.normalized;
		return dv * maxSpeed - transform.forward * speed;
	}

	public Vector3 Arrival()
	{
		Vector3 dv = Vector3.zero;
		//vector3direction.normalized;



		return dv;
	}

	private void Arrival(float percentDecline)
	{
		steeringForce *= (maxForce * percentDecline);
	}

	////Assumtions:
	//// we can access radius of obstacle
	//// we have CharacterController component
	//public Vector3 AvoidObstacle(GameObject obst, float safeDistance)
	//{
	//    Vector3 dv = Vector3.zero;
	//    //compute a vector from charactor to center of obstacle
	//    Vector3 vecToCenter = obst.transform.position - transform.position;
	//    //eliminate y component so we have a 2D vector in the x, z plane
	//    vecToCenter.y = 0;
	//    float dist = vecToCenter.magnitude;

	//    //return zero vector if too far to worry about
	//    if (dist > safeDistance + obst.GetComponent<Dimensions>().Radius + GetComponent<Dimensions>().Radius)
	//        return dv;

	//    //return zero vector if behind us
	//    if (Vector3.Dot(vecToCenter, transform.forward) < 0)
	//        return dv;

	//    //return zero vector if we can pass safely
	//    float rightDotVTC = Vector3.Dot(vecToCenter, transform.right);
	//    if (Mathf.Abs(rightDotVTC) > obst.GetComponent<Dimensions>().Radius + Radius)
	//        return dv;

	//    //obstacle on right so we steer to left
	//    if (rightDotVTC > 0)
	//        dv = transform.right * -maxSpeed * safeDistance / dist;
	//    else
	//        //obstacle on left so we steer to right
	//        dv = transform.right * maxSpeed * safeDistance / dist;

	//    //stay in x/z plane
	//    dv.y = 0;

	//    //compute the force
	//    dv -= transform.forward * speed;
	//    renderer.material.color = Color.yellow;
	//    return dv;
	//}
}

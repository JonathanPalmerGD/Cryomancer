using UnityEngine;
using System.Collections;

public class SerpentBody : MonoBehaviour 
{
	public GameObject target = null;
	private RailNode nearestNode;
	private RailNode nextNode;
	public float seekWt = 10.0f;
	private CharacterController controller = null;
	private Steering steering = null;
	private Vector3 moveDirection;
	private Vector3 steeringForce;
	private Vector3 startPos;
	public bool changingDest = false;
	
	void Start () 
	{
		controller = gameObject.GetComponent<CharacterController>();
		steering = gameObject.GetComponent<Steering>();
		moveDirection = transform.forward;
		startPos = transform.position;
	}

	private void ClampSteering()
	{
		if (steeringForce.magnitude > steering.maxForce)
		{
			steeringForce.Normalize();
			steeringForce *= steering.maxForce;
		}
	}

	private void CalcSteeringForce()
	{
		steeringForce = Vector3.zero;
		if (target != null)
		{
			Debug.DrawLine(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), Color.cyan, .2f);
			steeringForce += seekWt * steering.Seek(target.transform.position);
		}
	}

	private void CheckArrive()
	{
		if (Vector3.Distance(new Vector3(transform.position.x, target.transform.position.y, transform.position.z), target.transform.position) < 3)
		{
			if (target.GetComponent<RailNode>() != null)
			{
				RailNode dest = target.GetComponent<RailNode>();

				int r = Random.Range(0, dest.adjacentNodes.Count);
				Debug.Log(r + "\n");
				target = dest.adjacentNodes[r].gameObject;
			}
		}
	}

	private void CheckDistanceToHead()
	{
		if (Vector3.Distance(transform.position, target.transform.position) < 8)
		{
			steering.maxSpeed = Vector3.Distance(transform.position, target.transform.position) / 2 * 15;
		}
	}

	void Update () 
	{
		CalcSteeringForce();
		ClampSteering();

		moveDirection = transform.forward * steering.Speed;

		moveDirection += steeringForce * Time.deltaTime;

		steering.Speed = moveDirection.magnitude;

		if (steering.Speed != moveDirection.magnitude)
		{
			moveDirection = moveDirection.normalized * steering.Speed;
		}
		if (moveDirection != Vector3.zero)
		{
			transform.forward = moveDirection;
		}

		controller.Move(moveDirection * Time.deltaTime);

		if (changingDest)
		{
			CheckArrive();
		}
	}
}

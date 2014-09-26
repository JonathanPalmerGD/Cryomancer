using UnityEngine;
using System.Collections;

public class FallDetect : MonoBehaviour
{
	private Ray playerRay;
	public bool rayCast;
	public RaycastHit hitInfo;
	public float jumpRayHeight = 1.0f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// Cast a sphere downward to see if it hits anything. This should reset the player's jumps even on edges/inclines.
		playerRay = new Ray(transform.position, -1 * transform.up);

		//Raycast down
		rayCast = Physics.SphereCast(playerRay, 0.5f, out hitInfo, jumpRayHeight);
		//Debug.Log(rayCast.ToString());
		//If we raycast with something that has a jumppad
		if (rayCast && hitInfo.collider.gameObject.tag == "Fallpad")
		{
			hitInfo.collider.gameObject.rigidbody.useGravity = true;
		}
	}
}

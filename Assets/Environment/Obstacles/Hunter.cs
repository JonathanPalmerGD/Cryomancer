using UnityEngine;
using System.Collections;

public class Hunter : MonoBehaviour 
{
	public GameObject targetObject;
	private GameObject player;
	public float playerRadius = 0.3f;
	public float collisionRadius = 3.2f;
	public float approachSpeed = 240f;
	public bool grounded = false;
	private Vector3 initialPosition;

	//Hunter does both Follow and Teleport. It will send the player back to checkpoints AND it will follow the player in 3D space.

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		initialPosition = transform.position;
		if (grounded)
		{
			collider.enabled = true;
			collisionRadius += 0.2f;
		}
		else
		{
			Destroy(rigidbody);
			collider.enabled = false;
		}
	}

	void Update()
	{
		Vector3 temp = Vector3.zero;
		
		temp.x = (transform.position.x - player.transform.position.x) / approachSpeed;
		if (!grounded)
		{
			temp.y = (transform.position.y - player.transform.position.y) / approachSpeed;
		}
		temp.z = (transform.position.z - player.transform.position.z) / approachSpeed;

		transform.position -= temp;


		CharacterController controller = player.GetComponent<CharacterController>();

		float distanceBetween = Vector3.Distance(player.transform.position, transform.position);

		//If we have the controller, go based on that
		if (controller != null)
		{
			if ((collisionRadius + controller.radius) > distanceBetween)
			{
				if (player.GetComponent<TeleTarget>().teleTarget != null)
				{
					player.transform.position = player.GetComponent<TeleTarget>().teleTarget.transform.position;
					player.transform.rotation = player.GetComponent<TeleTarget>().teleTarget.transform.rotation;
					transform.position = initialPosition;
				}
			}
		}
		//If we don't have the controller, hope we've preset a distance.
		else
		{
			if ((collisionRadius + playerRadius) > distanceBetween)
			{
				if (player.GetComponent<TeleTarget>().teleTarget != null)
				{
					player.transform.position = player.GetComponent<TeleTarget>().teleTarget.transform.position;
					player.transform.rotation = player.GetComponent<TeleTarget>().teleTarget.transform.rotation;
					transform.position = initialPosition;
				}
			}
		}
	}
}

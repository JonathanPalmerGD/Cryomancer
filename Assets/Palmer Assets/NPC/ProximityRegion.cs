using UnityEngine;
using System.Collections;

public class ProximityRegion : MonoBehaviour 
{
	private GameObject player;
	public FollowingEntity fairy;
	public GameObject fairyIdleGameObject;
	public bool playerInRange = false;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		fairy.target = fairyIdleGameObject;
	}

	// Update is called once per frame
	void Update ()
	{
		if (fairy.target != null)
		{
			//Debug.Log(fairy.target.name);
		}
	}

	/// <summary>
	/// When they enter the volume, tell the fairy to target the player.
	/// </summary>
	/// <param name="collider"></param>
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			//The fairy will always move towards its target and circle around it
			playerInRange = true;
			fairy.target = player;
			fairy.rigidbody.velocity = Vector3.zero;
		}
	}

	void OnTriggerExit(Collider collider) 
	{
		if (collider.tag == "Player")
		{
			//Tell the fairy to go home
			playerInRange = false;
			fairy.target = fairyIdleGameObject;
			fairy.rigidbody.velocity = Vector3.zero;
		}
	}
}

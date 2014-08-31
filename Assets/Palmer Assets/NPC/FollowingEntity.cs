using UnityEngine;
using System.Collections;

public class FollowingEntity : MonoBehaviour 
{
	public GameObject target;
	public GameObject visualBody;
	private GameObject player;
	public float approachSpeed;
	public float slowDist;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () 
	{
		if (target != null)
		{
			Vector3 dirToTarget = target.transform.position - transform.position;
			if (dirToTarget.sqrMagnitude > .5)
			{
				if (target.tag == "Player")
				{
					rigidbody.velocity = dirToTarget.normalized * approachSpeed * 10 * dirToTarget.magnitude / slowDist * Time.deltaTime;
				}
				else
				{
					rigidbody.velocity = dirToTarget.normalized * approachSpeed * 10 * Time.deltaTime;
				}
			}
			else
			{
				rigidbody.velocity = Vector3.zero;
			}
		}
		else
		{
			rigidbody.velocity = Vector3.zero;
		}
	}
}

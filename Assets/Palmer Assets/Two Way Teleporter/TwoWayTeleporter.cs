using UnityEngine;
using System.Collections;

public class TwoWayTeleporter : MonoBehaviour 
{
	private GameObject player;
	private TwoWayTeleporter target;
	public GameObject exit;
	public bool armed = true;
	public int counter = 0;
	public int reloadTime = 300;
	public float areaOfEffect = 3.0f;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if (exit != null)
		{
			target = exit.GetComponent<TwoWayTeleporter>();
		}
	}
	// Update is called once per frame
	void Update()
	{
		if (target != null)
		{
			if (armed)
			{
				float distanceBetween = Vector3.Distance(transform.position, player.transform.position);
				if (areaOfEffect > distanceBetween)
				{
					target.armed = false;
					target.counter = 0;
					player.transform.position = exit.transform.position;
				}
			}
			else
			{
				counter = counter + 1;
				if (counter > reloadTime)
				{
					armed = true;
					counter = 0;
				}
			}
		}
	}
}

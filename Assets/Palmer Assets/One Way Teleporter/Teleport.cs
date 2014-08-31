using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
	public GameObject targetObject;
	public bool staticTarget;
	private GameObject player;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () 
	{
		float distanceBetween = Vector3.Distance(player.transform.position, transform.position);

		if ((transform.localScale.x) > distanceBetween)
		{
			//This sends the player to 1 static target.
			if (staticTarget)
			{
				if (targetObject != null)
				{
					player.transform.position = targetObject.transform.position;
					player.transform.rotation = targetObject.transform.rotation;
				}
			}
			//This sends the player to their last checkpoint
			else
			{
				if (player.GetComponent<TeleTarget>().teleTarget != null)
				{
				
					player.transform.position = player.GetComponent<TeleTarget>().teleTarget.transform.position;
					player.transform.rotation = player.GetComponent<TeleTarget>().teleTarget.transform.rotation;
				}
			}
		}
	}
}

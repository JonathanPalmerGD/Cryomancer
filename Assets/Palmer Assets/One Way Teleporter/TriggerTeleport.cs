using UnityEngine;
using System.Collections;

public class TriggerTeleport : MonoBehaviour 
{
	public GameObject targetObject;
	public bool staticTarget;
	private GameObject player;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	/// <summary>
	/// A trigger based player relocator. It will send the player to a static target or a checkpoint. Multi-usable for specific enemies or 'death' regions
	/// </summary>
	/// <param name="collider"></param>
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			//This sends the player to 1 static target.
			if (staticTarget)
			{
				if (targetObject != null)
				{
					player.transform.position = targetObject.transform.position;
					player.transform.rotation = targetObject.transform.rotation;
					CharacterMotor charMotor = player.GetComponent<CharacterMotor>();
					charMotor.SetVelocity(new Vector3(0, 0, 0));
				}
			}
			//This sends the player to their last checkpoint
			else
			{
				if (player.GetComponent<TeleTarget>().teleTarget != null)
				{

					player.transform.position = player.GetComponent<TeleTarget>().teleTarget.transform.position;
					player.transform.rotation = player.GetComponent<TeleTarget>().teleTarget.transform.rotation;
					CharacterMotor charMotor = player.GetComponent<CharacterMotor>();
					charMotor.SetVelocity(new Vector3(0, 0, 0));
				}
			}
		}
	}

	void Update () 
	{
	}
}

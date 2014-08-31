using UnityEngine;
using System.Collections;

public class CheckpointTrigger : MonoBehaviour
{
	//Don't forget to face the checkpoint

	// Use this for initialization
	void Start () 
	{
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			collider.gameObject.GetComponent<TeleTarget>().teleTarget = this.gameObject;
		}
	}

	// Update is called once per frame
	void Update () 
	{
	}
}

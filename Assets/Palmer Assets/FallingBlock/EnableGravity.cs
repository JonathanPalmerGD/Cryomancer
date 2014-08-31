using UnityEngine;
using System.Collections;

public class EnableGravity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c)
	{
		//If when we hit something and it has a rigid body
		if (c.gameObject.rigidbody != null)
		{
			//Say that thing now respects gravity.
			c.gameObject.rigidbody.useGravity = true;
		}
	}
}

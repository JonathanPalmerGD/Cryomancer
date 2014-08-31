using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

	public GameObject bomb;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			bomb.GetComponent<Detonator>().Explode();
		}
	}
}

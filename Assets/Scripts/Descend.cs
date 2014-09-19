using UnityEngine;
using System.Collections;

public class Descend : MonoBehaviour 
{
	public Vector3 descendRate = new Vector3(0, 3f, 0);

	// Update is called once per frame
	void Update ()
	{
		Debug.Log("Old: " + transform.position + "\nNew: " + (transform.position - (descendRate * Time.deltaTime)));
		transform.position = transform.position - (descendRate * Time.deltaTime);
	}
}

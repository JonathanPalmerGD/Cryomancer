using UnityEngine;
using System.Collections;

public class Miner : MonoBehaviour 
{
	public Camera cam;
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Ray ray = new Ray(transform.position, cam.transform.forward);
			Debug.DrawLine(transform.position, transform.position + cam.transform.forward * 3, Color.blue, .5f);
			//Debug.DrawLine(ray.origin, ray.origin + ray.direction, Color.green, 3.0f);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit, 3.0f))
			{
				Debug.Log(hit.collider.name + " \n");
			}
		}
	}
}

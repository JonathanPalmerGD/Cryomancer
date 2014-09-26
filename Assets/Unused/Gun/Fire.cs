using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour 
{
	public GameObject gun;
	public GameObject firePoint;
	public GameObject bullet;
	public GameObject playerCamera;
	public float bulletSpeed = 100.0f;
	public float bulletLife = 4.0f;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.F))
		{
			GameObject newBullet = (GameObject)Object.Instantiate(bullet, firePoint.transform.position, new Quaternion(0, 0, 0, 0));
			Vector3 newForce = Vector3.zero;
			
			newForce = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
			newForce.Normalize();
			newBullet.rigidbody.AddForce(newForce * bulletSpeed);

			GameObject.Destroy(newBullet, bulletLife);
		}
	}
}

using UnityEngine;
using System.Collections;

public class AutoFire : MonoBehaviour 
{
	public GameObject streamProjectile;
	public GameObject nose;
	public GameObject head;
	public float streamVelocity = 110.0f;
	public float counter = 0;
	public float waitDuration = 1.0f;
	public float streamBulletLifeTime = 4f;
	public Color[] colorOfSignal;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		counter += Time.deltaTime;
		if (counter > waitDuration - 0.5f)
		{
			nose.GetComponent<Renderer>().material.color = colorOfSignal[1];
			head.GetComponent<Renderer>().material.color = colorOfSignal[1];
		}
		else
		{
			nose.GetComponent<Renderer>().material.color = colorOfSignal[0];
			head.GetComponent<Renderer>().material.color = colorOfSignal[0];
		}
		if (counter > waitDuration)
		{
			counter -= waitDuration;

			GameObject bullet = (GameObject)Instantiate(streamProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion());

			bullet.GetComponent<EvilToken>().damage[0] = 0;
			bullet.GetComponent<EvilToken>().damage[1] = 0;
			bullet.GetComponent<EvilToken>().damage[2] = 0;

			bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 10 * streamVelocity * bullet.GetComponent<Rigidbody>().mass);

			Destroy(bullet, streamBulletLifeTime);
		}
	}
}

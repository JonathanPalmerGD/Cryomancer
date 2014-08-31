using UnityEngine;
using System.Collections;

public class CollisionBreak : MonoBehaviour 
{
	public Transform explosionPrefab;

	// Use this for initialization
	void Start()
	{
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.contacts.Length);
		ContactPoint contact = collision.contacts[0];
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);

		Vector3 pos = contact.point;
		if (explosionPrefab != null)
		{
			GameObject.Instantiate(explosionPrefab, pos, rot);
		}
		Destroy(gameObject);
	}



	// Update is called once per frame
	void Update()
	{

	}

}
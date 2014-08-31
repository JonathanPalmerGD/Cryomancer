using UnityEngine;
using System.Collections;

public class IceRunner : MonoBehaviour 
{
	public GameObject blockPrefab;
	public bool blocksMelt = true;
	public float lifeTime = 3.0f;

	// Use this for initialization
	void Start () 
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.E))	//Incline block
		{
			//trans.pos - new v3 = (x - 0, y - 1, z - 0)
			Vector3 blockPosition = transform.position - new Vector3(0, 1.1f, 0);

			if(blocksMelt)
			{
				//Create a new object, that we will destroy after 3 seconds.
				Destroy(Instantiate(blockPrefab, blockPosition, new Quaternion()), lifeTime);
			}
			else
			{
				//Create a new object, that we will NEVER destroy.
				Instantiate(blockPrefab, blockPosition, new Quaternion());
			}
		}
		if (Input.GetKey(KeyCode.Q))	//Decline block
		{
			//trans.pos - new v3 = (x - 0, y - 1, z - 0)
			Vector3 blockPosition = transform.position - new Vector3(0, 1.35f, 0);

			//Create a new object, that we will destroy after 3 seconds.
			Destroy(Instantiate(blockPrefab, blockPosition, new Quaternion()), lifeTime);

			if(blocksMelt)
			{
				//Create a new object, that we will destroy after 3 seconds.
				Destroy(Instantiate(blockPrefab, blockPosition, new Quaternion()), lifeTime);
			}
			else
			{
				//Create a new object, that we will NEVER destroy.
				Instantiate(blockPrefab, blockPosition, new Quaternion());
			}
		}
	}
}
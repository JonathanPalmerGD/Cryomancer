using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	//What we will spawn every X seconds
	public GameObject spawnPrefab;
	//Our time counter.
	public float counter = 0.0f;
	//How long our object will live
	public float objectLifeTime = 25.0f;
	//If we are spawning things currently.
	public bool online = true;
	public bool randomSpawning = false;
	//The bounds for random or not
	public float spawnMin = 4.0f;
	public float spawnMax = 8.0f;
	public float curSpawnTime = 5.0f;

	// Use this for initialization
	void Start () 
	{
		//If we spawn randomly, get a rand.
		if (randomSpawning)
		{
			curSpawnTime = Random.Range(spawnMin, spawnMax);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If we are online, as in if we are spawning currently
		if (online)
		{
			//Counter up
			counter += Time.deltaTime;

			//If we exceed spawn time
			if (counter > curSpawnTime)
			{
				if (randomSpawning)
				{
					curSpawnTime = Random.Range(spawnMin, spawnMax);
				}
				//Make a new object at our position which will die after X seconds
				Destroy(Instantiate(spawnPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), new Quaternion()), objectLifeTime);
				//Reset our counter.
				counter = 0.0f;
			}
		}
	}
}

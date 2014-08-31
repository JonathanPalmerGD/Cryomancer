using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chunk : MonoBehaviour 
{
	//Adjacent 24 chunks
	//public List<Chunk> adjacents;
	public Dictionary<Vector3, Chunk> adjacents;
	public int child = 0;

	public void Start()
	{
		//ChildChunks(0);
	}

	public void ChildChunks(int generation)
	{
		child = generation;

		if (child < 1)
		{
			int count = 0;
			//Create children in all directions.
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					for (int k = -1; k < 2; k++)
					{
						count++;
						Vector3 direction = new Vector3(i, j, k);

						GameObject newChunk = new GameObject("New Chunk (" + i + "," + j + "," + k + ")");
						newChunk.AddComponent<Chunk>();
						#region Count
						if (direction == Vector3.zero)
						{
							Debug.Log("Zero!\n" + count);
						}
						else
						{
							Debug.Log("Count: " + count + "\t\tDirection: " + direction.ToString() + "\n");
						}
						#endregion
					}
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkManager : Singleton<MonoBehaviour> 
{
	//public Dictionary<int, Chunk> chunks;
	//public Chunk chunkRoot;
	public Dictionary<int, Chunk> chunks;
	public const int CHUNKSIZE = 16;
	//public Chunk[,,] chunks;

	public void TrackChunk(int ID, Chunk newChunk)
	{
		Debug.Log("Tracking Chunk: " + ID + "\nPosition: " + newChunk.gameObject.transform.position + "\n");
		chunks.Add(ID, newChunk);
	}

	void Start ()
	{
		GameObject newRoot = new GameObject("Root Chunk (" + 000 + "," + 000 + "," + 000 + ")");
		Chunk newChunk = newRoot.AddComponent<Chunk>();
		newRoot.transform.position = new Vector3(0, 0, 0);
		chunks.Add(000000000, newChunk);
	}

	public void AddChunks(int x, int y, int z)
	{
		GameObject newRoot = new GameObject("Root Chunk (" + x + "," + y + "," + z + ")");
		Chunk newChunk = newRoot.AddComponent<Chunk>();
		newRoot.transform.position = new Vector3(x * CHUNKSIZE, y * CHUNKSIZE, z * CHUNKSIZE);
		chunks.Add(IntToID(x, y, z), newChunk);
	}

	public int IntToID(int x, int y, int z)
	{
		int formatted = 0;

		//x.ToString() + y.ToString() + z.ToString()

		return formatted;
	}
}
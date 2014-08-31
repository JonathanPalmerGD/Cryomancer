using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : Singleton<MonoBehaviour> 
{
	public Dictionary<int, GameObject> vox;
	public int endX = 10;
	public int endY = 10;
	public int endZ = 10;
	//public Chunk chunkRoot;
	GameObject voxelPrefab;
	List<string> fileNames;

	void Start ()
	{
		#region Texture File Names
		fileNames = new List<string>();
		fileNames.Add("houseBeigeAlt");
		fileNames.Add("houseBeigeAlt2");
		fileNames.Add("houseDarkAlt");
		fileNames.Add("houseDarkAlt2");
		fileNames.Add("houseGrayAlt");
		fileNames.Add("houseGrayAlt2");
		fileNames.Add("roofGreyMid");
		fileNames.Add("roofGreyTopMid");
		fileNames.Add("roofRedMid");
		fileNames.Add("roofRedTopMid");
		fileNames.Add("roofYellowMid");
		fileNames.Add("roofYellowTopMid");
		#endregion

		vox = new Dictionary<int, GameObject>();
		voxelPrefab = Resources.Load<GameObject>("VoxelPrefab");

		//Create Voxels
		/*for (int i = 0; i < endX; i++)
		{
			for (int j = 0; j < endY; j++)
			{
				for (int k = 0; k < endZ; k++)
				{
					CreateVoxel(-i, -j, -k);
				}
			}
		}*/
	}

	public void CreateVoxel(int x, int y, int z)
	{
		GameObject newVox = (GameObject)Instantiate(voxelPrefab);
		newVox.transform.position = new Vector3(x, y, z);
		int randTex = (int)Random.Range(0, fileNames.Count);
		newVox.renderer.material.mainTexture = Resources.Load<Texture>(fileNames[randTex]);
		newVox.name = fileNames[randTex];
	}

	void Update() 
	{
		
	}
}

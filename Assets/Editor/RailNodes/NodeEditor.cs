using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class NodeEditor : EditorWindow 
{
	const int versionNum = 0;

	[MenuItem("Architect/Create Nodes #%N")]
	static void HotKeyQuickFolder()
	{
		GameObject parentOfNodes = GameObject.Find("RailNodes");

		if (parentOfNodes == null)
		{
			parentOfNodes = new GameObject();
			parentOfNodes.name = "RailNodes";
		}
		
		//Make a bunch of rail nodes.
		//Set their name

		GameObject floorObject = GameObject.Find("DragonFloor");

		//Debug.Log("X: " + (int)(floorObject.transform.localScale.x / 10) + "\nZ: " + (int)(floorObject.transform.localScale.z / 10));

		int xVal = (int)(floorObject.transform.localScale.x / 10);
		int zVal = (int)(floorObject.transform.localScale.z / 10);

		GameObject[,] nodes = new GameObject[xVal - 1, zVal - 1];
		//Debug.Log(nodes.Length + "\n");
		int counter = 0;
		if (floorObject != null)
		{
			GameObject parent;
			for (int i = 1; i < xVal - 1; i++)
			{
				parent = new GameObject("Row X - " + i);
				parent.transform.parent = parentOfNodes.transform;
				for (int j = 1; j < zVal - 1; j++)
				{
					nodes[i, j] = new GameObject("RailNode (" + i + "," + j + ")");
					nodes[i, j].transform.position = new Vector3(
							i * 10 - floorObject.transform.localScale.x / 2,
							40, 
							j * 10 - floorObject.transform.localScale.z / 2);
					nodes[i, j].transform.parent = parent.transform;

					RailNode rn = nodes[i, j].AddComponent<RailNode>();
					rn.id = counter;

					counter++;
				}
			}
			//Debug.Log(counter + "\n");

			for (int i = 0; i < xVal - 1; i++)
			{
				for (int j = 0; j < zVal - 1; j++)
				{
					RailNode rn = nodes[i, j].GetComponent<RailNode>();

					// If not at the left edge
					if (nodes[i, j + 1] != null)
						rn.adjacentNodes[3] = nodes[i, j + 1].GetComponent<RailNode>();

					if (nodes[i, j - 1] != null)
						rn.adjacentNodes[1] = nodes[i, j - 1].GetComponent<RailNode>();

					if (nodes[i + 1, j] != null)
						rn.adjacentNodes[0] = nodes[i + 1, j].GetComponent<RailNode>();

					if (nodes[i - 1, j] != null)
						rn.adjacentNodes[0] = nodes[i - 1, j].GetComponent<RailNode>();
				}
			}
		}
		

		/*
		//Make a new game object, set the name and set the parent
		GameObject newFolder = new GameObject();
		newFolder.name = qFolderName;
		if (sharedParent != null)
		{
			newFolder.transform.parent = sharedParent.transform;
		}

		for (int i = 0; i < Selection.gameObjects.Length; i++)
		{
			Selection.gameObjects[i].transform.parent = newFolder.transform;
		}

		//have the user select the new folder
		Selection.activeGameObject = newFolder;

		//Print out the name just in case they click away.
		message = "[Quickfolder] - Success\n" + "Folder Name: " + qFolderName;*/
	}
}

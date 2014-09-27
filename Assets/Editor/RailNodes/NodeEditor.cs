using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class NodeEditor : EditorWindow 
{
	const int versionNum = 0;

	[MenuItem("GameObject/QuickFolderHotkey #%N")]
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

		GameObject[,] nodes = new GameObject[(int)(floorObject.transform.localScale.x / 10), (int)(floorObject.transform.localScale.y / 10)];

		if (floorObject != null)
		{
			for (int i = 0; i < (int)(floorObject.transform.localScale.x / 10); i++)
			{
				for (int j = 0; j < (int)(floorObject.transform.localScale.z / 10); j++)
				{
					nodes[i, j].name = "RailNode (" + i + "," + j + ")";
					nodes[i, j].transform.parent = parentOfNodes.transform;
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

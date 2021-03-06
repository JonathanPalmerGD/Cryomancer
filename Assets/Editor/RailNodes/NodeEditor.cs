﻿using UnityEngine;
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

			for (int i = 1; i < xVal - 1; i++)
			{
				for (int j = 1; j < zVal - 1; j++)
				{
					RailNode rn = nodes[i, j].GetComponent<RailNode>();

					rn.adjacentNodes = new List<RailNode>();

					// If not at the left edge
					if (i + 1 < xVal - 1 && nodes[i + 1, j] != null)
					{
						rn.adjacentNodes.Add(nodes[i + 1, j].GetComponent<RailNode>());
					}
					if (j > 0 && nodes[i, j - 1] != null)
					{
						rn.adjacentNodes.Add(nodes[i, j - 1].GetComponent<RailNode>());
					}
					if (i > 0 && nodes[i - 1, j] != null)
					{
						rn.adjacentNodes.Add(nodes[i - 1, j].GetComponent<RailNode>());
					}
					if (j + 1 < zVal - 1 && nodes[i, j + 1] != null)
					{
						rn.adjacentNodes.Add(nodes[i, j + 1].GetComponent<RailNode>());
					}
				}
			}
		}
	}
}

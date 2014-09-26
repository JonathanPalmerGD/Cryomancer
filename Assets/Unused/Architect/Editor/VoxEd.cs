using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(Voxel))]
public class VoxEd : Editor 
{
	static bool displaying;
	[MenuItem("Architect/Display Voxel Control %T")]
	static void DisplayVoxEd()
	{
		Tools.hidden = !Tools.hidden;
		displaying = Tools.hidden;
	}

	GameObject targ;
	void OnSceneGUI()
	{
		targ = Selection.activeGameObject;
		
		if(displaying)
		{
			if (targ != null)
			{
				List<Vector3> corners = new List<Vector3>();

				Vector3 pos = targ.transform.position;
				Vector3 scale = targ.transform.localScale;

				Handles.color = new Color(.9f, .4f, .4f, 1f);
				if (CheckLegalDirection(new Vector3(pos.x + scale.x, pos.y, pos.z)))
				{
					DrawHandleButton(new Vector3(pos.x + scale.x, pos.y, pos.z));
				}

				Handles.color = new Color(.9f, .4f, .4f, 1f);
				if (CheckLegalDirection(new Vector3(pos.x - scale.x, pos.y, pos.z)))
				{
					DrawHandleButton(new Vector3(pos.x - scale.x, pos.y, pos.z));
				}

				Handles.color = new Color(.4f, .9f, .4f, 1f);
				if (CheckLegalDirection(new Vector3(pos.x, pos.y + scale.y, pos.z)))
				{
					DrawHandleButton(new Vector3(pos.x, pos.y + scale.y, pos.z));
				}

				Handles.color = new Color(.4f, .9f, .4f, 1f);
				if (CheckLegalDirection(new Vector3(pos.x, pos.y - scale.y, pos.z)))
				{
					DrawHandleButton(new Vector3(pos.x, pos.y - scale.y, pos.z));
				}
				
				Handles.color = new Color(.4f, .4f, .9f, 1f);
				if (CheckLegalDirection(new Vector3(pos.x, pos.y, pos.z + scale.z)))
				{
					DrawHandleButton(new Vector3(pos.x, pos.y, pos.z + scale.z));
				}

				Handles.color = new Color(.4f, .4f, .9f, 1f);
				if (CheckLegalDirection(new Vector3(pos.x, pos.y, pos.z - scale.z)))
				{
					DrawHandleButton(new Vector3(pos.x, pos.y, pos.z - scale.z));
				}
			}
		}
	}

	bool CheckLegalDirection(Vector3 newPos)
	{
		Ray testSpace = new Ray(Selection.activeGameObject.transform.position, newPos - Selection.activeGameObject.transform.position);
		int layerMask = 1 << 8;
		bool hit = Physics.Raycast(testSpace, Vector3.Distance(Selection.activeGameObject.transform.position, newPos), layerMask);

		if (hit)
		{
			return false;
		}
		return true;
	}

	void DrawHandleButton(Vector3 newPos)
	{
		if (Handles.Button(newPos, targ.transform.rotation, 1f, .5f, Handles.CubeCap))
		{
			if (CheckLegalDirection(newPos))
			{
				GameObject newGo = (GameObject)GameObject.Instantiate(targ);
				if (targ.transform.parent == null)
				{
					GameObject newParent = new GameObject("Voxel Folder");
					newParent.tag = "VoxelParent";
					targ.transform.parent = newParent.transform;
				}
				newGo.transform.parent = targ.transform.parent;
				newGo.transform.position = newPos;

				newGo.layer = 8;
				newGo.name = targ.name;
				Selection.activeGameObject = newGo;
			}
		}
	}

	[MenuItem("Architect/Merge Voxels &%T")]
	static void MergeVoxels()
	{
		List<GameObject> voxelParents = GameObject.FindGameObjectsWithTag("VoxelParent").ToList();
		
		//Make a list of all the voxels
		List<GameObject> voxels = GameObject.FindGameObjectsWithTag("Voxel").ToList();
		
		for(int i = 0; i < voxelParents.Count; i++)
		{
			//Make a list for current parent
			List<GameObject> childVoxels = new List<GameObject>();

			#region Populate the list with parent's children
			for (int j = 0; j < voxels.Count; j++)
			{
				if (voxels[j].transform.parent != null)
				{
					if (voxels[j].transform.parent.gameObject == voxelParents[i])
					{
						childVoxels.Add(voxels[j]);
					}
				}
			}
			#endregion

			#region Create sortedChildren list for child voxels sorting by X/Y/Z
			List<GameObject> sortedChildren = childVoxels.OrderBy(v => v.transform.position.y).OrderBy(v => v.transform.position.z).OrderBy(v => v.transform.position.x).ToList();
			#endregion

			//For each child [Sorted]
			for (int j = 0; j < sortedChildren.Count; j++)
			{
				//Debug.Log("[Sorted] j: " + j + "  " + sortedChildren[j].name + " : " + sortedChildren[j].GetInstanceID().ToString() + "\n");

				if (j + 1 < sortedChildren.Count)
				{
					float diffX = sortedChildren[j].transform.position.x - sortedChildren[j + 1].transform.position.x;
					if (diffX < .01f && diffX > -.01f)
					{
						Debug.Log("[X Compare] " + sortedChildren[j].name + " to " + sortedChildren[j+1].name + " = " + diffX);


						float diffY = sortedChildren[j].transform.position.y - sortedChildren[j + 1].transform.position.y;
						if (diffY < .01f && diffY > -.01f)
						{
							Debug.Log("[Y Compare] " + sortedChildren[j].name + " to " + sortedChildren[j + 1].name + " = " + diffY);

							//Check Y scale
								//Check X scale

									//Check Adjacency?
											//Merge objects
											//Remove the unneeded object
					

						}
					}
				}

			}
			//Sort by X/Y?
			//Merge adjacent ones with similar attributes?
		}
		//Merge it with an adjacent one
		//Start a new thing
			
	}
}

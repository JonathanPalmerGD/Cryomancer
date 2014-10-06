using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(AmbientSource))]
public class AmbientSourceEditor : Editor 
{
	public static AmbientManager manager;
	private string holdName;

	public override void OnInspectorGUI()
	{
		AmbientSource targ = (AmbientSource)target;

		#region How do we know about the manager?
		if (Application.isPlaying)
		{
			manager = AmbientManager.Instance;
		}
		else
		{
			manager = GameObject.Find("AmbientManager").GetComponent<AmbientManager>();
		}
		#endregion

		DrawLocalSounds(targ);

		#region If Manager's ambient audios is null
		if (manager.ambGroups != null)
		{
			if (manager.ambGroups.Count > 0)
			{
				EditorGUILayout.LabelField("Sound Families: " + manager.ambGroups.Count, EdStyles.GetTitleLabel()); 
				GUILayout.Space(4);
				AmbientManagerEditor.DrawAmbients(manager, targ.localSounds);
			}
			if(EdStyles.ToolbarButton("Ambient Sound Manager"))
			{
				Selection.activeGameObject = manager.gameObject;
			}
			GUILayout.Space(4);
		}
		else
		{
			Debug.Log("Ambient Audio is null\n");
			manager.ambGroups = new List<AmbientGroup>();
		}
		#endregion
	}

	public void DrawLocalSounds(AmbientSource targ)
	{
		if (targ.localSounds == null)
		{
			targ.localSounds = new List<string>();
		}

		for(int i = 0; i < targ.localSounds.Count; i++)
		{
			if (targ.localSounds[i] == "Delete")
			{
				GUI.FocusControl("");
				//Remove it and set our index back by one.
				targ.localSounds.RemoveAt(i);
				i--;
			}
			else
			{
				targ.localSounds[i] = EditorGUILayout.TextField("Name", targ.localSounds[i]);
			}
		}

		EditorGUILayout.BeginHorizontal();
		holdName = EditorGUILayout.TextField(holdName);
		
		if (GUILayout.Button("Add Local Sound"))
		{
			targ.localSounds.Add(holdName);
		}
		EditorGUILayout.EndHorizontal();
	}
}
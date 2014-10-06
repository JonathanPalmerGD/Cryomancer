using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(AmbientManager))]
public class AmbientManagerEditor : Editor 
{
	private bool collapsedSources = false;

	public override void OnInspectorGUI()
	{
		AmbientManager targ = (AmbientManager)target;

		if (targ.ambGroups != null)
		{
			if(targ.ambGroups.Count > 0)
			{
				EditorGUILayout.LabelField("Sound Families: " + targ.ambGroups.Count, EdStyles.GetTitleLabel());
				GUILayout.Space(4);
				EditorGUILayout.BeginVertical();
				DrawAmbients(targ);
				EditorGUILayout.EndVertical();
			}
			EditorGUILayout.Space();
			if (GUILayout.Button("Add Ambient Volume"))
			{
				targ.ambGroups.Add(new AmbientGroup());
			}

			DrawAmbientSources(targ);
		}
		else
		{
			Debug.Log("Ambient Audio is null\n");
			targ.ambGroups = new List<AmbientGroup>();
		}
	}

	public void DrawAmbients(AmbientManager targ)
	{
		if (targ.ambGroups != null)
		{
			for (int i = 0; i < targ.ambGroups.Count; i++)
			{
				DrawAmbientSound(i, targ);
			}
		}
	}

	public static void DrawAmbients(AmbientManager targ, List<string> localSounds)
	{
		if (targ.ambGroups != null)
		{
			for (int i = 0; i < targ.ambGroups.Count; i++)
			{
				if (localSounds.Contains(targ.ambGroups[i].name))
				{
					DrawAmbientSound(i, targ);
				}
				else
				{

				}
			}
		}
	}

	public static void DrawAmbientSound(int i, AmbientManager targ)
	{
		EditorGUILayout.BeginVertical();

		if(EdStyles.FoldoutButton(targ.ambGroups[i].name))
		{
			targ.ambGroups[i].expanded = !targ.ambGroups[i].expanded;
		}

		if (targ.ambGroups[i].expanded)
		{
			targ.ambGroups[i].name = EditorGUILayout.TextField("Name", targ.ambGroups[i].name);
			targ.ambGroups[i].volume = EditorGUILayout.Slider("Volume", targ.ambGroups[i].volume, 0, 1);

			DrawClips(i, targ);

			DrawSliders(i, targ);
		}

		EditorGUILayout.EndVertical();

		EditorGUILayout.Space();
	}

	public static void DrawClips(int i, AmbientManager targ)
	{
		EditorGUILayout.BeginVertical("Box");

		if (targ.ambGroups[i].clips != null)
		{
			for (int j = 0; j < targ.ambGroups[i].clips.Count; j++)
			{
				//If there is an empty entry that isn't the last in the list.
				if (j < targ.ambGroups[i].clips.Count - 1 && targ.ambGroups[i].clips[j] == null)
				{
					//Remove it and set our index back by one.
					targ.ambGroups[i].clips.RemoveAt(j);
					j--;
				}
				//Otherwise, draw an objectfield selector.
				else
				{
					targ.ambGroups[i].clips[j] = (AudioClip)EditorGUILayout.ObjectField(targ.ambGroups[i].clips[j], typeof(AudioClip));
				}
			}
		}
		else
		{
			targ.ambGroups[i].clips = new List<AudioClip>();
		}

		//Add new clip
		if (GUILayout.Button("Add Clip"))
		{
			targ.ambGroups[i].clips.Add(new AudioClip());
		}
		EditorGUILayout.EndVertical();
	}

	public static void DrawSliders(int i, AmbientManager targ)
	{
		if (GUILayout.Button("Show Nuts and Bolts", EdStyles.GetFoldout()))
		{
			targ.ambGroups[i].expandedSliders = !targ.ambGroups[i].expandedSliders;
		}

		if (targ.ambGroups[i].expandedSliders)
		{
			targ.ambGroups[i].minFreq = EditorGUILayout.Slider("Min Frequency", targ.ambGroups[i].minFreq, .5f, 30);

			targ.ambGroups[i].playChance = EditorGUILayout.Slider("Play Chance", targ.ambGroups[i].playChance, 0f, 1f);

			targ.ambGroups[i].sleepDuration = EditorGUILayout.Slider("Sleep Duration", targ.ambGroups[i].sleepDuration, -.2f, 30);

			targ.ambGroups[i].sleepCounter = EditorGUILayout.Slider("Sleep Counter", targ.ambGroups[i].sleepCounter, -.2f, 30);

			EditorGUILayout.BeginHorizontal();

			targ.ambGroups[i].sleeping = EditorGUILayout.Toggle("Currently Sleeping", targ.ambGroups[i].sleeping);

			if (GUILayout.Button("Delete"))
			{
				if (EditorUtility.DisplayDialog("Confirm Ambient Group Deletion", "Are you sure you want to delete audio group " + targ.ambGroups[i].name + "?", "Delete", "Cancel"))
				{
					targ.ambGroups.RemoveAt(i);
				}
			}

			EditorGUILayout.EndHorizontal();
		}
		//GUILayout.EndVertical();

		GUILayout.Space(12);
	}

	public void DrawAmbientSources(AmbientManager targ)
	{
		GUILayout.Space(8);
		if (EdStyles.FoldoutButton("Source Objects"))
		{
			collapsedSources = !collapsedSources;
		}

		if (collapsedSources)
		{
			List<GameObject> sourceObject = GameObject.FindGameObjectsWithTag("AmbientSource").ToList();

			for (int i = 0; i < sourceObject.Count; i++)
			{
				if (EdStyles.ToolbarButton(sourceObject[i].name))
				{
					Selection.activeGameObject = sourceObject[i];
				}
			}
		}

		GUILayout.Space(8);
	}
}
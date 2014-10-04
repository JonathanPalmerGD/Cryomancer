using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(AmbientVolume))]
public class AmbientVolumeEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		AmbientVolume myTarget = (AmbientVolume)target;

		myTarget.player = (GameObject)EditorGUILayout.ObjectField(myTarget.player, typeof(GameObject));
		myTarget.mesh = (Mesh)EditorGUILayout.ObjectField(myTarget.mesh, typeof(Mesh));

		if (myTarget.ambAudios != null)
		{
			if(myTarget.ambAudios.Count > 0)
			{
				EditorGUILayout.LabelField("Number of Ambient Sounds: " + myTarget.ambAudios.Count);
				EditorGUILayout.BeginVertical("Box");
				DrawAmbients(myTarget);
				EditorGUILayout.EndVertical();
			}
			if (GUILayout.Button("Add Ambient Volume"))
			{
				myTarget.ambAudios.Add(new AmbientAudio());
			}
		}
		else
		{
			Debug.Log("Ambient Audio is null\n");
			myTarget.ambAudios = new List<AmbientAudio>();
		}
		
		//myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
		// EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
	}

	public void DrawAmbients(AmbientVolume targ)
	{
		if (targ.ambAudios != null)
		{
			for (int i = 0; i < targ.ambAudios.Count; i++)
			{
				EditorGUILayout.BeginVertical("Box");

				EditorGUILayout.LabelField(targ.ambAudios[i].name);
				targ.ambAudios[i].name = EditorGUILayout.TextField("Name", targ.ambAudios[i].name);
				targ.ambAudios[i].volume = EditorGUILayout.Slider("Volume", targ.ambAudios[i].volume, 0, 1);

				DrawClips(i, targ);

				DrawSliders(i, targ);

				EditorGUILayout.EndVertical();
			}
		}
		
	}

	private void DrawClips(int i, AmbientVolume targ)
	{
		EditorGUILayout.BeginVertical("Box");

		if (targ.ambAudios[i].clips != null)
		{
			for (int j = 0; j < targ.ambAudios[i].clips.Count; j++)
			{
				targ.ambAudios[i].clips[j] = (AudioClip)EditorGUILayout.ObjectField(targ.ambAudios[i].clips[j], typeof(AudioClip));
			}
		}
		else
		{
			targ.ambAudios[i].clips = new List<AudioClip>();
		}
		//Add new clip
		if (GUILayout.Button("Add Clip"))
		{
			targ.ambAudios[i].clips.Add(new AudioClip());
		}
		EditorGUILayout.EndVertical();
	}

	private void DrawSliders(int i, AmbientVolume targ)
	{
		targ.ambAudios[i].minFreq = EditorGUILayout.Slider("Min Frequency", targ.ambAudios[i].minFreq, .5f, 30);

		targ.ambAudios[i].playChance = EditorGUILayout.Slider("Play Chance", targ.ambAudios[i].playChance, 0f, 1f);

		targ.ambAudios[i].sleepDuration = EditorGUILayout.Slider("Sleep Duration", targ.ambAudios[i].sleepDuration, -.2f, 30);

		targ.ambAudios[i].sleepCounter = EditorGUILayout.Slider("Sleep Counter", targ.ambAudios[i].sleepCounter, -.2f, 30);

		targ.ambAudios[i].sleeping = EditorGUILayout.Toggle("Currently Sleeping", targ.ambAudios[i].sleeping);
	}
}
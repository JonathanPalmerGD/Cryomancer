using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class Architect : EditorWindow
{
	//GUIStyle title;
	//GUIStyle body;
	//GUIStyle foldButton;
	GUIStyle region;

	#region Variables for Editor
	bool mainGroup = false;
	/// <summary>
	/// The background Color.
	/// </summary>
	Texture2D bg;
	#endregion

	[MenuItem("Architect/Architect Window")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(Architect), false, "Architect", false);
	}


	void Init()
	{
		ArchStyle.SetTextures();

		region = new GUIStyle(GUIStyle.none);
		bg = new Texture2D(128, 128);
		for (int y = 0; y < bg.height; ++y)
		{
			for (int x = 0; x < bg.width; ++x)
			{
				Color color = ArchStyle.BackColor;
				bg.SetPixel(x, y, color);
			}
		}
		bg.Apply();
		region.normal.background = bg;
	}

	void OnGUI()
	{
		#region Initialize
		//title = ArchStyle.GetTitle();

		if (bg == null)
		{
			Init();
		}
		#endregion

		#region Draw
		GUILayout.BeginVertical(region);
		DrawInterface();
		GUILayout.Space(2000);
		GUILayout.EndVertical();

		DrawSceneContents();
		#endregion
	}

	void DrawInterface()
	{
		ArchStyle.DrawTitle("Architect v.01");

		ArchStyle.DrawGuiDivider();

		if (ArchStyle.DrawButton("Re-init", "Initializes various files"))
		{
			Init();
		}

		mainGroup = ArchStyle.DrawDropDown("This is text", mainGroup);
		if (mainGroup)
		{
			GUILayout.BeginVertical(region);
			GUILayout.Space(40);
			if (ArchStyle.DrawButton("This is a button", "tooltip"))
			{
				Debug.Log("Hit\n");
			}

			GUILayout.Space(40);

			GUILayout.EndVertical();
		}
		ArchStyle.DrawGuiDivider();

		GUILayout.Box("Content", region);
	}

	void DrawSceneContents()
	{
		

	}

}

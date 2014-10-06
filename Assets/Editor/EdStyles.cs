using UnityEngine;
using System.Collections;
using UnityEditor;

public static class EdStyles 
{
	public static GUIStyle GetFoldout()
	{
		GUIStyle foldout = new GUIStyle(EditorStyles.toolbarDropDown);
		return foldout;
	}

	public static GUIStyle GetToolbarButton()
	{
		GUIStyle toolbar = new GUIStyle(EditorStyles.toolbarButton);
		return toolbar;
	}

	public static GUIStyle GetTitleLabel()
	{
		GUIStyle title = new GUIStyle(EditorStyles.largeLabel);

		return title;
	}

	public static bool FoldoutButton(string buttonLabel)
	{
		GUILayout.Space(-4);
		if (GUILayout.Button(buttonLabel, EdStyles.GetFoldout()))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public static bool ToolbarButton(string buttonLabel)
	{
		if (GUILayout.Button(buttonLabel, EdStyles.GetToolbarButton()))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public static void DrawTitleLabel(string content)
	{

	}
}

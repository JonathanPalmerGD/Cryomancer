using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class ArchStyle : EditorWindow
{
	public static Color AlphaColor = new Color(.1f, .15f, .7f);
	public static Color BetaColor = new Color(.1f, .9f, .1f);
	public static Color GammaColor = new Color(.65f, .7f, .65f);
	public static Color ControlColor = new Color(.5f, .5f, .5f);
	public static Color BackColor = new Color(.5f, .5f, .55f);

	public static Texture2D bg;
	public static void SetTextures()
	{
		bg = new Texture2D(128, 128);
		for (int y = 0; y < bg.height; ++y)
		{
			for (int x = 0; x < bg.width; ++x)
			{
				Color color = ArchStyle.ControlColor;
				bg.SetPixel(x, y, color);
			}
		}
		bg.Apply();
	}

	public static GUIStyle GetTitle()
	{
		GUIStyle title = new GUIStyle(EditorStyles.boldLabel);

		title.fontSize = 24;
		title.alignment = TextAnchor.UpperRight;
		title.clipping = TextClipping.Overflow;
		if (!EditorGUIUtility.isProSkin)
			title.normal.textColor = AlphaColor;
		return title;
	}

	public static GUIStyle GetSmallFoldoutButton()
	{
		GUIStyle smallFoldoutButton = new GUIStyle(EditorStyles.toolbarButton);
		smallFoldoutButton.fixedHeight = 24f;
		return smallFoldoutButton;
	}

	public static GUIStyle GetSmallLabelIcon()
	{
		GUIStyle largeLabelIcon = new GUIStyle(EditorStyles.largeLabel);
		largeLabelIcon.fixedWidth = 36;
		largeLabelIcon.padding = new RectOffset(6, 0, -2, 0);
		largeLabelIcon.fontStyle = FontStyle.Bold;
		if (EditorGUIUtility.isProSkin)
			largeLabelIcon.normal.textColor = Color.white;
		return largeLabelIcon;
	}

	public static GUIStyle GetSmallLabel()
	{
		GUIStyle label = new GUIStyle(EditorStyles.label);
		label.alignment = TextAnchor.LowerLeft;
		return label;
	}

	public static GUIStyle GetUniformButton()
	{
		GUIStyle uniformButton = new GUIStyle(EditorStyles.toolbarButton);
		uniformButton.fontSize = 12;
		uniformButton.fixedHeight = 24;
		//uniformButton.normal.background = bg;
		return uniformButton;
	}

	public static GUIStyle GetLargeLabel()
	{
		GUIStyle largeLabel = new GUIStyle(EditorStyles.largeLabel);
		largeLabel.fontStyle = FontStyle.Bold;
		return largeLabel;
	}

	public static GUIStyle GetRegion()
	{
		GUIStyle region = new GUIStyle(EditorStyles.foldout);
		
		return region;
	}

	public static GUIStyle GetLargeLabelIcon()
	{
		GUIStyle largeLabelIcon = new GUIStyle(EditorStyles.largeLabel);
		largeLabelIcon.fixedWidth = 48;
		largeLabelIcon.padding = new RectOffset(12, 0, 0, 0);
		largeLabelIcon.fontStyle = FontStyle.Bold;
		if (!EditorGUIUtility.isProSkin)
			largeLabelIcon.normal.textColor = AlphaColor;
		return largeLabelIcon;
	}

	public static GUIStyle GetFoldoutButton()
	{
		GUIStyle foldoutButton = new GUIStyle(EditorStyles.toolbarButton);
		foldoutButton.fixedHeight = 36f;
		return foldoutButton;
	}

	public static void DrawTitle(string content)
	{
		EditorGUILayout.LabelField(content, GetTitle());
	}

	public static void DrawGuiDivider()
	{
		GUILayout.Space(1f);

		if (Event.current.type == EventType.Repaint)
		{
			Texture2D tex = EditorGUIUtility.whiteTexture;
			Rect rect = GUILayoutUtility.GetLastRect();
			GUI.color = new Color(0f, 0f, 0f, 0.25f);
			GUI.DrawTexture(new Rect(0f, rect.yMin + 15f, Screen.width, 4f), tex);
			GUI.DrawTexture(new Rect(0f, rect.yMin + 15f, Screen.width, 1f), tex);
			GUI.DrawTexture(new Rect(0f, rect.yMin + 18f, Screen.width, 1f), tex);
			GUI.color = Color.white;
		}

		GUILayout.Space(20f);
	}

	public static bool DrawButton(string displayText, string toolTip = null)
	{
		if (GUILayout.Button(new GUIContent(displayText, toolTip), ArchStyle.GetUniformButton()))
		{
			return true;
		}
		return false;
	}

	public static bool DrawToggleBar(string displayText, bool toggleDropDown, string toolTip = null)
	{

		Rect buttonRect = EditorGUILayout.BeginVertical("box");

		if (GUI.Button(buttonRect, new GUIContent("", toolTip), GetSmallFoldoutButton()))
		{
			toggleDropDown = (toggleDropDown ? false : true);
		}

		EditorGUILayout.BeginHorizontal();
		if (toggleDropDown)
		{

			GUILayout.Label("[ X ]", GetSmallLabelIcon());
		}
		else
		{
			GUI.enabled = false;
			GUILayout.Label("[    ]", GetSmallLabelIcon());
			GUI.enabled = true;
		}
		GUILayout.Label(displayText, GetSmallLabel());
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();

		return toggleDropDown;
	}

	public static bool DrawDropDown(string displayText, bool toggleDropDown, string toolTip = null)
	{

		Rect buttonRect = EditorGUILayout.BeginVertical("box");

		if (GUI.Button(buttonRect, new GUIContent("", toolTip), GetFoldoutButton()))
		{
			toggleDropDown = (toggleDropDown ? false : true);
		}

		GUILayout.Space(5f);
		EditorGUILayout.BeginHorizontal();
		if (toggleDropDown)
		{
			GUILayout.Label("[     ]", GetLargeLabelIcon());
		}
		else
		{
			GUILayout.Label("  []  ", GetLargeLabelIcon());
		}
		GUILayout.Label(displayText, GetLargeLabel());
		EditorGUILayout.EndHorizontal();
		GUILayout.Space(8f);
		EditorGUILayout.EndVertical();
		return toggleDropDown;
	}
}

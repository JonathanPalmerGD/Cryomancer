
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class Samples : EditorWindow {

	bool[] visControl = new bool[45];
	float[] hSliderVal = new float[3];
	string textField = "Sample Text";
	float favoriteNum = 0f;
	bool toggleBasic;
	bool toggleLeft;
	bool inspectorBool;
	bool customBtnExpand;
	string passwordText;
	
	public UnityEngine.Object darkwindImg;
	public UnityEngine.Object transparentMat;
	
	int selGridIndex;
	string[] gridStrings = { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9" };
	
	int playerIndices;
	string[] playerGrid = { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9" };
	
	int enemyIndices;
	string[] enemyGrid = { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9" };
	
	[MenuItem ("Window/Darkwind/Window Samples")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(Samples), false, "Window of Opportunity", false);
	}
	
	void Start()
	{
		hSliderVal[0] = 0;
		hSliderVal[1] = 200;
		hSliderVal[2] = 100;
	}
	
	void OnGUI()
	{
		darkwindImg = EditorGUILayout.ObjectField("Image", darkwindImg, (typeof(Texture2D)), false);
		transparentMat = EditorGUILayout.ObjectField("Material", transparentMat, (typeof(Material)), false);
		
		DrawSplash();
		#region Demos
		//To avoid having an armada of booleans that toggle on or off. We just have a big array of them and increment after every example.
		int i = 0;
		
		GUILayout.Label ("EditorGUILayout Basics", EditorStyles.boldLabel);
		
		GUILayout.Space(25);
		
		if ( GUILayout.Button("Expand All"))
		{
			for(int j = 0; j < visControl.Length; j++)
			{
				visControl[j] = true;
			}
		}
		
		if ( GUILayout.Button("Collapse All"))
		{
			for(int j = 0; j < visControl.Length; j++)
			{
				visControl[j] = false;
			}
		}
		
		//We have a boolean that Foldout toggles.
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Foldouts");
		//If the boolean is true, we display content
		if(visControl[i])
		{
			//This content will or won't be shown.
			GUILayout.Label("Clicking the arrow toggles the foldout");
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Labels, TextFields, and Text Areas");
		if(visControl[i])
		{
			//Different label examples
			GUILayout.Label("This is a GUILayout minilabel", EditorStyles.miniLabel);
			GUILayout.Label("This is a GUILayout label");
			GUILayout.Label("This is a bolded GUILayout label", EditorStyles.boldLabel);
			GUILayout.Label("This is a white large GUILayout label", EditorStyles.whiteLargeLabel);
			GUILayout.Space(12);
			textField = EditorGUILayout.TextField("This is a Text Field", textField);
			EditorGUILayout.TextArea("This is a Text Area");
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Sliders");
		if(visControl[i])
		{
			//Our first two sliders are between two static values.
			hSliderVal[0] = EditorGUILayout.Slider("Min Value ", hSliderVal[0], 0, 50);
			hSliderVal[1] = EditorGUILayout.Slider("Max Value ", hSliderVal[1], 150, 200);
			
			//Our third slider has a variable min and max based on the other two sliders.
			hSliderVal[2] = EditorGUILayout.Slider("Slider (min, max) ", hSliderVal[2], hSliderVal[0], hSliderVal[1]);
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Help Boxes");
		if(visControl[i])
		{
			EditorGUILayout.HelpBox("This is a Help Box (None)", MessageType.None);
			EditorGUILayout.HelpBox("This is a Help Box (Info)", MessageType.Info);
			EditorGUILayout.HelpBox("This is a Help Box (Warning)", MessageType.Warning);
			EditorGUILayout.HelpBox("This is a Help Box (Error)", MessageType.Error);
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Toggles");
		if(visControl[i])
		{
			//Toggles are like foldouts with a different control paradigm.
			
			//Set up the toggle to save to the variable toggleBasic.
			toggleBasic = (EditorGUILayout.Toggle("This is a basic toggle", toggleBasic));
			//If toggle basic is true
			if(toggleBasic)
			{
				//Then we display a button which will minimize this example.
				if(GUILayout.Button("Collapse this example"))
				{
					visControl[i] = false;
				}
			}
			else
			{
				GUILayout.Label("Toggle disabled.");
			}
			
			GUILayout.Space(12);
			
			//Set up the toggle to save to the variable toggleBasic.
			toggleLeft = (EditorGUILayout.ToggleLeft("This is a left toggle", toggleLeft));
			//If toggle basic is true
			if(toggleLeft)
			{
				//Then we display a button which will minimize this example.
				if(GUILayout.Button("Collapse this example"))
				{
					visControl[i] = false;
				}
			}
			else
			{
				GUILayout.Label("Toggle disabled.");
			}
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": EGUILayout fields (Unfinished)");
		if(visControl[i])
		{
			//LabelFields are good for showing read only information. Does not automatically update. Junky
			EditorGUILayout.LabelField("[Label] Time since startup " + EditorApplication.timeSinceStartup.ToString());
			
			//EditorGUILayout.LabelField("LabelField", "LabelField2");
			
			EditorGUILayout.FloatField("[Float] Pick a number: ", favoriteNum);
			//ColorField
			//CurveField
			//FloatField
			//LabelField
			//LayerField
			//MaskField
			//ObjectField
			//RectField
			//TagField
			
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Horizontal and Vertical Group");
		if(visControl[i])
		{
			GUILayout.Label("This is a label horizontal region");
			GUILayout.BeginHorizontal("label");
			GUILayout.Button("Left Button");
			GUILayout.Button("Right Button");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(12);
			
			GUILayout.Label("This is a box horizontal region");
			GUILayout.BeginHorizontal("box");
			GUILayout.Button("Left Button");
			GUILayout.Button("Right Button");
			GUILayout.EndHorizontal();
			
			GUILayout.Space(12);
			
			GUILayout.Label("This is a label vertical region");
			GUILayout.BeginVertical("label");
			GUILayout.Button("Top Button");
			GUILayout.Button("Bottom Button");
			GUILayout.EndVertical();
			
			GUILayout.Space(12);
			
			GUILayout.Label("This is a box vertical region");
			GUILayout.BeginVertical("box");
			GUILayout.Button("Top Button");
			GUILayout.Button("Bottom Button");
			GUILayout.EndVertical();
			
			GUILayout.Space(20);
		}
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Password Field");
		if(visControl[i])
		{
			passwordText = EditorGUILayout.PasswordField("Enter test Password:", passwordText);
			EditorGUILayout.LabelField("Hidden password: ", passwordText);
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Custom sized buttons");
		if(visControl[i])
		{
			customBtnExpand = (EditorGUILayout.Toggle("Allow button expansion", customBtnExpand));
			//If toggle basic is true
			
			GUILayoutOption[] btnOptions = {GUILayout.Height(16), GUILayout.Width(48), GUILayout.ExpandWidth(customBtnExpand), GUILayout.ExpandHeight(customBtnExpand)};
			//GUILayout.Button(" + \n+++\n + ", GUILayout.Height(48), GUILayout.Width(100));
			GUILayout.Button("Add", btnOptions);
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Selection Grid");
		if(visControl[i])
		{
			GUILayout.Label("This is a basic selection grid. It stores an index of what was selected.");
			selGridIndex = GUILayout.SelectionGrid(selGridIndex, gridStrings, 3);
			
			GUILayout.Space(12);
			
			
			GUILayout.Label("A modified selection grid. It has different button sizes");
			//The options here give size for the ENTIRE grid. You can't resize the individual elements.
			GUILayoutOption[] gridOptions = {GUILayout.Height(100), GUILayout.Width(200)};
			selGridIndex = GUILayout.SelectionGrid(selGridIndex, gridStrings, 3, gridOptions);
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": GUIStyles (Unfinished)");
		if(visControl[i])
		{
			/*
			GUIStyle style = new GUIStyle();

			GUILayout.Label("Lets try out some GUIStyle modification");

			//style.alignment = TextAnchor.MiddleCenter;

			style.hover.textColor = Color.green;

			//This is ugly and does not work as intended.
			GUILayout.Button("Mouse over me", style);*/
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Inspector Title (Unfinished) ");
		if(visControl[i])
		{
			//Figure out inspector title bar at some point.
			//inspectorBool = EditorGUILayout.InspectorTitlebar(inspectorBool);
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": ");
		if(visControl[i])
		{
			//It is better policy to remove parts out so you don't have a GIANT bloated OnGUI method (like I currently have)
			DrawGrid();
			
			GUILayout.Space(20);
		}
		i++;
		
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": Editor Notification and Popups");
		if (visControl[i])
		{
			if (GUILayout.Button("Press for Notification"))
			{
				this.ShowNotification(new GUIContent("This is a notification"));
			}
			
			
			
			GUILayout.Space(20);
		}
		i++;
		
		
		/* [Blank Template]
		visControl[i] = EditorGUILayout.Foldout(visControl[i], "Example " + i + ": ");
		if(visControl[i])
		{
			
			GUILayout.Space(20);
		}
		i++;*/
		
		#endregion
		
	}
	
	void DrawSplash()
	{
		/*
		Rect rect = new Rect(50, this.position.height - 600, 600, 600);
		Rect rect2 = new Rect(this.position.width - 164, this.position.height - 164, 128, 128);
		GUIStyle style = new GUIStyle();
		float minWidth = 0;
		float maxWidth = 0;
		GUIContent gcontent = new GUIContent((Texture)darkwindImg);
		style.CalcMinMaxWidth(gcontent, out minWidth, out maxWidth);
		//Debug.Log("Min: " + minWidth + "   Max: " + maxWidth);
		style.stretchWidth = false;
		style.stretchHeight = false;
		style.padding = new RectOffset(0, 0, 0, 0);
		style.alignment = TextAnchor.LowerCenter;
		EditorGUI.DrawPreviewTexture(rect, (Texture)darkwindImg, (Material)transparentMat, ScaleMode.ScaleToFit);
		
		GUI.Label(rect2, (Texture)darkwindImg, style);*/
	}
	
	void DrawGrid()
	{
		GUILayout.Space(12);
		
		GUILayout.Label("A modified selection grid. It has different button sizes");
		//The options here give size for the ENTIRE grid. You can't resize the individual elements.
		GUILayoutOption[] gridOptions = { GUILayout.Height(100), GUILayout.Width(200) };
		int lastIndex = playerIndices;
		playerIndices = GUILayout.SelectionGrid(playerIndices, playerGrid, 3, gridOptions);
		
		//If the player makes an action, give the enemy a turn.
		if (lastIndex != playerIndices)
		{
			EnemyTurn();
		}
		
		GUILayout.Label("This is a basic selection grid. It stores an index of what was selected.");
		//We can do things if the player clicks on the enemy's grid?
		GUILayout.SelectionGrid(enemyIndices, enemyGrid, 3, gridOptions);
	}
	
	void EnemyTurn()
	{
		enemyIndices = UnityEngine.Random.Range(0, 9);
	}
}

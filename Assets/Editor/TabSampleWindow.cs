using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class TabSampleWindow : EditorWindow {
	[MenuItem ("Window/Darkwind/Tab Sample")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(TabSampleWindow), false, "Tab Sample", false);
	}

	private bool toggleAssets = true;
	
	private int tabSelected = 0;
	private int iconTabSelected = 0;
	private string[] tabNames = {"Alpha Tab", "Beta Tab", "Gamma", "Four", "Five", "Deadly Six", "Alpha Tab", "Beta Tab", "Gamma"};
	private string[] tabTooltips = {"Alpha Tab's contents: \"This is content of the alpha tab\"", "Beta Tab's contents: \"Lorem ipsum doler sit\"", "Gamma Tab's contents: \"Gamma Tab\t\tGamma Tab\t\tGamma Tab\t\tGamma Tab\""};
	public Texture2D[] tabIcons = {};
	public string[] iconTabTooltips = {"Alpha Tab", "Beta Tab", "Gamma", "Four"};
	public List<UnityEngine.Object> tabIconAssets;
	private void DrawTabs(){
		EditorGUILayout.Space();

		int checkChange = tabSelected;

		tabSelected = DarkwindStyles.DrawTabs(tabSelected, tabNames, tabTooltips, tabIcons, 5, (EditorWindow)this);
		//tabSelected = GUILayout.Toolbar(tabSelected, tabNames);

		if(tabSelected != checkChange){
			//In case something needs to be called when you change to a tab.
			//Debug.Log("Changed Tab from " + tabNames[checkChange] + " to " + tabNames[tabSelected]);
			//this.ShowNotification(new GUIContent("Different Tab selected"));
		}

		if(tabSelected == 0){
			for(int i = 0; i < 10; i++){
				GUILayout.Label("This is content of the alpha tab");
			}
		}
		else if(tabSelected == 1){
			for(int i = 0; i < 15; i++){
				GUILayout.Label("Lorem ipsum doler sit");
			}
		}
		else if(tabSelected == 2){
			for(int i = 0; i < 20; i++){
				GUILayout.Label("Gamma Tab\t\tGamma Tab\t\tGamma Tab\t\tGamma Tab");
			}
		}
		else if(tabSelected == 3){
			for(int i = 0; i < 4; i++){
				GUILayout.Label("Fantastic Four\t\tFantastic Four\t\tFantastic Four\t\tFantastic Four");
			}
		}
		else if(tabSelected == 4){
			for(int i = 0; i < 5; i++){
				GUILayout.Label("FIVE IS THE LONELIEST NUMBERRRRRRR");
			}
		}
		else if(tabSelected == 5){
			for(int i = 0; i < 5; i++){
				GUILayout.Label("BANG, You're dead.");
			}
		}

		
		GUILayout.Space(16);

		iconTabSelected = DarkwindStyles.DrawIconTabs(iconTabSelected, tabIcons, iconTabTooltips, 2, (EditorWindow)this);
		if(iconTabSelected == 0){
			for(int i = 0; i < 6; i++){
				GUILayout.Label("Alpha");
			}
		}
		else if(iconTabSelected == 1){
			for(int i = 0; i < 8; i++){
				GUILayout.Label("Lorem ipsum doler sit");
			}
		}
		else if(iconTabSelected == 2){
			for(int i = 0; i < 10; i++){
				GUILayout.Label("Gamma Tab\t\tGamma Tab\t\tGamma Tab\t\tGamma Tab");
			}
		}
		else if(iconTabSelected == 3){
			for(int i = 0; i < 4; i++){
				GUILayout.Label("Fantastic Four\t\tFantastic Four\t\tFantastic Four\t\tFantastic Four");
			}
		}
		else if(iconTabSelected == 4){
			for(int i = 0; i < 5; i++){
				GUILayout.Label("FIVE IS THE LONELIEST NUMBERRRRRRR");
			}
		}
	}
	
	void OnGUI()
	{
		toggleAssets = EditorGUILayout.ToggleLeft("Toggle Asset Control", toggleAssets);
		if(toggleAssets){
			if(tabIconAssets == null){
				tabIconAssets = new List<UnityEngine.Object>();
			}

			for(int i = 0; i < tabIconAssets.Count; i++){
				tabIconAssets[i] = EditorGUILayout.ObjectField("Tab Icon Image", tabIconAssets[i], (typeof(Texture2D)), false);
				if(tabIconAssets[i] == null){
					tabIconAssets.RemoveAt(i);
				}
			}

			if(tabIconAssets.Count == 0 || tabIconAssets[tabIconAssets.Count - 1] != null){
				UnityEngine.Object temp = EditorGUILayout.ObjectField("Add another icon", null, (typeof(Texture2D)), false);
				if(temp != null){
					tabIconAssets.Add(temp);
				}
			}
			List<Texture2D> tempList = new List<Texture2D>();
			for(int i = 0; i < tabIconAssets.Count; i++){
				tempList.Add((Texture2D)tabIconAssets[i]);
			}
			tabIcons = tempList.ToArray();
		}


		DrawTabs();
	}
}
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Root : MonoBehaviour
{
	public Rect rootRect;
	private bool dragWindow = true;

	void Start()
	{
		
	}

	void OnGUI()
	{

		if (dragWindow)
		{
			rootRect = GUI.Window(0, rootRect, MainDraw, "Root Window");
		}
		else
		{
			MainDraw(0);
		}
	}

	void MainDraw(int windowID)
	{
		rootRect = new Rect(0, 0, Screen.width, Screen.height);
		List<Anchor> anchors = GetComponentsInChildren<Anchor>().ToList();

		//We sort by the depth of the anchors
		anchors = anchors.OrderBy(item => item.depth).ToList();
		anchors.Reverse();
		//Deeper items are drawn first.

		for (int i = 0; i < anchors.Count; i++)
		{
			//Debug.Log(anchors[i].name + "\n");

			anchors[i].Draw(rootRect);
		}
	}
}



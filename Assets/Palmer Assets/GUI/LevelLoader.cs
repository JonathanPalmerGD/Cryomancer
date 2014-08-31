using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour 
{
	public string gameName = "Game";
	public string creatorName = "Zeus Almighty";
	public bool visible = true;
	// Use this for initialization
	void Start () 
	{

	}

	void OnGUI () 
	{
		if (visible)
		{
			// Make a background box
			GUI.Box(new Rect(25, 75, 250, 100), gameName + " made by " + creatorName);
			GUI.Label(new Rect(50, 105, 230, 30), "Press Tab to open/close the menu");
			if (GUI.Button(new Rect(35, 135, 230, 20), "Reload game"))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			visible = !visible;
		}
	}
}

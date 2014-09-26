using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
	#region Variables
	private GameObject player;
	//Where the player currently is in the dialogue tree
	private int dialogue = 0;
	//How big the font is
	public int textFontSize;
	//GUI Rect- where is it, how big
	public Rect windowRect;
	//Is the player currently in our volume
	private bool playerInRange = false;
	//What we say, in order
	public string[] lines = new string[10];
	#endregion

	// Use this for initialization
	void Start () 
	{
		textFontSize = 20;
		player = GameObject.FindGameObjectWithTag("Player");
		windowRect = new Rect((Screen.width / 2) - 225, 10, 450, 140);
		#region Error Proofing
		if (lines.Length == 0)
		{
			//Just to stop us from accidentally having Index Out of Bounds issues
			lines = new string[2];
			lines[0] = "Look Out!";
			lines[1] = "Hey, Listen!";
		}
		#endregion
	}

	void OnGUI()
	{
		GUI.skin.box.fontSize = textFontSize;
		if(playerInRange)
		{
			//If the player is in range, display the text at their current spot
			GUI.skin.box.wordWrap = true;
			GUI.Box(windowRect, "[T to Advance Text]\n" + lines[dialogue]);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		//This should be updated to be a trigger volume instead of a sphere point distance check
		//Note to self, update that at some point
		float distanceBetween = Vector3.Distance(transform.position, player.transform.position);

		if (transform.localScale.x/2 > distanceBetween)
		{
			//If they're inside and hit T, advance dialogue
			playerInRange = true;
			if (Input.GetKeyDown(KeyCode.T))
			{
				if (dialogue < lines.Length - 1)
				{
					dialogue++;
				}
			}
		}
		else
		{
			//Reset when they leave
			playerInRange = false;
			dialogue = 0;
		}

	}
}

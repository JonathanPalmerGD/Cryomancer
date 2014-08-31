using UnityEngine;
using System.Collections;

public class TriggerNPC : MonoBehaviour 
{
	private GameObject player;
	private int dialogue = 0;
	public int textFontSize;
	public Rect windowRect;
	public bool playerInRange = false;
	public string[] lines = new string[10];
	public float counter = 0.0f;

	// Use this for initialization
	void Start () 
	{
		textFontSize = 20;
		player = GameObject.FindGameObjectWithTag("Player");
		windowRect = new Rect((Screen.width / 2) - 225, 10, 450, 140);
		#region Error Proofing. Do not change this section
		if (lines.Length == 0)
		{
			//Do not change this section
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
			GUI.skin.box.wordWrap = true;
			if (dialogue < lines.Length - 1)
			{
				GUI.Box(windowRect, "[T to Advance Text]\n" + lines[dialogue]);
			}
			else
			{
				GUI.Box(windowRect, lines[dialogue]);
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		//The counter for the fairy timing out. This is in case the fairy is lagging a small bit.
		if(counter > 0)
		{
			counter -= Time.deltaTime;
			if (counter < 0)
			{
				dialogue = 0;
				playerInRange = false;
			}
		}
		if (playerInRange)
		{
			if (Input.GetKeyDown(KeyCode.T))
			{
				if (dialogue < lines.Length - 1)
				{
					dialogue++;
				}
			}
		}
	}

	/// <summary>
	/// This is a trigger region dialogue NPC. When the player enters we remember
	/// </summary>
	/// <param name="collider"></param>
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			//Tell the fairy to talk
			playerInRange = true;
			counter = 0.0f;
		}
	}

	//When they leave, we start a counter
	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Player")
		{
			//Start the countdown to the fairy being quiet
			counter = .25f;
		}
	}
}

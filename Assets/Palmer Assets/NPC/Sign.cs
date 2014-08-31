using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour 
{
	private GameObject player;
	private bool playerInRange = false;
	public Rect windowRect;
	public int textFontSize;
	public string instruction;

	// Use this for initialization
	void Start () 
	{
		textFontSize = 20;
		windowRect = new Rect((Screen.width / 2) - 225, 10, 450, 140);
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void OnGUI()
	{
		GUI.skin.box.fontSize = textFontSize;
		if(playerInRange)
		{
			GUI.skin.box.wordWrap = true;
			GUI.Box(windowRect, instruction);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		float distanceBetween = Vector3.Distance(transform.position, player.transform.position);

		playerInRange = false;

		if (transform.localScale.x/2 > distanceBetween)
		{
			playerInRange = true;
		}
	}
}

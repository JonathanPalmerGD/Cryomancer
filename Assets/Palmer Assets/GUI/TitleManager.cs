using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour
{
	public int menuState = 0;
	//My image
	public Texture splashTexture;
	public Rect ScreenInfo = new Rect(Screen.width, Screen.height, 0, 0);
	public string playText = "Play";
	public string[] tryAgainText = {"Try Again - Easy!", "Try Again - Medium!", "Try Again - Hard!"};
	public string loadingText = "Loading...";
	public bool[] isLoading = { false, false, false };
	public GUISkin mainSkin;
	public GUISkin titleSkin;
	public Vector2 screenSize;
	public Rect buttonInfo = new Rect(0, 0, 150, 50);
	public Rect[] buttonInfoArr = { new Rect(0, 0, 150, 50), new Rect(0, 0, 150, 50), new Rect(0, 0, 150, 50) };

	void OnGUI()
	{
		screenSize = new Vector2(Screen.width, Screen.height);
		buttonInfo = new Rect(Screen.width / 2 - buttonInfo.width / 2, (Screen.height * .85f) - buttonInfo.height / 2, buttonInfo.width, buttonInfo.height);
		buttonInfoArr[0] = new Rect(Screen.width / 2 - buttonInfo.width / 2 - buttonInfoArr[0].width - 10, (Screen.height * .85f) - buttonInfoArr[0].height / 2, buttonInfoArr[0].width, buttonInfoArr[0].height);
		buttonInfoArr[1] = buttonInfo;

		buttonInfoArr[2] = new Rect(Screen.width / 2 - buttonInfo.width / 2 + buttonInfoArr[2].width + 10, (Screen.height * .85f) - buttonInfoArr[2].height / 2, buttonInfoArr[2].width, buttonInfoArr[2].height);

		#region Title State
		if (menuState == 0)
		{
			GUI.skin = titleSkin;
			//Draw a rectangle
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), splashTexture, ScaleMode.StretchToFill);
			GUI.Label(new Rect(Screen.width / 2 - Screen.width * .3f, Screen.height * .05f, Screen.width * .6f, Screen.height * .3f), "Cryomancer Ascension");

			GUI.skin = mainSkin;
			GUI.skin.label.wordWrap = true;
			if (Screen.width > 700 && Screen.height > 450)
			{
				GUI.Label(new Rect(Screen.width / 2 - Screen.width * .25f, Screen.height * .35f, Screen.width * .5f, Screen.height * .15f), "WASD to move, Space to Jump");
				GUI.Label(new Rect(Screen.width * .20f, Screen.height * .50f, Screen.width * .60f, Screen.height * .30f), "You are a cryomancer, a naturally gifted ice mage.\n\nYou were imprisoned by the dark tyrant.\n\nThe ice fae have come to aid you.\n\n\n\nYou have awoken.\n\nIt is time to fight back and escape.");

				GUI.Label(new Rect(Screen.width * .05f, Screen.height * .85f, Screen.width * .30f, Screen.height * .10f), "Made by Jon Palmer");
				GUI.Label(new Rect(Screen.width * .05f, Screen.height * .90f, Screen.width * .30f, Screen.height * .10f), "www.JonathanPalmerGD.com");
			}
			else
			{
				GUI.Label(new Rect(0, Screen.height * .10f, Screen.width, Screen.height * .50f), "Your resolution is low.\n\nPlease full screen.");
			}

			string buttonText = "";
			if (isLoading[0])
			{
				buttonText = loadingText;
			}
			else
			{
				buttonText = playText;
			}
			if (GUI.Button(buttonInfo, buttonText))
			{
				isLoading[0] = true;
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		}
		#endregion
		#region Game Over State
		else if (menuState == 1)
		{
			GUI.skin = titleSkin;
			//Draw a rectangle
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), splashTexture, ScaleMode.StretchToFill);
			GUI.Label(new Rect(Screen.width / 2 - Screen.width * .3f, Screen.height * .05f, Screen.width * .6f, Screen.height * .3f), "Game Over");

			GUI.skin = mainSkin;
			GUI.skin.label.wordWrap = true;

			GUI.Label(new Rect(Screen.width * .05f, Screen.height * .85f, Screen.width * .30f, Screen.height * .10f), "Made by Jon Palmer");
			GUI.Label(new Rect(Screen.width * .05f, Screen.height * .90f, Screen.width * .30f, Screen.height * .10f), "www.JonathanPalmerGD.com");

			string[] buttonTextArr = {"", "", ""};
			for(int i = 0; i < isLoading.Length; i++)
			{
				if (isLoading[i])
				{
					buttonTextArr[i] = loadingText;
				}
				else
				{
					buttonTextArr[i] = tryAgainText[i];
				}

				if (GUI.Button(buttonInfoArr[i], buttonTextArr[i]))
				{
					isLoading[i] = true;
					if (GameObject.FindGameObjectWithTag("Properties") != null)
					{
						GameObject.FindGameObjectWithTag("Properties").GetComponent<GameStats>().difficulty = i;
					}
					Application.LoadLevel("DarkChallenge");
				}
			}
		}
		#endregion
		#region Victory State
		else if (menuState == 2)
		{
			GUI.skin = titleSkin;
			//Draw a rectangle
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), splashTexture, ScaleMode.StretchToFill);
			GUI.Label(new Rect(Screen.width / 2 - Screen.width * .3f, Screen.height * .05f, Screen.width * .6f, Screen.height * .3f), "You have defeated\n\nthe dark tyrant!");

			GUI.skin = mainSkin;
			GUI.skin.label.wordWrap = true;

			GUI.Label(new Rect(Screen.width / 2 - Screen.width * .25f, Screen.height * .26f, Screen.width * .5f, Screen.height * .50f), "\n\n\nThanks for playing\n\n\nCredit to BHinton Photography for the\nRetro Ceiling Tile filterforge texture\n\nThanks to Variance Theory for\nthe Detonator Package\n\n\nPlay again and press I to activate cheats.\nThe O-letter key also damages the tyrant.\n\nPress P in the title screen for a\ndifferent game - Ice Survival.");
		

			GUI.Label(new Rect(Screen.width * .05f, Screen.height * .85f, Screen.width * .30f, Screen.height * .10f), "Made by Jon Palmer");
			GUI.Label(new Rect(Screen.width * .05f, Screen.height * .90f, Screen.width * .30f, Screen.height * .10f), "www.JonathanPalmerGD.com");

			if (GUI.Button(buttonInfo, playText + " Again?"))
			{
				//playText = "Loading...";
				menuState = 0;
				//Application.LoadLevel(0);
			}
			else
			{

			}
		}
		#endregion
	}

	// Use this for initialization
	void Start()
	{
		Screen.showCursor = true;
		Screen.lockCursor = false;

		if (GameObject.FindGameObjectWithTag("Properties") != null)
		{
			GameStats stats = GameObject.FindGameObjectWithTag("Properties").GetComponent<GameStats>();
			menuState = stats.menuState;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			GameObject.Destroy(GameObject.Find("Game Properties"));
			Application.LoadLevel("Icevade");
		}
	}
}

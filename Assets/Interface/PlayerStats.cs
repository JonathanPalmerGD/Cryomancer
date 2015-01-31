using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
	//Player's core attributes
	public float health = 100;
	public int money = 25;
	public int maxHealth = 100;
	public int difficulty = 1;

	//If we can cheat
	public bool cheatKey = false;

	//If the cursor is bound in the screen.
	public bool cursorLocking = true;

	private BossStats boss;
	public GameStats gameStats;
	public Vector2 curScreenSize;

	public static bool paused = false;

	#region Menu Variables
	private Rect pauseMenuRect;
	private float pauseMenuWidth = 200;
	private float pauseMenuHeight = 390;

	private int menuMode;
	public bool wasPlaying = false;

	#endregion

	void Start ()
	{
		if (cursorLocking)
		{
			Screen.showCursor = false;
			Screen.lockCursor = true;
		}

		health = maxHealth;

		//Get game stat initialization info
		if (GameObject.FindGameObjectWithTag("Properties") != null)
		{
			gameStats = GameObject.FindGameObjectWithTag("Properties").GetComponent<GameStats>();
			gameStats.AssignData();
		}

		//Get a ref to the boss (for cheat keys)
		if (GameObject.FindGameObjectWithTag("Boss") != null)
		{
			boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossStats>();
		}

		SetPauseMenuRect();
	}

	void OnGUI()
	{
		//Print our health bar. Will likely get updated.
		curScreenSize = new Vector2(Screen.width, Screen.height);
		Rect boxInfo = new Rect(Screen.width / 2 - 175, Screen.height - 45, 350, 28);
		string playerHp = "";
		for (int i = 0; i < (int)(health / 10); i++)
		{
			playerHp += " [ + ] ";
		}
		GUIStyle style = new GUIStyle(GUI.skin.box);
		style.fontSize = 16;
		GUI.Box(boxInfo, playerHp, style);

		SetPauseMenuRect();

		if (paused)
		{
			GUI.Box(new Rect(-5, -5, Screen.width + 5, Screen.height + 5), "");
			GUI.Box(new Rect(-5, -5, Screen.width + 5, Screen.height + 5), "");
			switch (menuMode)
			{
				case 0:
					pauseMenuRect = GUI.Window(menuMode, pauseMenuRect, DrawPauseMenu, "Paused");
					break;
				case 1:
					pauseMenuRect = GUI.Window(menuMode, pauseMenuRect, DrawSettingsMenu, "Settings & Controls");
					break;
				case 2:
					pauseMenuRect = GUI.Window(menuMode, pauseMenuRect, DrawAboutMenu, "About");
					break;
				default:
					Debug.LogError("Defaulted on menu switch statement. Should not happen. Unpausing to try and reset state.\n");
					paused = false;
					Time.timeScale = 1.0f;
					break;
			}
		}
	}

	#region Helper Methods - Pause, Resume, Size Pause Rect
	void ResumePlay()
	{
		paused = false;
		Time.timeScale = 1.0f;

		if (wasPlaying)
		{
			transform.FindChild("AudioBus").audio.Play();
		}
	}

	void PausePlay()
	{
		paused = true;
		Time.timeScale = 0.0f;

		if (transform.FindChild("AudioBus") != null)
		{
			AudioSource playerBus = transform.FindChild("AudioBus").audio;
			if (playerBus.isPlaying)
			{
				transform.FindChild("AudioBus").audio.Pause();
				wasPlaying = true;
			}
		}
	}

	void SetPauseMenuRect()
	{
		pauseMenuRect = new Rect(
			Screen.width / 2 - pauseMenuWidth / 2,
			Screen.height / 2 - pauseMenuHeight / 2,
			pauseMenuWidth, pauseMenuHeight);
		//Debug.Log(pauseMenuRect + "\n");
	}
	#endregion

	#region Draw Menus
	void DrawPauseMenu(int windowID)
	{
		if(GUI.Button(new Rect(10, 20, 180, 80), "Resume"))
		{
			ResumePlay();
		}

		if (GUI.Button(new Rect(10, 110, 180, 80), "Settings & Controls"))
		{
			menuMode = 1;
		}

		if (GUI.Button(new Rect(10, 200, 180, 80), "About"))
		{
			menuMode = 2;
		}

		if (GUI.Button(new Rect(10, 290, 180, 80), "Quit"))
		{
			Application.Quit();
		}
	}

	void DrawSettingsMenu(int windowID)
	{
		if (GUI.Button(new Rect(10, 20, 180, 80), "Back"))
		{
			menuMode = 0;
		}

		if (GUI.Button(new Rect(10, 110, 90, 80), "Reduce\nSensitivity"))
		{
			MouseLook look = GetComponent<MouseLook>();
			MouseLook look2 = transform.FindChild("Main Camera").camera.GetComponent<MouseLook>();
			if (look.sensitivityX > 3)
			{
				look.sensitivityX -= 2f;
			}
			if (look2.sensitivityX > 3)
			{
				look2.sensitivityX -= 2f;
			}
			if (look2.sensitivityY > 3)
			{
				look2.sensitivityY -= 2f;
			}
		}
		if (GUI.Button(new Rect(100, 110, 90, 80), "Increase\nSensitivity"))
		{
			MouseLook look = GetComponent<MouseLook>();
			MouseLook look2 = transform.FindChild("Main Camera").camera.GetComponent<MouseLook>();
			if (look.sensitivityX < 19)
			{
				look.sensitivityX += 2f;
			}
			if (look2.sensitivityX < 19)
			{
				look2.sensitivityX += 2f;
			} 
			if (look2.sensitivityY < 19)
			{
				look2.sensitivityY += 2f;
			}
		}
		/*
		if (GUI.Button(new Rect(10, 200, 180, 80), ""))
		{
		}

		if (GUI.Button(new Rect(10, 290, 180, 80), ""))
		{
		}*/
	}

	void DrawAboutMenu(int windowID)
	{
		if (GUI.Button(new Rect(10, 20, 180, 80), "Back"))
		{
			menuMode = 0;
		}

		if (GUI.Button(new Rect(10, 110, 180, 80), ""))
		{

		}
	}
	#endregion

	void Update ()
	{
		#region Player Health Check
		//If the player dies, do something
		if (health <= 0)
		{
			//Go back to the menu
			gameStats.menuState = 1;
			Application.LoadLevel(0);
		}
		#endregion

		#region Adjust Sensitivity
		if (Input.GetKeyDown(KeyCode.G))
		{
			MouseLook look = GetComponent<MouseLook>();
			if (look.sensitivityX > 3)
			{
				look.sensitivityX -= 2f;
			}
		}

		if(Input.GetKeyDown(KeyCode.H))
		{
			MouseLook look = GetComponent<MouseLook>();
			if (look.sensitivityX < 19)
			{
				look.sensitivityX += 2f;
			}
		}
		#endregion

		#region Check Paused
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(paused)
			{
				ResumePlay();
			}
			else
			{
				PausePlay();
			}
		}
		#endregion

		#region Cursor Hide & Cursor LockLocking
		if (paused)
		{
			Screen.showCursor = true;
			Screen.lockCursor = false;
		}
		else if (!paused)
		{
			Screen.showCursor = false;
			Screen.lockCursor = true;
		}
		#endregion

		#region Check for cheat code enabling
		if (Input.GetKey(KeyCode.Comma) && Input.GetKey(KeyCode.Period))
		{
			if (Input.GetKeyDown(KeyCode.Y))
			{
				cheatKey = !cheatKey;
			}
		}
		#endregion

		#region Mute Game Volume Toggling
		if (Input.GetKeyDown(KeyCode.M))
		{
			AudioListener.volume = 1 - AudioListener.volume;
		}
		#endregion

		#region Cheat Section
		if (cheatKey)
		{
			if(Input.GetKeyDown(KeyCode.O))
			{
				if (boss != null)
				{
					if (boss.phase == BossStats.BossPhase.Secondary)
					{
						boss.shieldHealth -= 5;
					}
					boss.health -= 5;
				}
			}
			if (Input.GetKeyDown(KeyCode.I))
			{
				Cryomancer runner = GameObject.FindGameObjectWithTag("Player").GetComponent<Cryomancer>();
				runner.changeMaxIce(1000);
				runner.restoreIce(1000);
				runner.declineUnlocked = true;
				runner.inclineUnlocked = true;
				runner.shieldUnlocked = true;
				runner.platformUnlocked = true;
				runner.oldGUI = true;
			}
		}
		#endregion
	}

	/// <summary>
	/// Heal accessor for everyone else. Will not overheal
	/// </summary>
	/// <param name="healAmount"></param>
	public void healPlayer(float healAmount)
	{
		if (health < maxHealth)
		{
			health += healAmount;
			if (health > maxHealth)
			{
				health = maxHealth;
			}
		}
	}
}

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

	private Rect pauseMenuRect;
	public float pauseMenuWidth = 200;
	public float pauseMenuHeight = 50;

	// Use this for initialization
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
		Rect boxInfo = new Rect(Screen.width / 2 - 200, Screen.height - 45, 400, 35);
		string playerHp = "";
		for (int i = 0; i < (int)(health / 10); i++)
		{
			playerHp += " [ + ] ";
		}
		GUI.Box(boxInfo, playerHp);

		SetPauseMenuRect();

		if (paused)
		{
			pauseMenuRect = GUI.Window(0, pauseMenuRect, PauseMenu, "Paused");
		}
	}

	void SetPauseMenuRect()
	{
		pauseMenuRect = new Rect(
			Screen.width / 2 - pauseMenuWidth / 2,
			Screen.height / 2 - pauseMenuHeight / 2,
			pauseMenuWidth, pauseMenuHeight);
		Debug.Log(pauseMenuRect + "\n");

	}

	void PauseMenu(int windowID)
	{
		if(GUI.Button(new Rect(10, 110, 80, 80), "Resume"))
		{
			paused = false;
		}

	}

	// Update is called once per frame
	void Update ()
	{
		//If the player dies, do something
		if (health <= 0)
		{
			//Go back to the menu
			gameStats.menuState = 1;
			Application.LoadLevel(0);
		}

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

		#region Mouse Control
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			paused = !paused;
		}

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

		/*if (Input.GetKeyDown(KeyCode.Return))
		{
			Screen.showCursor = false;
			Screen.lockCursor = true;
		}*/
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

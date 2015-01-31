using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour 
{
	public bool eldest = false;
	public float mouseXSensitivity = 8;
	public float mouseYSensitivity = 8;
	public float playerMaxIce = 25;
	public int difficulty = 1;
	public int menuState = 0;

	public Cryomancer runner;
	public MouseLook look;

	// Use this for initialization
	void Start () 
	{
		//GameObject otherProp;
		//My singleton check
		if (GameObject.FindGameObjectWithTag("Properties") != null)
		{
			if (GameObject.FindGameObjectsWithTag("Properties").Length == 1)
			{
				eldest = true;
			}
			if (!eldest)
			{
				Destroy(this.gameObject);
			}
			//otherProp = GameObject.FindGameObjectWithTag("Properties");
			//if (originalLevel > otherProp.GetComponent<GameStats>().originalLevel)
			//{
			//    Destroy(this.gameObject);
			//}
		}
		DontDestroyOnLoad(this);
	}

	public void CollectData()
	{
		MouseLook look = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
		mouseXSensitivity = look.sensitivityX;
		mouseYSensitivity = look.sensitivityY;

		MouseLook look2 = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Main Camera").camera.GetComponent<MouseLook>();

		mouseXSensitivity = look2.sensitivityX;
		mouseYSensitivity = look2.sensitivityY;

		Cryomancer runner = GameObject.FindGameObjectWithTag("Player").GetComponent<Cryomancer>();
		playerMaxIce = runner.maxIce;
	}

	public void AssignData()
	{
		if (GameObject.FindGameObjectWithTag("Player") != null)
		{
			look = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
			look.sensitivityX = mouseXSensitivity;
			look.sensitivityY = mouseYSensitivity;

			MouseLook look2 = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Main Camera").camera.GetComponent<MouseLook>();

			look2.sensitivityX = mouseXSensitivity;
			look2.sensitivityY = mouseYSensitivity;
			
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().difficulty = difficulty;
			runner = GameObject.FindGameObjectWithTag("Player").GetComponent<Cryomancer>();
			runner.changeMaxIce(playerMaxIce);
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}

using UnityEngine;
using System.Collections;

public class TriggerNextScene : MonoBehaviour 
{
	public int SceneToTrigger = -1;
	public int changeMainMenuStateTo = 0;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			if(GameObject.FindGameObjectWithTag("Properties") != null)
			{
				GameObject.FindGameObjectWithTag("Properties").GetComponent<GameStats>().CollectData();
				if (SceneToTrigger != -1)
				{
					GameObject.FindGameObjectWithTag("Properties").GetComponent<GameStats>().menuState = changeMainMenuStateTo;
					Application.LoadLevel(SceneToTrigger);
				}
				else
				{
					Application.LoadLevel(Application.loadedLevel + 1);
				}
			}
		}
	}
}

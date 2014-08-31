using UnityEngine;
using System.Collections;

public class HealthGUI : MonoBehaviour {
	public int health = 100;
	// Use this for initialization
	void Start () {
	
	}

	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width / 2-20, Screen.height / 2-20, 40, 40), health.ToString());
	}

	// Update is called once per frame
	void Update () {
	
	}
}

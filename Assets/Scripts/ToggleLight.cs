using UnityEngine;
using System.Collections;

public class ToggleLight : MonoBehaviour {

	public Light worldDirLight;

	// Use this for initialization
	void Start()
	{
		worldDirLight.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			worldDirLight.enabled = !worldDirLight.enabled;
		}
	}
}

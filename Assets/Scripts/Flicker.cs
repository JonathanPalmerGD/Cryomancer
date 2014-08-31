using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

	public float counter = 0.0f;
	private float toggleTime = 0.8f;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		counter += Time.deltaTime;
		if (counter > toggleTime)
		{
			counter = 0.0f;
			toggleTime = Random.Range(0.04f, 0.6f);
			light.enabled = !light.enabled;
		}


	}
}

using UnityEngine;
using System.Collections;

public class MaterialFlicker : MonoBehaviour {

	public float counter = 0.0f;
	public Material mat1;
	public Material mat2;
	public bool matChoice = true;
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
			toggleTime = Random.Range(0.4f, 0.6f);
			if (matChoice)
			{
				matChoice = !matChoice;
				renderer.material = mat2;
			}
			else
			{
				matChoice = !matChoice;
				renderer.material = mat1;
			}

			//renderer.material = //other material
		}


	}
}

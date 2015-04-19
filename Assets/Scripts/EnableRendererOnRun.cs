using UnityEngine;
using System.Collections;

public class EnableRendererOnRun : MonoBehaviour 
{

	void Start () 
	{
		GetComponent<Renderer>().enabled = true;
	}
}

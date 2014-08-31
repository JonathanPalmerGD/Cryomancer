using UnityEngine;
using System.Collections;

public class IceBlast : MonoBehaviour 
{
	public float minBlast;
	public float timeSinceBlast;
	public bool firing;
	public ParticleSystem partSys;

	void Start () 
	{

	}	
	
	void Update () 
	{
		if (firing)
		{
			timeSinceBlast += Time.deltaTime;
		}

		
		if(Input.GetKeyDown(KeyCode.C))
		{
			partSys.Play();
		}
		else if (firing)
		{
			partSys.Stop();
			firing = false;
		}


		if (Input.GetKeyDown(KeyCode.V))
		{
			firing = false;
			partSys.Stop();
		}
	}
}

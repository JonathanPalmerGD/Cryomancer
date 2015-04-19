using UnityEngine;
using System.Collections;

public class Igniter : MonoBehaviour 
{
	public GameObject[] platformInner;
	public GameObject[] platformCorner;
	public GameObject[] platformExterior;
	public GameObject primaryCheckpoint;
	public GameObject secondaryCheckpoint;
	public GameObject[] torches;
	public float counter;
	public enum PlatformGroup { None, Inner, Corner, Exterior };
	public PlatformGroup ignited = PlatformGroup.None;
	public float burnFreqMin = 15f;
	public float burnFreqMax = 25f;
	public float burnFreqStandard = 20f;
	public float platformBurnDuration = 5.0f;
	private GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		for (int i = 0; i < torches.Length; i++)
		{
			torches[i].GetComponentInChildren<Burning>().Enable();
		}
		for (int i = 0; i < platformInner.Length; i++)
		{
			platformInner[i].GetComponentInChildren<Burning>().Disable();

			//change enabled checkpoint
			player.GetComponent<TeleTarget>().teleTarget = secondaryCheckpoint;
		}
		for (int i = 0; i < platformCorner.Length; i++)
		{
			platformCorner[i].GetComponentInChildren<Burning>().Disable();
		}
		for (int i = 0; i < platformExterior.Length; i++)
		{
			platformExterior[i].GetComponentInChildren<Burning>().Disable();
		}
	}

	public void DisableAll()
	{
		for (int i = 0; i < platformInner.Length; i++)
		{
			torches[i].GetComponentInChildren<Burning>().Disable();
			platformInner[i].GetComponentInChildren<Burning>().Disable();
			platformCorner[i].GetComponentInChildren<Burning>().Disable();
			platformExterior[i].GetComponentInChildren<Burning>().Disable();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		counter += Time.deltaTime;

		if (ignited == PlatformGroup.None)
		{
			for (int i = 0; i < platformInner.Length; i++)
			{
				platformInner[i].GetComponentInChildren<Burning>().Disable();
				platformCorner[i].GetComponentInChildren<Burning>().Disable();
				platformExterior[i].GetComponentInChildren<Burning>().Disable();
			}

			//change enabled checkpoint
			player.GetComponent<TeleTarget>().teleTarget = primaryCheckpoint;
		}
		else if (ignited == PlatformGroup.Inner)
		{
			if (counter >= platformBurnDuration)
			{
				for (int i = 0; i < platformInner.Length; i++)
				{
					platformInner[i].GetComponent<Burning>().Disable();
					platformCorner[i].GetComponent<Burning>().Enable();

					//change enabled checkpoint
					player.GetComponent<TeleTarget>().teleTarget = primaryCheckpoint;
				}
				counter -= platformBurnDuration;
				ignited = PlatformGroup.Corner;
			}
			else
			{
				if (platformInner.Length > 0)
				{
					if (!platformInner[0].GetComponent<Burning>().onFire)
					{
						for (int i = 0; i < platformInner.Length; i++)
						{
							platformInner[i].GetComponent<Burning>().Enable();

						}
					}
				}
			}
		}
		else if (ignited == PlatformGroup.Corner)
		{
			if (counter >= platformBurnDuration)
			{
				for (int i = 0; i < platformInner.Length; i++)
				{

					platformCorner[i].GetComponent<Burning>().Disable();
					platformExterior[i].GetComponent<Burning>().Enable();
				}
				counter -= platformBurnDuration;
				ignited = PlatformGroup.Exterior;
			}
		}
		else if (ignited == PlatformGroup.Exterior)
		{
			if (counter >= platformBurnDuration)
			{
				for (int i = 0; i < platformInner.Length; i++)
				{
					platformExterior[i].GetComponent<Burning>().Disable();
					platformInner[i].GetComponent<Burning>().Enable();
					//Change back enabled checkpoint.
					player.GetComponent<TeleTarget>().teleTarget = secondaryCheckpoint;

				}
				counter -= platformBurnDuration;
				ignited = PlatformGroup.Inner;
			}
		}
	}
}

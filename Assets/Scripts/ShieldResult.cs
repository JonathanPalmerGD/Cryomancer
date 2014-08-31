using UnityEngine;
using System.Collections;

public class ShieldResult : MonoBehaviour 
{
	public int shieldResult = 0;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	/// <summary>
	/// -1 ignores and destroys
	/// 0 ignores
	/// 1 destroys
	/// 2 stop
	/// 3 reflect
	/// 4 freeze
	/// </summary>
	public int Result()
	{
		return shieldResult;
	}
}

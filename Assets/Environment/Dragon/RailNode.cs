using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RailNode : MonoBehaviour 
{
	//Node location is the position.

	public int id;
	public List<RailNode> adjacentNodes;
	public float verticalOffset;

	void Start () 
	{
	
	}
	
	void Update () 
	{
		for (int i = 0; i < adjacentNodes.Count; i++)
		{
			Debug.DrawLine(transform.position, adjacentNodes[i].transform.position, Color.green);
		}
	}
}

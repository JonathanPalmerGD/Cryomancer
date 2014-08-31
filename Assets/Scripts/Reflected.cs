using UnityEngine;
using System.Collections;

public class Reflected : MonoBehaviour 
{
	//A projectile that hits the boss

	private GameObject boss;
	public float damage = 5;
	public bool damageShield = false;
	public float collisionRadius = 2.0f;

	// Use this for initialization
	void Start () 
	{
		boss = GameObject.FindGameObjectWithTag("Boss");
	}
	
	// Should update to be a trigger instead of a sphere - point check.
	void Update () 
	{
		float distanceBetween = Vector3.Distance(boss.transform.position, transform.position);

		if (collisionRadius > distanceBetween)
		{
			BossStats stats = boss.GetComponent<BossStats>();
			stats.DamageBoss((int)damage, 0.25f, damageShield);
			gameObject.SetActive(false);
		}
	}
}

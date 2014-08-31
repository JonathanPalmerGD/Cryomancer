using UnityEngine;
using System.Collections;

public class DestroyWall : MonoBehaviour 
{
	public AudioSource crateAudio;
	public AudioClip meltClip;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter(Collision c)
	{
		if (c.collider.GetComponent<ShieldResult>() != null)
		{
			crateAudio.PlayOneShot(meltClip);
			//playerAudio.clip = meltClip;
			//playerAudio.Play();
			Destroy(this.gameObject);
		}
	}
}

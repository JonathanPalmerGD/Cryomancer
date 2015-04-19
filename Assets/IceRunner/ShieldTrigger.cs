using UnityEngine;
using System.Collections;

public class ShieldTrigger : MonoBehaviour 
{
	public AudioClip reflectClip;
	public AudioSource playerAudio;
	public AudioClip meltClip;
	public Material freezeMaterial;

	// Use this for initialization
	void Start () 
	{
		playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.GetComponent<ShieldResult>() != null)
		{
			ShieldResult sResult = collider.GetComponent<ShieldResult>();
			// -1 destroy+ignore 0 ignores 1 destroys 2 stop 3 reflect 4 freeze
			if (sResult.Result() == -1)
			{
				//Melt the shield. Play the noise. The noise doesn't work great because it uses the same audio player. Should update that.
				playerAudio.clip = meltClip;
				playerAudio.Play();
				Destroy(this.gameObject);
			}
			else if (sResult.Result() == 0)
			{
				//Nothing happens!
			}
			else if (sResult.Result() == 1)
			{
				//Destroy the fired projectile. Buckshot ceases to be
				Destroy(collider.gameObject);
			}
			else if (sResult.Result() == 2)
			{
				if (collider.gameObject.GetComponent<Rigidbody>() != null)
				{
					//Stop the object
					collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
				}
			}
			else if (sResult.Result() == 3)
			{
				if (collider.gameObject.GetComponent<Rigidbody>() != null)
				{
					//Send the projectile in the direction of the shield
					collider.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * collider.gameObject.GetComponent<Rigidbody>().velocity.magnitude;

					//Play our audio
					playerAudio.clip = reflectClip;
					playerAudio.Play();

					//different trigger results. Haven't set up a proper bounce that feels good to use.
					//collider.gameObject.rigidbody.velocity = -collider.gameObject.rigidbody.velocity;
					//collider.gameObject.rigidbody.velocity = new Vector3(transform.forward.x * collider.gameObject.rigidbody.velocity.x, transform.forward.y * collider.gameObject.rigidbody.velocity.y, transform.forward.z * collider.gameObject.rigidbody.velocity.z);
					//Debug.DrawLine(transform.position, transform.position + collider.gameObject.rigidbody.velocity * 2, Color.green, 5.0f);
				}
				
			}
			else if (sResult.Result() == 4 && collider.gameObject.GetComponent<MoveToTarget>().enabled)
			{
				//Tell the thing to freeze. Destroy the shield.
				collider.gameObject.GetComponent<EvilToken>().enabled = false;
				collider.gameObject.GetComponent<MoveToTarget>().enabled = false;
				collider.gameObject.GetComponent<Rigidbody>().useGravity = true;
				collider.gameObject.GetComponent<Renderer>().material = freezeMaterial;
				collider.gameObject.GetComponent<Renderer>().GetComponent<ParticleSystem>().enableEmission = false;
				collider.gameObject.GetComponent<Renderer>().GetComponent<ParticleSystem>().GetComponent<Light>().enabled = false;
				
				Destroy(this.gameObject);
			}
		}
	}
}

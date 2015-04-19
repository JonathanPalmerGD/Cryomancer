using UnityEngine;
using System.Collections;

public class DarkCharger : MonoBehaviour 
{
	private GameObject player;
	//Our counter for time based things
	public float counter = 0.0f;
	//How long we follow the player.
	public float followDuration = 4.0f;
	//How long we pause before charging
	public float pauseDuration = 0.70f;
	//How long our charge is
	public float chargeDuration = 1.5f;
	//How fast we follow when in follow mode.
	public float followVelocity = 10.0f;

		//public float pauseForce = 20.0f;
	//The speed at which we move when in pause mode. (set to 0 if you want it to stop moving)
	public float pauseVelocity = 4.0f;
	//How much force we move with when we charge.
	private float chargeForce = 3000.0f;
		//public float chargeVelocity = 45.0f;
	//The direction from the charger to the player.
	public Vector3 dirToPlayer;

	//What are our three modes
	public enum ChargeState {Following, Pausing, Charging, Dying};

	//What we are currently doing (Look at ChargeState's modes)
	public ChargeState motionState = ChargeState.Following;

	// Use this for initialization
	void Start () 
	{
		//Set ourself to follow mode.
		motionState = ChargeState.Following;
		GetComponent<Renderer>().material.color = Color.white;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Increase our counter.
		counter += Time.deltaTime;

		#region Following
		if (motionState == ChargeState.Following)
		{
			//Find Vector3 to the player.
			dirToPlayer = player.transform.position - transform.position;
			
			//Set the velocity while it is following the player
			GetComponent<Rigidbody>().velocity = dirToPlayer.normalized * followVelocity;
			
			//If we have followed long enough.
			if (counter > followDuration)
			{
				//Stop moving and any forward motion
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				GetComponent<Rigidbody>().rotation = Quaternion.identity;

				//Update to pause color and pause state.
				GetComponent<Renderer>().material.color = Color.grey;
				motionState = ChargeState.Pausing;

				//Reset timer
				counter = 0.0f;
			}
		}
		#endregion
		#region Pausing
		if (motionState == ChargeState.Pausing)
		{
			//Set ourself to our pause velocity.
			GetComponent<Rigidbody>().velocity = (dirToPlayer.normalized * pauseVelocity) + Vector3.up * 3.0f;

			//If we have paused long enough
			if (counter > pauseDuration)
			{
				//Set our velocity and rotation to zero
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				GetComponent<Rigidbody>().rotation = Quaternion.identity;

				//Show them we are angry
				GetComponent<Renderer>().material.color = Color.red;

				//Update our state to say we're charging
				motionState = ChargeState.Charging;
				counter = 0.0f;

				//We move in the direction of the player. We don't update that when charging
				//Give ourselves a force that scales with our charge force and mass.
				GetComponent<Rigidbody>().AddForce(dirToPlayer.normalized * chargeForce * GetComponent<Rigidbody>().mass);
			}
		}
		#endregion
		#region Charging
		if (motionState == ChargeState.Charging)
		{
			//If we have charged for long enough
			if (counter > chargeDuration)
			{
				//Set our velocity and rotation back to zero to stop charge.
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				GetComponent<Rigidbody>().rotation = Quaternion.identity;
				//Change our color and state to say we're not aggressive.
				GetComponent<Renderer>().material.color = Color.white;
				motionState = ChargeState.Following;
				//Reset our counter
				counter = 0.0f;
			}
		}
		#endregion
	}
}

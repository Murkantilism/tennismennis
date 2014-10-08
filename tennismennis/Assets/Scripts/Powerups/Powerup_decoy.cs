using UnityEngine;
using System.Collections;

public class Powerup_decoy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Actives the power up on the player given (true = player 1, false = player 2)
	public void ActivatePowerup(bool player){
		Debug.Log("DECOY ACTIVATED");
		
		// Load a ball prefab from the Resources folder
		GameObject decoy = (GameObject)Resources.Load("/Prefabs/Ball");
		
		// Rename it so it cannot affect score/be hit by anything like a real ball can
		decoy.name = "Decoy";
		
		// Get the real ball's velocity and angle
		BallMovement ballMvmt = GameObject.Find("Ball").GetComponent<BallMovement>();
		
		
		// Set the decoy's velocity and angle to something different
		BallMovement decoyMvmt = decoy.GetComponent<BallMovement>();
		
	}
	
	public void DeactivatePowerup(bool player){
	
	}
}

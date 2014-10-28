using UnityEngine;
using System.Collections;

public class Powerup_powerhitter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Actives the power up on the player given (true = player 1, false = player 2)
	public void ActivatePowerup(bool player){
		Debug.Log("POWERHITTER ACTIVATED");
		// Get the ball's script, set powerhitter boolean to true
		BallMovement ballMvmt = GameObject.Find("Ball").GetComponent<BallMovement>();
		if(player){
			ballMvmt.powerHitterEnabled_p1 = true;
		}else{
			ballMvmt.powerHitterEnabled_p2 = true;
		}
	}
	
	public void DeactivatePowerup(bool player){
		// Get the ball's script, set powerhitter boolean to false
		BallMovement ballMvmt = GameObject.Find("Ball").GetComponent<BallMovement>();
		if(player){
			ballMvmt.powerHitterEnabled_p1 = false;
		}else{
			ballMvmt.powerHitterEnabled_p2 = false;
		}
	}
}

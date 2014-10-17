using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	
	PowerupSystem powerupSystem;
	
	void Start(){
		powerupSystem = GameObject.Find("PowerupSystem").GetComponent<PowerupSystem>();
	}

	// Check for things hitting this power-up
	void OnTriggerEnter2D(Collider2D col){
		// If either player's racket hits the power-up, check if it's being tossed
		if(col.gameObject.name == "racket_p1"){
			Debug.Log("P1 Racket Toss Hit Powerup");
			RacketToss tossp1 = col.gameObject.GetComponent<RacketToss>();
			// If player 1 did indeed toss his racket, tell PowerupSystem about it!
			if (tossp1.being_thrown == true){
				powerupSystem.PowerupAcquired(true); // true = p1
				Debug.Log("P1 Racket was indeed being tossed");
			}
		}
		
		if(col.gameObject.name == "racket_p2"){
			Debug.Log("P2 Racket Toss Hit Powerup");
			RacketToss tossp2 = col.gameObject.GetComponent<RacketToss>();
			// If player 2 did indeed toss his racket, tell PowerupSystem about it!
			if (tossp2.being_thrown == true){
				powerupSystem.PowerupAcquired(false); // false = p2
				Debug.Log("P2 Racket was indeed being tossed");
			}
		}
		
		// If the ball collides with this power-up, tell the PowerupSytem about it!
		if(col.gameObject.name == "Ball"){
			BallMovement ball = col.gameObject.GetComponent<BallMovement>();
			
			powerupSystem.PowerupAcquired(ball.lastHit);
			
			Debug.Log("BALL HIT POWERUP");
		}
		
		// If this power-up collides with the net or the court, destroy it!
		if(col.gameObject.name == "Net" || col.gameObject.name == "Court"){
			powerupSystem.DestroyPowerup();
		}
	}
	
	//void OnCollisionEnter2D(Collision2D col){
		
	//}
}
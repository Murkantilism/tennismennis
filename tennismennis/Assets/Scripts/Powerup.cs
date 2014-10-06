using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	
	PowerupSystem powerupSystem;

	// If the ball collides with this power-up, tell the PowerupSytem about it!
	void OnTriggerEnter(Collider col){
		powerupSystem = GameObject.Find("PowerupSystem").GetComponent<PowerupSystem>();
		
		if(col.name == "Ball"){
			BallMovement ball = col.gameObject.GetComponent<BallMovement>();
			
			powerupSystem.PowerupAcquired(ball.lastHit);
		}
	}
}
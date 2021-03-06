﻿using UnityEngine;
using System.Collections;

public class Powerup_spaceWalk : MonoBehaviour {

	Player1 p1_movement;
	Player2 p2_movement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Actives the power up on the player given (true = player 1, false = player 2)
	public void ActivatePowerup(bool player){
		Debug.Log("SPACE WALK ACTIVATED");
		
		p1_movement = GameObject.Find("Player1").GetComponent<Player1>();
		p2_movement = GameObject.Find("Player2").GetComponent<Player2>();
		
		if(player == true){
			p1_movement.jumpHeight += 5;
		}else{
			p2_movement.jumpHeight += 5;
		}		
	}
	
	public void DeactivatePowerup(bool player){
		if(player == true){
			p1_movement.jumpHeight -= 5;
		}else{
			p2_movement.jumpHeight -= 5;
		}
	}
}

using UnityEngine;
using System.Collections;

public class EndOfRound : MonoBehaviour {

	Transform player1;
	Transform player2;
	
	Transform ball;

	Vector3 player1_spawn;
	Vector3 player2_spawn;

	bool serving_player = true; // Who's currently serving? true = player 1, false = player 2 | player 1 by default

	float roundStartDelay = 3.0f; // How long do we wait to start the next round?

	float ballSpawnHeight = 2.0f; // How high above the players should the ball spawn?

	// Use this for initialization
	void Start () {
		// Find and assign all relevent vars
		player1 = GameObject.Find ("Player1").transform;
		player2 = GameObject.Find ("Player2").transform;
		ball = GameObject.Find ("Ball").transform;

		player1_spawn = GameObject.Find ("player1_spawn").transform.position;
		player2_spawn = GameObject.Find ("player2_spawn").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Pause the game while we reset for the next round
	void PauseGame(){

	}

	// Reset both player's positions to their respective spawn points
	void RespawnPlayers(){
		player1.position = player1_spawn;
		player2.position = player2_spawn;
	}

	// Reset the ball's position to the opposite player that last served
	void RespawnBall(){
		// Flip the value of the server_player bool to switch who serves next
		serving_player = !serving_player;

		// Spawn ball above player 1
		if(serving_player == true){
			ball.position = new Vector3(player1_spawn.x, player1_spawn.y + ballSpawnHeight, player1_spawn.z);
		// Spawn ball above player 2
		}else{
			ball.position = new Vector3(player2_spawn.x, player2_spawn.y + ballSpawnHeight, player2_spawn.z);
		}
	}

	// Delay the start of the next round, then start it
	IEnumerator DelayedRoundStart(){
		yield return new WaitForSeconds(roundStartDelay);
		// Unpause to start next round

	}
}

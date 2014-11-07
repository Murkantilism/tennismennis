using UnityEngine;
using System.Collections;

public class EndOfRound : MonoBehaviour {
	
	GameObject player1;
	GameObject player2;
	
	Transform ball;
	
	BallMovement ballMovement;
	
	GameObject racket_p1;
	GameObject racket_p2;
	
	Vector3 player1_spawn;
	Vector3 player2_spawn;
	
	bool serving_player = true; // Who's currently serving? true = player 1, false = player 2 | player 1 by default
	
	float ballSpawnHeight = 2.0f; // How high above the players should the ball spawn?
	
	public GUISkin guiSkin;
	
	bool paused = false;
	
	Player1 player_1;
	Player2 player_2;
	
	public Texture2D threeLabel;
	public Texture2D twoLabel;
	public Texture2D oneLabel;
	public Texture2D startLabel;
	
	bool three;
	bool two;
	bool one;
	bool start;
	
	// Use this for initialization
	void Start () {
		// Find and assign all relevent vars
		player1 = GameObject.Find ("Player1");
		player2 = GameObject.Find ("Player2");
		racket_p1 = GameObject.Find ("racket_p1");
		racket_p2 = GameObject.Find ("racket_p2");
		ball = GameObject.Find ("Ball").transform;
		
		ballMovement = ball.GetComponent<BallMovement> ();
		
		
		
		player1_spawn = GameObject.Find ("player1_spawn").transform.position;
		player2_spawn = GameObject.Find ("player2_spawn").transform.position;
		
		player_1 = player1.GetComponent<Player1> ();
		player_2 = player2.GetComponent<Player2> ();
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: Eventually remove this input check, this is for development use only
		if(Input.GetKeyUp(KeyCode.Escape)){
			RespawnBall();
		}
	}
	
	void OnGUI(){
		//// Game Messages
		if (three) {
			GUI.Label (new Rect(Screen.width*15/32, Screen.height*10/32,Screen.width*2/32, Screen.height*6/32), threeLabel);
		}else if (two) {
			GUI.Label (new Rect(Screen.width*15/32, Screen.height*10/32,Screen.width*2/32, Screen.height*6/32), twoLabel);
		}else if (one) {
			GUI.Label (new Rect(Screen.width*15/32, Screen.height*10/32,Screen.width*2/32, Screen.height*6/32), oneLabel);
		}else if (start) {
			GUI.Label (new Rect(Screen.width*12/32, Screen.height*10/32,Screen.width*8/32, Screen.height*6/32), startLabel);
			paused = false;
			Time.timeScale = 1f;
		}
	}
	
	// Reset ALL THE THINGS!
	void ResetRound(){
		PauseGame ();
		player_1 = player1.GetComponent<Player1> ();
		player_2 = player2.GetComponent<Player2> ();
		player_1.SendMessage ("ResetRound");
		player_2.SendMessage ("ResetRound");
	}
	
	// Pause the game while we reset for the next round
	void PauseGame(){
		Time.timeScale = 0.0f;
		paused = true;
		StartCoroutine ("DelayedServe");
	}

	void SetBallPos(){
		Vector3 ballSpawn;
		if (serving_player) {
			ballSpawn = new Vector3 (player1.transform.position.x + 1.0f, player1.transform.position.y, player1.transform.position.z - 1);
			ball.transform.position = Vector3.MoveTowards (ball.transform.position, ballSpawn, 2f);
		} else {
			ballSpawn = new Vector3(player2.transform.position.x - 1.0f, player2.transform.position.y, player2.transform.position.z - 1);
			ball.transform.position = Vector3.MoveTowards (ball.transform.position, ballSpawn, 2f);
		}
	}
	
	// Reset both player's positions to their respective spawn points, and reset the
	// local scale so they are facing the correct direction.
	void RespawnPlayers(){
		player1.transform.position = player1_spawn;
		player1.transform.localScale = new Vector3 (2.25f, 2.25f, 1);
		racket_p1.collider2D.enabled = false;
		
		player2.transform.position = player2_spawn;
		player2.transform.localScale = new Vector3 (-2.25f, 2.25f, 1);
		racket_p2.collider2D.enabled = false;
	}
	
	// Reset the ball's position to the opposite player that last served
	void RespawnBall(){
		// Flip the value of the server_player bool to switch who serves next
		serving_player = !serving_player;
		
		// Spawn ball in relation to the player 1's position
		if(serving_player == true){
			ball.position = new Vector3(player1.transform.position.x + 1.0f, player1.transform.position.y, player1.transform.position.z);
			// Reset the ball's velocity
			ball.rigidbody2D.velocity = new Vector2(0, 0);
			// Spawn ball in relation to the player 2's position
		}else{
			ball.position = new Vector3(player2.transform.position.x - 1.0f, player2.transform.position.y, player2.transform.position.z);
			// Reset the ball's velocity
			ball.rigidbody2D.velocity = new Vector2(0, 0);
		}
	}
	
	void Serve() {
		if (serving_player) {
			ballMovement.ServeShot(new Vector2 (8, 7));
		} else {
			ballMovement.ServeShot(new Vector2(-8, 7));
		}
	}

	bool ServeCheck1(){
		if (player_1.playerIsSwinging) {
			if (player_1._playerChip) {
				Serve ();
				return true;
			} else 
			if (player_1._playerPowerRelease) {
				Vector2 forceVector = new Vector2 (8 * player_1.playerPower, 7);
				ballMovement.ServeShot (forceVector);
				return true;
			} else return false;
		} else return false;
	}

	bool ServeCheck2() {
		if (player_2.playerIsSwinging) {
			if (player_2._playerChip) {
				Serve ();
				return true;
			} else 
			if (player_2._playerPowerRelease) {
				Vector2 forceVector = new Vector2 (-8 * player_1.playerPower, 7);
				ballMovement.ServeShot (forceVector);
				return true;
			} else return false;
		} else return false;
	}
		  
	
	// Delay the start of the next round, then start it
	IEnumerator DelayedRoundStart(){
		// Reset timer to original value each round
		float roundStartDelay = 4.0f;
		
		// This is a workaround so we don't depend on WaitForSeconds while paused
		while(paused == true){
			float pauseEndTime = Time.realtimeSinceStartup + 1f;
			while (Time.realtimeSinceStartup < pauseEndTime){
				yield return 0;
			}
			Debug.Log(roundStartDelay);
			
			// Decrement the timer until it is zero, then unpause
			if (roundStartDelay > -2){ // Stop decrementing at -1
				roundStartDelay -= 1; 
			}
			
			if (roundStartDelay == 3) {
				three = true;
			}else if (roundStartDelay == 2) {
				three = false;
				two = true;
			}else if (roundStartDelay == 1) {
				two = false;
				one = true;
			}else if (roundStartDelay == 0) {
				one = false;
				start = true;
			}else{
				start = false;
			}
		}
	}

	IEnumerator DelayedServe () {
		RespawnPlayers ();
		RespawnBall ();
		yield return StartCoroutine ("DelayedRoundStart");
		Debug.Log ("Serve");
		float roundStart = Time.realtimeSinceStartup + 5f;
		bool serve = false;
		while ((Time.realtimeSinceStartup < roundStart) 
		       && serve == false){
			Debug.Log("SERVING");
			if(serving_player){
				serve = ServeCheck1();
				if(serve){
					break;
				}
			}else {
				serve = ServeCheck2();
				if(serve){
					break;
				}
			}
			SetBallPos();
			yield return 0;
		}
		Serve ();
	}
}
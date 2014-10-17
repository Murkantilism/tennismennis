using UnityEngine;
using System.Collections;

public class EndOfRound : MonoBehaviour {

	GameObject player1;
	GameObject player2;
	
	Transform ball;

	Vector3 racket_p1;
	Vector3 racket_p2;

	Vector3 player1_spawn;
	Vector3 player2_spawn;

	bool serving_player = true; // Who's currently serving? true = player 1, false = player 2 | player 1 by default

	float ballSpawnHeight = 2.0f; // How high above the players should the ball spawn?

	public GUISkin guiSkin;

	bool paused = false;

	PlayerMovement playerMovement_p1;
	PlayerMovement playerMovement_p2;
	
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
		ball = GameObject.Find ("Ball").transform;

		racket_p1 = GameObject.Find ("racket_p1").transform.position;
		racket_p2 = GameObject.Find ("racket_p2").transform.position;

		player1_spawn = GameObject.Find ("player1_spawn").transform.position;
		player2_spawn = GameObject.Find ("player2_spawn").transform.position;

		playerMovement_p1 = player1.GetComponent<PlayerMovement> ();
		playerMovement_p2 = player2.GetComponent<PlayerMovement> ();
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
		RespawnPlayers ();
		RespawnBall ();
		playerMovement_p1.SendMessage ("ResetRound");
		playerMovement_p2.SendMessage ("ResetRound");
	}

	// Pause the game while we reset for the next round
	void PauseGame(){
		Time.timeScale = 0.0f;
		paused = true;
		StartCoroutine ("DelayedRoundStart");
	}

	// Reset both player's positions to their respective spawn points, and reset the
	// local scale so they are facing the correct direction.
	void RespawnPlayers(){
		player1.transform.position = player1_spawn;
		player1.transform.localScale = new Vector3 (2.25f, 2.25f, 1);

		player2.transform.position = player2_spawn;
		player2.transform.localScale = new Vector3 (-2.25f, 2.25f, 1);
	}

	// Reset the ball's position to the opposite player that last served
	void RespawnBall(){
		// Flip the value of the server_player bool to switch who serves next
		serving_player = !serving_player;

		// Spawn ball above player 1's racket
		if(serving_player == true){
			ball.position = new Vector3(racket_p1.x, racket_p1.y + ballSpawnHeight, racket_p1.z);
			// Reset the ball's velocity
			ball.rigidbody2D.velocity = new Vector2(0, 0);
		// Spawn ball above player 2's racket
		}else{
			ball.position = new Vector3(racket_p2.x, racket_p2.y + ballSpawnHeight, racket_p2.z);
			// Reset the ball's velocity
			ball.rigidbody2D.velocity = new Vector2(0, 0);
		}
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
			if (roundStartDelay > -2){ // Stop decrementing at -2
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
}

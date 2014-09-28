using UnityEngine;
using System.Collections;

// NOTE: This script must be attached to EACH out-of-bounds object in the scene
public class OutOfBoundsDetection : MonoBehaviour {

	EndOfRound endOfRound;
	ScoreKeeping scoreKeeping;

	public bool leftSideOfCourt; // player 1 = left | Assigned via inspector
	public bool rightSideOfCourt; // player 2 = right | Assigned via inspector

	public bool canPlayersOOB = false; // Can players go out-of-bounds?

	public bool sideAgnosticOOB; // Does this OOB object care about sides?

	// Use this for initialization
	void Start () {
		// Find and assign the scripts OOBDetection needs to talk to
		scoreKeeping = GameObject.Find ("ScoreKeeper").GetComponent<ScoreKeeping> ();
		endOfRound = GameObject.Find ("EndOfRound").GetComponent<EndOfRound> ();

		// An OOB object cannot be on both sides of the court, if so print error
		if (leftSideOfCourt == true && rightSideOfCourt == true) {
			Debug.LogError("ERROR: This OOB object: " + transform.name + ", is marked as being on both sides of the court, you dunce.");
		}

		// If this OOB object has been marked as side agnostic, it cannot have side assignments
		if (sideAgnosticOOB == true){
			if (leftSideOfCourt == true || rightSideOfCourt == true){
				leftSideOfCourt = false;
				rightSideOfCourt = false;
				Debug.LogWarning("WARN: This OOB object: " + transform.name + ", has been marked as side agnostic.");
			}
		}
	}


	// Check if the ball or either player has collided with this OOB object
	void OnTriggerEnter2D(Collider2D col){
		Debug.Log (col.name);
		// If the ball has collided with an OOB object that doesn't care about sides, it 
		// probably went flying somewhere it shouldn't, so reset without awards points.
		if (col.name == "Ball" && sideAgnosticOOB == true){
			ResetRound();
		}

		// If the ball goes out-of-bounds, check which side it went out on
		if (col.name == "Ball") {
			// Left side = player 1, which means player 2 scored
			if(leftSideOfCourt == true){
				PointScored("Player2");
				ResetRound();
			// Right side = player 2, which means player 1 scored
			}else if(rightSideOfCourt == true){
				PointScored("Player1");
				ResetRound();
			// An OOB object cannot be on both sides of the court, if so print error
			}else if (leftSideOfCourt == true && rightSideOfCourt == true) {
				Debug.LogError("ERROR: This Out-Of-Bounds object: " + transform.name + ", is marked as being on both sides of the court, you dunce.");
			}
		// If players can go out-of-bounds, and player 1 has gone OOB
		}else if(canPlayersOOB == true && col.name == "Player1"){
			PointScored("Player2"); // Award point to player 2
			ResetRound();
		// If players can go out-of-bounds, and player 2 has gone OOB
		}else if(canPlayersOOB == true && col.name == "Player2"){
			PointScored("Player1"); // Award point to player 1
			ResetRound();
		}
	}

	// Calls out to ScoreKeeper.cs to increment the winning player's score
	void PointScored(string whoScored){
		scoreKeeping.SendMessage ("PointScored", whoScored);
	}

	// Calls out to EndOfRound.cs to reset everything for the next round
	void ResetRound(){
		endOfRound.SendMessage ("ResetRound");
	}
}

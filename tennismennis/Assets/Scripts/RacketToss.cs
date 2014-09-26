using UnityEngine;
using System.Collections;

// NOTE: This script must be attached to the racket gameObject
public class RacketToss : MonoBehaviour {

	Vector3[] path; // The path the racket toss will follow

	public GameObject endMarker; // The end marker
	public GameObject originalRacketPosMarker; // The original racket's position
	
	private float racketThrowDist = 3.0f; // The max distance the racket can go
	float tossSpeed = 7.5f; // The racket toss speed

	public bool being_thrown = false; // Is this racket being thrown?

	public bool _player1 = false; // Is this player 1's racket? Set via inspector
	public bool _player2 = false; // Is this player 2's racket? Set via inspector

	PlayerMovement playerMovement1;
	PlayerMovement playerMovement2;

	// Use this for initialization
	void Start () {
		// Save the racket's original position
		originalRacketPosMarker.transform.position = transform.position;

		// Set the end marker's position
		endMarker.transform.position = new Vector3 (transform.position.x + racketThrowDist, transform.position.y, transform.position.z);

		playerMovement1 = GameObject.Find ("Player1").GetComponent<PlayerMovement> ();
		playerMovement2 = GameObject.Find("Player2").GetComponent<PlayerMovement>();

		// Define path array with max length of 2 members
		path = new Vector3[2];
		// Set the first member of the array to the endMarker
		path[0] = endMarker.transform.position;
		// Set the second member of the array to the original racket's position
		path[1] = originalRacketPosMarker.transform.position;
	}

	// Update is called once per frame
	void Update () {
		// Update the path array
		path[0] = endMarker.transform.position;
		path[1] = originalRacketPosMarker.transform.position;
		// If player 1 throws racket, throw it
		if(_player1 == true &&(Input.GetAxis("P1_Throw") > 0)){
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				// TODO: Some call to freeze position
				StartCoroutine (ThrowRacket (false));
			}
		}

		// If player 2 throws racket, throw it
		if (_player2 == true && (Input.GetAxis ("P2_Throw") > 0)) {
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				// TODO: Some call to freeze position
				StartCoroutine (ThrowRacket (false));
			}
		}
	}

	// Freeze the player (helper function) then walk through the array of targets (only two positions) and move the racket
	IEnumerator ThrowRacket(bool loop){
		// First freeze the player's X position
		FreezePlayer ();
		do {
				foreach (Vector3 point in path) {
						yield return StartCoroutine (MoveRacketToPosition (point));
				}
		} while(loop);
		being_thrown = false; // Reset after throw finished
		UnfreeezePlayer ();
	}

	// Send a call to PlayerMovement.cs to freeze the player's X position
	void FreezePlayer(){
		if(_player1 == true){
			playerMovement1.SendMessage("FreezePlayer");
		}else if(_player2 == true){
			playerMovement2.SendMessage("FreezePlayer");
		}
	}

	// Send a call to PlayerMovement.cs to unfreeze the player's x posotion
	void UnfreeezePlayer(){
		if(_player1 == true){
			playerMovement1.SendMessage("UnfreezePlayer");
		}else if(_player2 == true){
			playerMovement2.SendMessage("UnfreeezePlayer");
		}
	}

	// Move the racket to the given position
	IEnumerator MoveRacketToPosition(Vector3 target){
		while(transform.transform.position != target){
			if(being_thrown == true){
				transform.position = Vector3.MoveTowards(transform.position, target, tossSpeed * Time.deltaTime);
			}
			yield return 0;
		}
		// Invoke the redundant return method to ensure racket is returned
		InvokeRepeating ("ReturnRacket", 0.5f, 0.25f);
	}

	// A redundant method just to make sure the racket is returned to the correct position
	// TODO: Check if this method is necessary now that (as of 09/25/14) player movement in 
	// X direction (and jumping) is disabled. The racket should (theorhetically) return to the hand A-Okay now.
	void ReturnRacket(){
		// If the racket isn't at the original position where it should be
		if (transform.position != originalRacketPosMarker.transform.position) {
			// move it there
			transform.position = originalRacketPosMarker.transform.position;
		}
		// Once this method has run, cancel all invocation calls on this MonoBehaviour
		CancelInvoke();
	}
}

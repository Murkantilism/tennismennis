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

	// Use this for initialization
	void Start () {
		// Save the racket's original position
		originalRacketPosMarker.transform.position = transform.position;

		// Set the end marker's position
		endMarker.transform.position = new Vector3 (transform.position.x + racketThrowDist, transform.position.y, transform.position.z);

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
		if(_player1 && Input.GetKeyUp(KeyCode.R)){
			// Update the path array //NOTE: These are commented out in order to allow player
									 //movement during racket toss (not perfect). Uncomment
									 //this and below p2 path array update, comment out above
									 //in order to reverse this effect.
			//path[0] = endMarker.transform.position;
			//path[1] = originalRacketPosMarker.transform.position;

			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				StartCoroutine (ThrowRacket (false));
			}
		}

		// If player 2 throws racket, throw it
		if(_player2 && Input.GetKeyUp(KeyCode.RightControl)){
			// Update the path array
			//path[0] = endMarker.transform.position;
			//path[1] = originalRacketPosMarker.transform.position;

			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				StartCoroutine (ThrowRacket (false));
			}
		}
	}

	// Walk through the array of targets (only two positions) and move the racket
	IEnumerator ThrowRacket(bool loop){
		do {
				foreach (Vector3 point in path) {
						yield return StartCoroutine (MoveRacketToPosition (point));
				}
		} while(loop);
		being_thrown = false;
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

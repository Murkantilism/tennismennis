using UnityEngine;
using System.Collections;
using InControl;

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

	PlayerMovement playerMovement;

	InputDevice inputDevice;

	bool keyboard_input_enabled = true;
	
	MennisMeter mennisMeter_p1;
	MennisMeter mennisMeter_p2;
	
	private Animator _animator;

	#region Event Listeners
	// Listen for the input controls setup by PlayerControls.cs
	public void GetInputSettings(bool b){
		keyboard_input_enabled = b;
	}
	#endregion

	// Use this for initialization
	void Start () {
		// Save the racket's original position
		originalRacketPosMarker.transform.position = transform.position;

		// Set the end marker's position
		endMarker.transform.position = new Vector3 (transform.position.x + racketThrowDist, transform.position.y, transform.position.z);

		if(_player1 == true){
			playerMovement = GameObject.Find ("Player1").GetComponent<PlayerMovement> ();
		}else if(_player2 == true){
			playerMovement = GameObject.Find("Player2").GetComponent<PlayerMovement>();
		}

		// Define path array with max length of 2 members
		path = new Vector3[2];
		// Set the first member of the array to the endMarker
		path[0] = endMarker.transform.position;
		// Set the second member of the array to the original racket's position
		path[1] = originalRacketPosMarker.transform.position;
		
		mennisMeter_p1 = GameObject.Find("MennisMeter_p1").GetComponent<MennisMeter>();
		mennisMeter_p2 = GameObject.Find("MennisMeter_p2").GetComponent<MennisMeter>();
		
		if(_player1 == true){
			_animator = GameObject.Find("Player1").GetComponent<Animator>();
		}else if(_player2 == true){
			_animator = GameObject.Find("Player2").GetComponent<Animator>();
		}
	}

	// Update is called once per frame
	void Update () {
		if(keyboard_input_enabled == true){
			KeyboardInput();
		}else if(keyboard_input_enabled == false){
			ControllerInput();
		}

		// Update the path array
		path[0] = endMarker.transform.position;
		path[1] = originalRacketPosMarker.transform.position;
	}

	// ======================= \\
	// ***KEYBOARD INPUTS***   \\
	// ======================= \\
	void KeyboardInput(){
		// If player 1 throws the racket at a high angle, throw it high
		if (_player1 == true && (Input.GetAxisRaw ("P1_Throw_High") > 0)) {
			Debug.Log("P1 THROW RACKET HIGH");
			playerMovement.SendMessage("RacketToss", true);
			_animator.Play( Animator.StringToHash("RacketToss_High"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter_p1.DecrementMennis_racketToss();
				Debug.Log("P1 RACKET THROW");
				StartCoroutine (ThrowRacket (false));
			}
		// If player 1 throws the racket at a straight angle, throw it straight
		}else if (_player1 == true && (Input.GetAxisRaw ("P1_Throw_Straight") > 0)) {
			//Debug.Log("P1 THROW RACKET STRAIGHT");
			playerMovement.SendMessage("RacketToss", true);
			_animator.Play( Animator.StringToHash("RacketToss_Straight"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter_p1.DecrementMennis_racketToss();
				Debug.Log("P1 RACKET THROW");
				StartCoroutine (ThrowRacket (false));
			}
		// If player 1 throws the racket at a low angle, throw it low
		}else if (_player1 == true && (Input.GetAxisRaw ("P1_Throw_Low") > 0)) {
			Debug.Log("P1 THROW RACKET LOW");
			playerMovement.SendMessage("RacketToss", true);
			_animator.Play( Animator.StringToHash("RacketToss_Low"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter_p1.DecrementMennis_racketToss();
				Debug.Log("P1 RACKET THROW");
				StartCoroutine (ThrowRacket (false));
			}
		}
		
		// If player 2 throws the racket at a high angle, throw it high
		if (_player2 == true && (Input.GetAxisRaw ("P2_Throw_High") > 0)) {
			Debug.Log("P2 THROW RACKET HIGH");
			playerMovement.SendMessage("RacketToss", true);
			_animator.Play( Animator.StringToHash("RacketToss_High"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter_p2.DecrementMennis_racketToss();
				Debug.Log("P2 RACKET THROW");
				StartCoroutine (ThrowRacket (false));
			}
		// If player 2 throws the racket at a straight angle, throw it straight
		}else if (_player2 == true && (Input.GetAxisRaw ("P2_Throw_Straight") > 0)) {
			//Debug.Log("P2 THROW RACKET STRAIGHT");
			playerMovement.SendMessage("RacketToss", true);
			_animator.Play( Animator.StringToHash("RacketToss_Straight"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter_p2.DecrementMennis_racketToss();
				Debug.Log("P2 RACKET THROW");
				StartCoroutine (ThrowRacket (false));
			}
		// If player 2 throws the racket at a low angle, throw it low
		}else if (_player2 == true && (Input.GetAxisRaw ("P2_Throw_Low") > 0)) {
			Debug.Log("P2 THROW RACKET LOW");
			playerMovement.SendMessage("RacketToss", true);
			_animator.Play( Animator.StringToHash("RacketToss_Low"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter_p2.DecrementMennis_racketToss();
				Debug.Log("P2 RACKET THROW");
				StartCoroutine (ThrowRacket (false));
			}
		}
	}

	// ======================= \\
	// ***CONTROLLER INPUTS*** \\
	// ======================= \\
	void ControllerInput(){
		// Setup the controller inputs
		if(_player1 == true){
			inputDevice = InputManager.Devices [0];
		}else if(_player2 == true){
			inputDevice = InputManager.Devices [1];
		}

		// If player 1 sets the racket angle straight, set it straight (RIGHT on right stick)
		if(_player1 == true && (inputDevice.RightStickX > 0.7)){
			Debug.Log("P1 RACKET SET TO STRAIGHT");
			
			// If player 1 sets the racket angle high, set it high (UP on right stick)
		}else if(_player1 == true && (inputDevice.RightStickY > 0.7)){
			Debug.Log("P1 RACKET SET TO HIGH");
			
			// If player 1 sets the racket angle low, set it low (down on right stick)
		}else if(_player1 == true && (inputDevice.RightStickY < -0.7)){
			Debug.Log("P1 RACKET SET TO LOW");
		}
		
		// If player 2 sets the racket angle straight, set it straight (RIGHT on right stick)
		if(_player2 == true && (inputDevice.RightStickX > 0.7)){
			Debug.Log("P2 RACKET SET TO STRAIGHT");
			
			// If player 2 sets the racket angle high, set it high (UP on right stick)
		}else if(_player2 == true && (inputDevice.RightStickY > 0.7)){
			Debug.Log("P2 RACKET SET TO HIGH");
			
			// If player 1 sets the racket angle low, set it low (down on right stick)
		}else if(_player2 == true && (inputDevice.RightStickY < -0.7)){
			Debug.Log("P2 RACKET SET TO LOW");
		}
		
		// If player 1 throws racket, throw it
		if(_player1 == true && (inputDevice.RightBumper > 0)){
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter_p1.DecrementMennis_racketToss();
				Debug.Log("P1 RACKET THROW");
				StartCoroutine (ThrowRacket (false));
			}
		}
		
		// If player 2 throws racket, throw it
		if(_player2 == true && (inputDevice.RightBumper > 0)){
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter_p2.DecrementMennis_racketToss();
				Debug.Log("P2 RACKET THROW");
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
		playerMovement.SendMessage("FreezePlayer");
	}

	// Send a call to PlayerMovement.cs to unfreeze the player's x posotion
	void UnfreeezePlayer(){
		playerMovement.SendMessage("UnfreezePlayer");
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

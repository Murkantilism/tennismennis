﻿using UnityEngine;
using System.Collections;
using InControl;

public class PlayerMovement : MonoBehaviour {

	// Movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 100f;
	public float jumpHeight = 3f;
	
	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;
	
	private CharacterController2D _controller;
	public Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
	
	// input bools
	public bool _right;
	public bool _left;
	public bool _up;

	public bool _input = true; // True = keyboard, false = controller
	
	public bool _player1 = false; // Is this player 1? Set via inspector
	public bool _player2 = false; // Is this player 2? Set via inspector


	// Player swing vars
	private bool _playerChip;

	private bool _playerPowerCharge;

	private bool _playerPowerRelease;

	public bool playerIsSwinging;
	
	public bool powerHitterEnabled = false;

	// Power shot variables

	// Once the player releases the power shot, this variable saves the amount of 
	// power stored
	public float _playerPower = .5f;

	private float chargeRate = .75f;

	// Player racket vars
	private GameObject racket1;
	private GameObject racket2;

	private bool freezePlayerp = false;

	InputDevice inputDevice;

	bool keyboard_input_enabled;
	
	public bool racketBeingTossed = false;
	
	void Awake(){
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
		
		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;

		// Check if any controllers are connected, if not set boolean to keyboard
		if(Input.GetJoystickNames().Length == 0){
			_input = true; // Keyboard connected
		}else{
			_input = false; // Controller connected
		}

		// Find & assign racket vars
		racket1 = GameObject.Find("Player1").transform.GetChild(0).gameObject;
		racket2 = GameObject.Find("Player2").transform.GetChild(0).gameObject;
		racket1.collider2D.enabled = false;
		racket2.collider2D.enabled = false;

		// Players are not swinging by default
		playerIsSwinging = false;
		
		if(_player2 == true){
			transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
		}
	}

	// Reset the swing booleans
	IEnumerator ToggleSwing(GameObject racket){
		yield return new WaitForSeconds(0.5f);
		playerIsSwinging = false;
		_playerPower = 0.5f;
		racket.collider2D.enabled = false;
	}

	#region Event Listeners
	
	void onControllerCollider( RaycastHit2D hit ){
		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}
	
	void onTriggerEnterEvent( Collider2D col ){
		//Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}

	void onTriggerExitEvent( Collider2D col ){
		//Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	// Freeze the player's x-position
	void FreezePlayer(){
		freezePlayerp = true;
	}
	// Unfreeze the player's x-postion
	void UnfreezePlayer(){
		freezePlayerp = false;
	}

	// Listen for the input controls setup by PlayerControls.cs
	void GetInputSettings(bool b){
		keyboard_input_enabled = b;
	}
	// Listen for the RacketToss.cs to control movement animations during racket toss
	void RacketToss(bool b){
		//Debug.Log("Player 1: " + _player1 + " Player 2: " + _player2 + " Racket being tossed: " + racketBeingTossed);
		racketBeingTossed = b;
	}
	
	#endregion
	
	
	// the Update loop only gathers input. Actual movement is handled in FixedUpdate because we are using the Physics system for movement
	void Update(){
		if(keyboard_input_enabled == true){
			KeyboardInput();
		}else if(keyboard_input_enabled == false){
			ControllerInput();
		}
	}

	// ======================= \\
	// ***CONTROLLER INPUTS*** \\
	// ======================= \\
	void ControllerInput(){
		// Setup the controller inputs
		try{
			if(_player1 == true){
				inputDevice = InputManager.Devices [0];
			}else if(_player2 == true){
				inputDevice = InputManager.Devices [1];
			}
		}catch(UnityException e){
			Debug.LogWarning(e.ToString());
		}

		_up = _up || ((inputDevice.LeftStickY) > 0.5);
		_right = ((inputDevice.LeftStickX) > 0.5);
		_left = ((inputDevice.LeftStickX) < -0.5);
		
		// If player 1 hits right trigger, perform a quick racket swing
		if(_player1 == true){
			_playerChip = inputDevice.RightTrigger > 0;
			_playerPowerCharge = inputDevice.LeftTrigger > 0;
			// _playerPowerRelease = inputDevice.LeftTrigger.WasReleased;
			// If player 2 hits right trigger, perform a quick racket swing
		}else if(_player2 == true){
			_playerChip = inputDevice.RightTrigger > 0;
			_playerPowerCharge = inputDevice.LeftTrigger > 0;
			// _playerPowerRelease = inputDevice.LeftTrigger.WasReleased;
		}
	}

	// ======================= \\
	//  ***KEYBOARD INPUT***   \\
	// ======================= \\
	void KeyboardInput(){
		if(_player1 == true){
			_up = _up || (Input.GetAxisRaw ( "P1_Vertical" ) > 0);
			_right = (Input.GetAxisRaw ( "P1_Horizontal" ) > 0);
			_left = (Input.GetAxisRaw ( "P1_Horizontal" ) < 0);
			_playerChip = Input.GetKey (KeyCode.E);
			_playerPowerCharge = Input.GetKey (KeyCode.Q);
			_playerPowerRelease = Input.GetKeyUp (KeyCode.Q);
		}else if(_player2 == true){
			_up = _up || (Input.GetAxisRaw ( "P2_Vertical" ) > 0);
			_right = (Input.GetAxisRaw ( "P2_Horizontal" ) > 0);
			_left = (Input.GetAxisRaw ( "P2_Horizontal" ) < 0);
			_playerChip = Input.GetKey (KeyCode.O);
			_playerPowerCharge = Input.GetKey (KeyCode.LeftBracket);
			_playerPowerRelease = Input.GetKey (KeyCode.LeftBracket);
		}
	}

	// When a point is scored, reset all of the input vars, the normalized horizontal speed, and 
	// the local scale. This prevents movement inputs from being carried over into the next round
	void ResetRound(){
		_up = false;
		_right = false;
		_left = false;
		_playerChip = false;
		normalizedHorizontalSpeed = 0;
		transform.localScale = new Vector3 (2.25f, 2.25f, 1);
	}
	
	void FixedUpdate(){
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

		if( _controller.isGrounded )
			_velocity.y = 0;

		// If the right movement is input, and the player isn't throwing his racket (frozen)
		if( _right && freezePlayerp == false){
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				if(_player1 == true){
					transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
				}
			
			if( _controller.isGrounded )
				if(_player1 == true){
						_animator.Play( Animator.StringToHash( "Run_forward" ) );
				}else if(_player2 == true){
					_animator.Play( Animator.StringToHash( "Run_backward" ) );
				}

		// If the left movement is input, and the player isn't throwing his racket (frozen)
		}else if( _left && freezePlayerp == false){
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				if(_player2 == true){
					transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
				}
			
			if( _controller.isGrounded )
				if(_player1 == true){
					_animator.Play( Animator.StringToHash( "Run_backward" ) );
				}else if(_player2 == true){
					_animator.Play( Animator.StringToHash( "Run_forward" ) );
				}
		}else{
			normalizedHorizontalSpeed = 0;

			if( _controller.isGrounded && racketBeingTossed == false && playerIsSwinging == false )
				_animator.Play( Animator.StringToHash( "Idle" ) );
		}

		// If the swing input is hit by either player, and that player isn't already swinging:
		// -Set the swinging bool to true
		// -Enable the collider on the racket
		// -Trigger a coroutine which will eventually reset the swinging bool and collider
		if(_playerChip && playerIsSwinging == false && _player1 == true) {
			Debug.Log("Player 1 racket swung");
			playerIsSwinging = true;
			racket1.collider2D.enabled = true;
			_playerPower = 1.0f;
			_animator.Play( Animator.StringToHash( "RacketSwing_Forward" ) );
			StartCoroutine(ToggleSwing(racket1));
		}else if(_playerChip && playerIsSwinging == false && _player2 == true) {
			Debug.Log("Player 2 racket swung");
			playerIsSwinging = true;
			racket2.collider2D.enabled = true;
			_playerPower = 1.0f;
			StartCoroutine(ToggleSwing(racket2));
		}

		if(_playerPowerCharge && playerIsSwinging == false) {
			if(_playerPower < 1.5f){
				_playerPower += chargeRate * Time.deltaTime;
			}
			Debug.Log("Player 1 Power: " + _playerPower);
		}
		if(_playerPowerRelease && playerIsSwinging == false && _player1 == true) {
			Debug.Log ("Player 1 CHARGE: " + _player1Power);
			player1IsSwinging = true;
			racket1.collider2D.enabled = true;
			StartCoroutine(ToggleSwing(racket1));
		}
		}
		if(Input.GetKeyUp(KeyCode.BackQuote) && player2IsSwinging == false) {
			player2IsSwinging = true;
			racket2.collider2D.enabled = true;
			_animator.Play( Animator.StringToHash( "RacketSwing_Forward" ) );
			StartCoroutine(ToggleSwing(2, racket2));
		}*/

		// we can only jump whilst grounded, and the player isn't throwing his racket (frozen)
		if( _controller.isGrounded && _up  && freezePlayerp == false){
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );

			//Debug.Log("Velocity " + _velocity.y);
			//Debug.Log(Mathf.Sqrt( 2f * jumpHeight * -gravity ));
			_animator.Play( Animator.StringToHash( "Jump" ) );
		}
		// reset input
		_up = false;
		
		racketBeingTossed = false;
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.fixedDeltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		_velocity.y += gravity * Time.fixedDeltaTime;
		
		_controller.move( _velocity * Time.fixedDeltaTime );
		

	}
}

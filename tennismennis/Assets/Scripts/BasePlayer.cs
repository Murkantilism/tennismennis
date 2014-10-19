using UnityEngine;
using System.Collections;

public abstract class BasePlayer : MonoBehaviour{
	public GameObject playerObj;
	public CharacterController2D _controller;
	public Animator _animator;

	// Movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 100f;
	public float jumpHeight = 3f;
	
	public float normalizedHorizontalSpeed = 0;
	private RaycastHit2D _lastControllerColliderHit;
	public Vector3 _velocity;
	
	// Input bools
	public bool _right;
	public bool _left;
	public bool _up;
	
	bool _input = true; // True = keyboard, false = controller
	
	bool keyboard_input_enabled;
	
	public bool freezePlayerp = false;
	
	// Player swing vars
	public bool _playerChip;
	private bool _playerPowerShot;
	public bool playerIsSwinging = false;
	
	public bool racketBeingTossed = false;
	
	// Power-ups
	public bool powerHitterEnabled = false;
	
	// Use this for initialization
	void Start () {
		// Find & assign the required gameObjects
		playerObj = gameObject;
		_controller = playerObj.GetComponent<CharacterController2D>();
		_animator = playerObj.GetComponent<Animator>();
		
		// Check if any controllers are connected, if not set boolean to keyboard
		if(Input.GetJoystickNames().Length == 0){
			_input = true; // Keyboard connected
		}else{
			_input = false; // Controller connected
		}
	}
	
	// Find & assign the player gameObject
	public void GetPlayerObject() {
		if (playerObj == null) {
			playerObj = gameObject;
		}
	}
	
	// Find & assign the player controller
	public void GetPlayerController() {
		if (_controller == null) {
			_controller = playerObj.GetComponent<CharacterController2D>();
		}
	}
		
	public void Jump(){
		GetPlayerObject();
		GetPlayerController();
	}
	
	public void RacketSwing(){
		
	}
	
	public void RacketToss(){
		
	}
	
	// When a point is scored, reset all of the input vars, the normalized horizontal speed, and 
	// the local scale. This prevents movement inputs from being carried over into the next round
	public void ResetRound(){
		_up = false;
		_right = false;
		_left = false;
		_playerChip = false;
		normalizedHorizontalSpeed = 0;
	}
	
	// ToggleSwing method
	public IEnumerator ToggleSwing(){
		yield return new WaitForSeconds(0.5f);
		playerIsSwinging = false;
		//racket.collider2D.enabled = false;
	}
	
	// Abstract input method
	abstract public void SetInput();
	
	// Abstract move method
	public abstract void Move();
}

class Player1 : BasePlayer{

	// Override input method
	public override void SetInput(){
		_up = _up || (Input.GetAxisRaw ( "P1_Vertical" ) > 0);
		_right = (Input.GetAxisRaw ( "P1_Horizontal" ) > 0);
		_left = (Input.GetAxisRaw ( "P1_Horizontal" ) < 0);
		_playerChip = Input.GetKey (KeyCode.E);
	}
	
	public override void Move(){
		GetPlayerObject();
		GetPlayerController();
		
		// Move this player
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
		
		if( _controller.isGrounded )
			_velocity.y = 0;
		
		// If the right movement is input, and the player isn't throwing his racket (frozen)
		if( _right && freezePlayerp == false){
			normalizedHorizontalSpeed = 1;
			
			if( transform.localScale.x < 0f ){
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			}
			if( _controller.isGrounded ){
				_animator.Play( Animator.StringToHash( "Run_forward" ) );
			}
			
		// If the left movement is input, and the player isn't throwing his racket (frozen)
		}else if( _left && freezePlayerp == false){
			normalizedHorizontalSpeed = -1;
			
			if( _controller.isGrounded ){
				_animator.Play( Animator.StringToHash( "Run_backward" ) );
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
		if(_playerChip && playerIsSwinging == false) {
			Debug.Log("Player 1 racket swung");
			playerIsSwinging = true;
			//racket1.collider2D.enabled = true;
			_animator.Play( Animator.StringToHash( "RacketSwing_Forward" ) );
			StartCoroutine(ToggleSwing());
		}
		
		// we can only jump whilst grounded, and the player isn't throwing his racket (frozen)
		if( _controller.isGrounded && _up  && freezePlayerp == false){
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
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
	
	void FixedUpdate(){
		SetInput();
		Move ();
	}
}

class Player2 : BasePlayer{
	void Start(){
		transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
		GetPlayerObject();
		GetPlayerController();
		
		playerObj = gameObject;
		_controller = playerObj.GetComponent<CharacterController2D>();
		_animator = playerObj.GetComponent<Animator>();
	}
	
	// Override input method
	public override void SetInput(){
		_up = _up || (Input.GetAxisRaw ( "P2_Vertical" ) > 0);
		_right = (Input.GetAxisRaw ( "P2_Horizontal" ) > 0);
		_left = (Input.GetAxisRaw ( "P2_Horizontal" ) < 0);
		_playerChip = Input.GetKey (KeyCode.O);
	}
	
	public override void Move(){
		GetPlayerObject();
		GetPlayerController();
		
		// Move this player
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
		
		if( _controller.isGrounded )
			_velocity.y = 0;
		
		// If the right movement is input, and the player isn't throwing his racket (frozen)
		if( _right && freezePlayerp == false){
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			if( _controller.isGrounded ){
				_animator.Play( Animator.StringToHash( "Run_backward" ) );
			}
			
			// If the left movement is input, and the player isn't throwing his racket (frozen)
		}else if( _left && freezePlayerp == false){
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f ){
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			}
			
			if( _controller.isGrounded ){
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
		if(_playerChip && playerIsSwinging == false) {
			Debug.Log("Player 2 racket swung");
			playerIsSwinging = true;
			//racket1.collider2D.enabled = true;
			_animator.Play( Animator.StringToHash( "RacketSwing_Forward" ) );
			StartCoroutine(ToggleSwing());
		}
		
		// we can only jump whilst grounded, and the player isn't throwing his racket (frozen)
		if( _controller.isGrounded && _up  && freezePlayerp == false){
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
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
	
	void FixedUpdate(){
		SetInput();
		Move ();
	}
}

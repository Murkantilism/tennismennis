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
	
	// Racket Toss variables
	public Vector3[] path; // The path the racket toss will follow
	
	public GameObject endMarker; // The end marker
	public GameObject originalRacketPosMarker; // The original racket's position
	
	public float racketThrowDist = 3.0f; // The max distance the racket can go
	public float tossSpeed = 7.5f; // The racket toss speed
	
	public bool being_thrown = false; // Is this racket being thrown?
	
	public MennisMeter mennisMeter;
	
	public GameObject racket;
	
	public SaveSelections savedSelections;
	
	public AudioSource asrc;
	
	public AudioClip grunt0;
	public AudioClip grunt1;
	public AudioClip taunt;
	public float timer;
	public bool player_taunted = false;
	
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
	
	// Freeze the player (helper function) then walk through the array of targets (only two positions) and move the racket
	public IEnumerator ThrowRacket(bool loop){
		// First freeze the player's X position
		freezePlayerp = true;
		do {
			foreach (Vector3 point in path) {
				yield return StartCoroutine (MoveRacketToPosition (point));
			}
		} while(loop);
		being_thrown = false; // Reset after throw finished
		freezePlayerp = false;
	}
	
	// Move the racket to the given position
	public IEnumerator MoveRacketToPosition(Vector3 target){
		Debug.Log(Time.time);
		while(racket.transform.transform.position != target){
			if(being_thrown == true){
				racket.transform.position = Vector3.MoveTowards(racket.transform.position, target, tossSpeed * Time.deltaTime);
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
		if (racket.transform.position != originalRacketPosMarker.transform.position) {
			// move it there
			racket.transform.position = originalRacketPosMarker.transform.position;
		}
		Debug.Log(Time.time);
		// Once this method has run, cancel all invocation calls on this MonoBehaviour
		CancelInvoke();
	}
	
	void Awake(){
		asrc = gameObject.GetComponentInChildren<AudioSource>();
		
		savedSelections = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();
	}
	
	// Abstract input method
	abstract public void SetInput();
	
	// Abstract move method
	public abstract void Move();
	
	// Abstract racket Swing method
	public abstract void Swing();
	
	
}

class Player1 : BasePlayer{
	
	void Start(){
		playerObj = gameObject;
		_controller = playerObj.GetComponent<CharacterController2D>();
		_animator = playerObj.GetComponent<Animator>();
		Debug.Log(_animator.ToString() + "   animator object");
		
		racket = GameObject.Find("racket_p1");
		originalRacketPosMarker = transform.Find("originalRacketPos").gameObject;
		endMarker = transform.Find("endMarker").gameObject;
		
		// Save the racket's original position
		originalRacketPosMarker.transform.position = racket.transform.position;
		
		// Set the end marker's position
		endMarker.transform.position = new Vector3 (racket.transform.position.x + racketThrowDist, racket.transform.position.y, racket.transform.position.z);
		
		// Define path array with max length of 2 members
		path = new Vector3[2];
		// Set the first member of the array to the endMarker
		path[0] = endMarker.transform.position;
		// Set the second member of the array to the original racket's position
		path[1] = originalRacketPosMarker.transform.position;
		
		mennisMeter = GameObject.Find("MennisMeter_p1").GetComponent<MennisMeter>();
		
		// Load the grunt SFX for player 1
		if(grunt0 == null){
			if(savedSelections.selected_p1 == "S. Racks"){
				grunt0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_grunt0", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "SH1-V4"){
				grunt0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_grunt0", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "Colonel Topspin"){
				grunt0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_grunt0", typeof(AudioClip)));
			}else{
				grunt0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_grunt0", typeof(AudioClip)));
			}
		}
		if(grunt1 == null){
			if(savedSelections.selected_p1 == "S. Racks"){
				grunt1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_grunt1", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "SH1-V4"){
				grunt1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_grunt1", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "Colonel Topspin"){
				grunt1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_grunt1", typeof(AudioClip)));
			}else{
				grunt1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_grunt1", typeof(AudioClip)));
			}
		}
		
		// Load the taunt SFX for player 1
		if(taunt == null){
			if(savedSelections.selected_p1 == "S. Racks"){
				taunt = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_Taunt", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "SH1-V4"){
				taunt = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_Taunt", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "Colonel Topspin"){
				taunt = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_Taunt", typeof(AudioClip)));
			}else{
				taunt = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_Taunt", typeof(AudioClip)));
			}
		}
		// Player 1 taunts right away
		asrc.PlayOneShot(taunt, 1.0f);
	}
	
	// Override input method
	public override void SetInput(){
		_up = _up || (Input.GetAxisRaw ( "P1_Vertical" ) > 0);
		_right = (Input.GetAxisRaw ( "P1_Horizontal" ) > 0);
		_left = (Input.GetAxisRaw ( "P1_Horizontal" ) < 0);
		_playerChip = (Input.GetAxisRaw ("P1_Swing") > 0);
		
		// If player 1 throws the racket at a high angle, throw it high
		if (Input.GetAxisRaw ("P1_Throw_High") > 0) {
			Debug.Log("P1 THROW RACKET HIGH");
			racketBeingTossed = true;
			_animator.Play( Animator.StringToHash("RacketToss_High"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter.DecrementMennis_racketToss();
				Debug.Log("P1 RACKET THROW");
				racket.collider2D.enabled = true; // Enable the racket collider during toss
				StartCoroutine (ThrowRacket (false));
			}
			// If player 1 throws the racket at a straight angle, throw it straight
		}else if (Input.GetAxisRaw ("P1_Throw_Straight") > 0) {
			//Debug.Log("P1 THROW RACKET STRAIGHT");
			racketBeingTossed = true;
			_animator.Play( Animator.StringToHash("RacketToss_Straight"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter.DecrementMennis_racketToss();
				Debug.Log("P1 RACKET THROW");
				racket.collider2D.enabled = true; // Enable the racket collider during toss
				StartCoroutine (ThrowRacket (false));
			}
			// If player 1 throws the racket at a low angle, throw it low
		}else if (Input.GetAxisRaw ("P1_Throw_Low") > 0) {
			Debug.Log("P1 THROW RACKET LOW");
			racketBeingTossed = true;
			_animator.Play( Animator.StringToHash("RacketToss_Low"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter.DecrementMennis_racketToss();
				Debug.Log("P1 RACKET THROW");
				racket.collider2D.enabled = true; // Enable the racket collider during toss
				StartCoroutine (ThrowRacket (false));
			}
		}
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
	
	public override void Swing(){
		// If the swing input is hit by either player, and that player isn't already swinging:
		// -Set the swinging bool to true
		// -Enable the collider on the racket
		// -Trigger a coroutine which will eventually reset the swinging bool and collider
		if(_playerChip && playerIsSwinging == false) {
			Debug.Log("Player 1 racket swung");
			playerIsSwinging = true;
			racket.collider2D.enabled = true;
			_animator.Play( Animator.StringToHash( "RacketSwing_Forward" ) );
			asrc.PlayOneShot(grunt0, 1.0f);
			StartCoroutine(ToggleSwing());
		}
		
		
		if(being_thrown && playerIsSwinging == false){
			Debug.Log("Player 1 racket toss");
			playerIsSwinging = true;
			racket.collider2D.enabled = true;
			_animator.Play( Animator.StringToHash( "RacketToss_Straight" ) );
			asrc.PlayOneShot(grunt1, 1.0f);
			StartCoroutine(ToggleSwing());
		}
	}
	
	void FixedUpdate(){
		SetInput();
		Move ();
		Swing();
		
		// Update the path array
		path[0] = endMarker.transform.position;
		path[1] = originalRacketPosMarker.transform.position;
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
		
		racket = GameObject.Find("racket_p2");
		originalRacketPosMarker = transform.Find("originalRacketPos").gameObject;
		endMarker = transform.Find("endMarker").gameObject;
		
		// Save the racket's original position
		originalRacketPosMarker.transform.position = racket.transform.position;
		
		// Set the end marker's position
		endMarker.transform.position = new Vector3 (racket.transform.position.x - racketThrowDist, racket.transform.position.y, racket.transform.position.z);
		
		// Define path array with max length of 2 members
		path = new Vector3[2];
		// Set the first member of the array to the endMarker
		path[0] = endMarker.transform.position;
		// Set the second member of the array to the original racket's position
		path[1] = originalRacketPosMarker.transform.position;
		
		mennisMeter = GameObject.Find("MennisMeter_p2").GetComponent<MennisMeter>();
		
		// Load the grunt SFX for player 2
		if(grunt0 == null){
			if(savedSelections.selected_p2 == "S. Racks"){
				grunt0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_grunt0", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "SH1-V4"){
				grunt0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_grunt0", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "Colonel Topspin"){
				grunt0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_grunt0", typeof(AudioClip)));
			}else{
				grunt0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_grunt0", typeof(AudioClip)));
			}
		}
		if(grunt1 == null){
			if(savedSelections.selected_p2 == "S. Racks"){
				grunt1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_grunt1", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "SH1-V4"){
				grunt1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_grunt1", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "Colonel Topspin"){
				grunt1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_grunt1", typeof(AudioClip)));
			}else{
				grunt1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_grunt1", typeof(AudioClip)));
			}
		}
		
		// Load the taunt SFX for player 2
		if(taunt == null){
			if(savedSelections.selected_p2 == "S. Racks"){
				taunt = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_Taunt", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "SH1-V4"){
				taunt = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_Taunt", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "Colonel Topspin"){
				taunt = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_Taunt", typeof(AudioClip)));
			}else{
				taunt = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_Taunt", typeof(AudioClip)));
			}
		}

		StartCoroutine(Player2Taunt());
	}
	
	IEnumerator Player2Taunt(){
		yield return new WaitForSeconds(0.01f);
		if(player_taunted == false){
			player_taunted = true;
			asrc.PlayOneShot(taunt, 1.0f);
		}
		yield return 0;
	}
	
	// Override input method
	public override void SetInput(){
		_up = _up || (Input.GetAxisRaw ( "P2_Vertical" ) > 0);
		_right = (Input.GetAxisRaw ( "P2_Horizontal" ) > 0);
		_left = (Input.GetAxisRaw ( "P2_Horizontal" ) < 0);
		_playerChip = (Input.GetAxisRaw ("P2_Swing") > 0);
		
		// If player 2 throws the racket at a high angle, throw it high
		if (Input.GetAxisRaw ("P2_Throw_High") > 0) {
			Debug.Log("P2 THROW RACKET HIGH");
			racketBeingTossed = true;
			_animator.Play( Animator.StringToHash("RacketToss_High"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter.DecrementMennis_racketToss();
				Debug.Log("P2 RACKET THROW");
				racket.collider2D.enabled = true; // Enable the racket collider during toss
				StartCoroutine (ThrowRacket (false));
			}
			// If player 2 throws the racket at a straight angle, throw it straight
		}else if (Input.GetAxisRaw ("P2_Throw_Straight") > 0) {
			//Debug.Log("P2 THROW RACKET STRAIGHT");
			racketBeingTossed = true;
			_animator.Play( Animator.StringToHash("RacketToss_Straight"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter.DecrementMennis_racketToss();
				Debug.Log("P2 RACKET THROW");
				racket.collider2D.enabled = true; // Enable the racket collider during toss
				StartCoroutine (ThrowRacket (false));
			}
			// If player 2 throws the racket at a low angle, throw it low
		}else if (Input.GetAxisRaw ("P2_Throw_Low") > 0) {
			Debug.Log("P2 THROW RACKET LOW");
			racketBeingTossed = true;
			_animator.Play( Animator.StringToHash("RacketToss_Low"));
			// If the racket isn't already being thrown, kickoff the coroutine to throw & return it
			if (being_thrown == false){
				being_thrown = true;
				mennisMeter.DecrementMennis_racketToss();
				Debug.Log("P2 RACKET THROW");
				racket.collider2D.enabled = true; // Enable the racket collider during toss
				StartCoroutine (ThrowRacket (false));
			}
		}
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
	
	public override void Swing(){
		// If the swing input is hit by either player, and that player isn't already swinging:
		// -Set the swinging bool to true
		// -Enable the collider on the racket
		// -Trigger a coroutine which will eventually reset the swinging bool and collider
		if(_playerChip && playerIsSwinging == false) {
			Debug.Log("Player 2 racket swung");
			playerIsSwinging = true;
			racket.collider2D.enabled = true;
			_animator.Play( Animator.StringToHash( "RacketSwing_Forward" ) );
			asrc.PlayOneShot(grunt0, 1.0f);
			StartCoroutine(ToggleSwing());
		}
		
		if(being_thrown && playerIsSwinging == false){
			Debug.Log("Player 2 racket toss");
			playerIsSwinging = true;
			racket.collider2D.enabled = true;
			_animator.Play( Animator.StringToHash( "RacketToss_Straight" ) );
			asrc.PlayOneShot(grunt1, 1.0f);
			StartCoroutine(ToggleSwing());
		}
	}
	
	void FixedUpdate(){
		SetInput();
		Move ();
		Swing();
		
		// Update the path array
		path[0] = endMarker.transform.position;
		path[1] = originalRacketPosMarker.transform.position;
	}
}
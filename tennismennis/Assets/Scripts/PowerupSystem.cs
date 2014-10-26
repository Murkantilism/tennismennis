using UnityEngine;
using System.Collections;

public class PowerupSystem : MonoBehaviour {
	
	public GameObject prefab; //Assigned via the inspector
	
	public GameObject powerup;
	
	public bool spawned = false; // Has this power-up been spawned?
	
	public Transform powerup_transform;
	
	public float normalizedHorizontalSpeed = 0;
	
	public Rigidbody2D powerup_rigid2D;
	
	public Vector3 _velocity;
	
	public Vector3 powerup_speed = new Vector3(5, 0, 0);
	
	float powerup_spawnTimer_low = 5.0f;
	float powerup_spawnTimer_high = 15.0f;
	
	// The out-of-bounds values for this court
	public float xbound_pos = 16.0f;
	public float xbound_neg = -16.0f;
	public float ybound_pos = 10.0f;
	public float ybound_neg = -2.9f;
	
	private string powerup_type;
	
	GameObject player_go;

	// Use this for initialization
	IEnumerator Start () {
		transform.localScale = new Vector3 (2, 2, 1);
		normalizedHorizontalSpeed = 0;
		
		// Kick-off a continuous coroutine to spawn power-ups
		while(true){
			yield return StartCoroutine(Powerup());
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(spawned == true){
			MovePowerup();
		}
	}
	
	// Wait a semi-random amount of time, spawn the power-up
	IEnumerator Powerup(){
		if(spawned == false){
			//Debug.Log("Powerup coroutine");
			yield return new WaitForSeconds(Random.Range(powerup_spawnTimer_low, powerup_spawnTimer_high));
			SpawnPowerup();
		}
		//Debug.Log("Outside powerup coroutine");
	}
	
	// Spawn the power-up at max Y = 7.5, and random X between -17.5, 17.5
	// with a random type
	void SpawnPowerup(){
		Debug.Log("Powerup spawned");
		powerup = (GameObject)Instantiate(prefab, new Vector3(Random.Range(xbound_neg, xbound_pos), ybound_pos, 0.0f), Quaternion.identity);
		spawned = true;
		powerup_transform = powerup.transform;
		powerup_rigid2D = powerup.GetComponent<Rigidbody2D>();
		
		// Set the starting direction the power-up randomly (50/50)
		if(Random.value < 0.5f){
			normalizedHorizontalSpeed = -1;
		}else{
			normalizedHorizontalSpeed = 1;
		}
		powerup_type = "decoy";
		/*
		int randy = Random.Range(1, 5); // Do I make you randy, baby?
		if(randy == 1){
			powerup_type = "powerhitter";
		}else if(randy == 2){
			powerup_type = "hugeRacket";
		}else if(randy == 3){
			powerup_type = "spaceWalk";
		}else if(randy == 4){
			powerup_type = "decoy";
		}*/
	}
	
	// Move the power-up in a leaf-falling pattern
	void MovePowerup(){
		// Set the local scale
		powerup_transform.localScale = new Vector3( powerup_transform.localScale.x, powerup_transform.localScale.y, powerup_transform.localScale.z );
		
		// If it reaches either out-of-bounds X boundry, flip the direction
		if(powerup_transform.position.x > xbound_pos){
			normalizedHorizontalSpeed = -1;
		}else if(powerup_transform.position.x < xbound_neg){
			normalizedHorizontalSpeed = 1;
		}
		
		// Calculate the velocity (linear interpolation of the speed and direction)
		_velocity = new Vector3(Mathf.Lerp(powerup_speed.x, normalizedHorizontalSpeed, Time.fixedDeltaTime), powerup_speed.y, powerup_speed.z);
		
		// As long as the powerup hasn't reached the court floor, decrement the Y position at a constant rate
		if(powerup_transform.position.y > (ybound_neg - 0.1f)){
			powerup_transform.position = new Vector3(powerup_transform.position.x, powerup_transform.position.y - 0.01f, powerup_transform.position.z);
		}
		
		// Move the power up based on velocity and time
		powerup_rigid2D.MovePosition(powerup_transform.position +  (_velocity * normalizedHorizontalSpeed) * Time.fixedDeltaTime);
	}
	
	// If the ball collides with the power-up, destroy the power-up and invoke 
	// the class for the corresponding power-up to apply it to the player
	public void PowerupAcquired(bool player){ // true = player 1, false = player 2
		DestroyPowerup();
		
		if(player == true){
			player_go = GameObject.Find("Player1");
		}else{
			player_go = GameObject.Find("Player2");
		}
		

		// Invoke the power up class and pass the ball's last hit bool to it
		if(powerup_type == "powerhitter"){
			player_go.AddComponent<Powerup_powerhitter>();
			player_go.GetComponent<Powerup_powerhitter>().ActivatePowerup(player);
			StartCoroutine(WaitToDeactivate(player));
			
		}else if(powerup_type == "hugeRacket"){
			player_go.AddComponent<Powerup_hugeRacket>();
			player_go.GetComponent<Powerup_hugeRacket>().ActivatePowerup(player);
			StartCoroutine(WaitToDeactivate(player));
			
		}else if(powerup_type == "spaceWalk"){
			player_go.AddComponent<Powerup_spaceWalk>();
			player_go.GetComponent<Powerup_spaceWalk>().ActivatePowerup(player);
			StartCoroutine(WaitToDeactivate(player));
			
		}else if(powerup_type == "decoy"){
			player_go.AddComponent<Powerup_decoy>();
			player_go.GetComponent<Powerup_decoy>().ActivatePowerup(player);
			StartCoroutine(WaitToDeactivate(player));
		}
	}

	
	// Destroy the current power-up object, reset spawned bool
	public void DestroyPowerup(){
		Debug.Log("Powerup destroyed");
		spawned = false;
		Destroy(powerup);
	}
	
	IEnumerator WaitToDeactivate(bool player){
		yield return new WaitForSeconds(5.0f);
		
		if(powerup_type == "powerhitter"){
			player_go.GetComponent<Powerup_powerhitter>().DeactivatePowerup(player);
			Destroy(player_go.GetComponent("Powerup_powerhitter"));	
		}else if(powerup_type == "hugeRacket"){
			player_go.GetComponent<Powerup_hugeRacket>().DeactivatePowerup(player);
			Destroy(player_go.GetComponent("Powerup_hugeRacket"));
		}else if(powerup_type == "spaceWalk"){
			player_go.GetComponent<Powerup_spaceWalk>().DeactivatePowerup(player);
			Destroy(player_go.GetComponent("Powerup_spaceWalk"));
		}else if(powerup_type == "decoy"){
			player_go.GetComponent<Powerup_decoy>().DeactivatePowerup(player);
			Destroy(player_go.GetComponent("Powerup_decoy"));
		}
	}
}

using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
	bool player1Hit;
	bool player2Hit;
	float playerPower1;
	float playerPower2;
	GameObject player1;
	GameObject player2;
	GameObject racket1;
	public bool lastHit = true; // Who hit this ball last? true = player 1, false = player 2
	ScoreKeeping scoreKeeper;
	
	Vector2 idealVector;
	public Vector2 forceVector;
	
	public bool powerHitterEnabled_p1 = false;
	public bool powerHitterEnabled_p2 = false;

	public int numHits;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		racket1 = GameObject.Find ("racket_p1");
		transform.position = racket1.transform.position;
		scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeping>();
	}
	
	// Update is called once per frame
	void Update () {
		playerPower1 = player1.GetComponent<Player1> ().playerPower;
		playerPower2 = player2.GetComponent<Player2> ().playerPower;
		player1Hit = player1.GetComponent<Player1>().playerIsSwinging;
		player2Hit = player2.GetComponent<Player2>().playerIsSwinging;
	}
	
	public void TennisForce(Vector2 forceVector) {
		rigidbody2D.AddForce(forceVector, ForceMode2D.Impulse);
	}
	
	public void ServeShot(Vector2 forceVector) {
		rigidbody2D.AddForce (forceVector, ForceMode2D.Impulse);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		Vector2 collisionVector;
		if(col.gameObject.name == "Court"){
			Debug.Log("Hit the court!");
			// If the ball landed on the left side of the court, p2 scores
			if(transform.position.x < 0.05f){
				Debug.Log("Player 2 scores!");
				scoreKeeper.PointScored("Player2");
				// If the ball landed on the right side of the court, p1 scores	
			}else if(transform.position.x > 0.05f){
				Debug.Log("Player 1 scores!");
				scoreKeeper.PointScored("Player1");
			}
		}
		if(col.gameObject.name == "racket_p1" && player1Hit) {
			idealVector = new Vector2(5, 1);
			collisionVector = col.contacts[0].normal;
			collisionVector.x = Mathf.Abs (collisionVector.x * 10);
			collisionVector.y *= 5;
			if (collisionVector.y < 0) { collisionVector.y *= -1; }
			forceVector = playerPower1 * (idealVector + collisionVector);
			if(powerHitterEnabled_p1 == true){
				forceVector = forceVector * 1.5f;
			}
			TennisForce(forceVector);
			numHits += 1;
			lastHit = true;
			numHits += 1;
		}
		if (col.gameObject.name == "racket_p2" && player2Hit) {
			idealVector = new Vector2(-5, 1);
			collisionVector = col.contacts[0].normal;
			collisionVector.x = (collisionVector.x * 10) + idealVector.x;
			collisionVector.y *= 5 + idealVector.y;
			if (collisionVector.y < 0) { collisionVector.y *= -1; }
			forceVector = playerPower2 * collisionVector;
			if(powerHitterEnabled_p2 == true){
				forceVector = forceVector * 1.5f;
			}
			TennisForce(forceVector);
			numHits += 1;
			lastHit = false;
		}
	}
}

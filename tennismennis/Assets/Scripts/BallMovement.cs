using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
	bool player1Hit;
	bool player2Hit;
	GameObject player1;
	GameObject player2;
	GameObject racket1;
	bool serving;
	public bool lastHit = true; // Who hit this ball last? true = player 1, false = player 2
	ScoreKeeping scoreKeeper;
	
	// Use this for initialization
	void Start () {
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		racket1 = GameObject.Find ("racket_p1");
		transform.position = new Vector2 (racket1.transform.position.x, racket1.transform.position.y);
		TennisForce (new Vector2 (7, 7));
		scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeping>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (GameObject.Find ("racket_p2").collider2D.enabled);
		player1Hit = player1.GetComponent<Player1>().playerIsSwinging;
		player2Hit = player2.GetComponent<Player2>().playerIsSwinging;
	}
	
	void TennisForce(Vector2 forceVector) {
		rigidbody2D.AddForce(forceVector, ForceMode2D.Impulse);
	}

	public void ServeShot(Vector2 forceVector) {
		rigidbody2D.AddForce(forceVector, ForceMode2D.Impulse);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
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
			Vector2 idealVector = new Vector2(5, 2);
			Vector2 collisionVector = col.contacts[0].normal;
			collisionVector.x = Mathf.Abs(collisionVector.x * 5);
			collisionVector.y *= 6;
			Vector2 forceVector = idealVector + collisionVector;
			Debug.Log ("ANGLE EQUALS: " + Vector2.Angle(Vector2.right, forceVector.normalized));
			TennisForce(forceVector);
			lastHit = true;
		}
		if (col.gameObject.name == "racket_p2" && player2Hit) {
			Vector2 idealVector = new Vector2(-5, 2);
			Vector2 collisionVector = col.contacts[0].normal;
			collisionVector.x = -1* Mathf.Abs(collisionVector.x * 5);
			collisionVector.y *= 6;
			Vector2 forceVector = idealVector + collisionVector;
			TennisForce(forceVector);
			lastHit = false;
		}
	}
}
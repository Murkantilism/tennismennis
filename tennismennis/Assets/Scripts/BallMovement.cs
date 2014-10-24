using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
	bool serving = true;
	bool player1Hit;
	bool player2Hit;
	float player1Power;
	float player2Power;
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
<<<<<<< HEAD
		racket1 = GameObject.Find ("racket_p1");
		transform.position = new Vector2 (racket1.transform.position.x, racket1.transform.position.y);
		TennisForce (new Vector2 (7, 7));
=======
>>>>>>> df67f08eb0af04df8472e7eb840d0a535f1d29d0
		scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeping>();
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		Debug.Log (GameObject.Find ("racket_p2").collider2D.enabled);
		player1Hit = player1.GetComponent<Player1>().playerIsSwinging;
		player2Hit = player2.GetComponent<Player2>().playerIsSwinging;
=======
		player1Hit = player1.GetComponent<PlayerMovement>().playerIsSwinging;
		player2Hit = player2.GetComponent<PlayerMovement>().playerIsSwinging;
		player1Power = player1.GetComponent<PlayerMovement>()._playerPower;
		player2Power = player2.GetComponent<PlayerMovement>()._playerPower;
>>>>>>> df67f08eb0af04df8472e7eb840d0a535f1d29d0
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
<<<<<<< HEAD
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
=======
			TennisForce(new Vector2((500 * player1Power), 400));
			Debug.Log("HIT PLAYER 1");
		}
		if (col.gameObject.name == "racket_p2" && player2Hit) {
			TennisForce(new Vector2((-500 * player2Power), 400));
			TennisForce(new Vector2(400, 400));
			lastHit = true;
			Debug.Log("HIT PLAYER 1");
		}
		if (col.gameObject.name == "racket_p2" && player2Hit) {
			TennisForce(new Vector2(-400, 400));
			lastHit = false;
			Debug.Log("Hit Player 2");
>>>>>>> df67f08eb0af04df8472e7eb840d0a535f1d29d0
		}
	}
}

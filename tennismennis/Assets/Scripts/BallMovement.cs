using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
	bool player1Hit;
	bool player2Hit;
	GameObject player1;
	GameObject player2;
	public bool lastHit = true; // Who hit this ball last? true = player 1, false = player 2
	ScoreKeeping scoreKeeper;
	
	// Use this for initialization
	void Start () {
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeping>();
	}
	
	// Update is called once per frame
	void Update () {
		player1Hit = player1.GetComponent<Player1>().playerIsSwinging;
		player2Hit = player2.GetComponent<Player2>().playerIsSwinging;
	}
	
	void TennisForce(Vector2 forceVector) {
		rigidbody2D.AddForce(forceVector);
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
			TennisForce(new Vector2(400, 400));
			lastHit = true;
			Debug.Log("HIT PLAYER 1");
		}
		if (col.gameObject.name == "racket_p2" && player2Hit) {
			TennisForce(new Vector2(-400, 400));
			lastHit = false;
			Debug.Log("Hit Player 2");
		}
	}
}

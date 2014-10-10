using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
	bool player1Hit;
	bool player2Hit;
	GameObject player1;
	GameObject player2;
	public bool lastHit = true; // Who hit this ball last? true = player 1, false = player 2
	
	// Use this for initialization
	void Start () {
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
	}
	
	// Update is called once per frame
	void Update () {
		player1Hit = player1.GetComponent<PlayerMovement>().playerIsSwinging;
		player2Hit = player2.GetComponent<PlayerMovement>().playerIsSwinging;
	}
	
	void TennisForce(Vector2 forceVector) {
		rigidbody2D.AddForce(forceVector);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Court"){
			Debug.Log("GROUND");
		}
		if(col.gameObject.name == "racket_p1" && player1Hit) {
			TennisForce(new Vector2(400, 400));
			Debug.Log("HIT PLAYER 1");
		}
		if (col.gameObject.name == "racket_p2" && player2Hit) {
			TennisForce(new Vector2(-400, 400));
			Debug.Log("Hit Player 2");
		}
	}
}

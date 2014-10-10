using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
	bool player1Hit;
	bool player2Hit;
	float player1Power;
	float player2Power;
	GameObject player1;
	GameObject player2;
	
	// Use this for initialization
	void Start () {
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
	}
	
	// Update is called once per frame
	void Update () {
		player1Hit = player1.GetComponent<PlayerMovement>().player1IsSwinging;
		player2Hit = player2.GetComponent<PlayerMovement>().player2IsSwinging;
		player1Power = player1.GetComponent<PlayerMovement>()._player1Power;
		player2Power = player2.GetComponent<PlayerMovement>()._player2Power;
	}
	
	void TennisForce(Vector2 forceVector) {
		rigidbody2D.AddForce(forceVector);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Court"){
			Debug.Log("GROUND");
		}
		if(col.gameObject.name == "racket_p1" && player1Hit) {
			TennisForce(new Vector2((500 * player1Power), 400));
			Debug.Log("HIT PLAYER 1");
		}
		if (col.gameObject.name == "racket_p2" && player2Hit) {
			TennisForce(new Vector2((-500 * player2Power), 400));
			Debug.Log("Hit Player 2");
		}
	}
}

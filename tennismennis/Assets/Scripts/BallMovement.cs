using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

	GameObject player1;
	bool hit;
	
	// Use this for initialization
	void Start () {
		player1 = GameObject.Find("Player1");
	}
	
	// Update is called once per frame
	void Update () {
		hit = GameObject.Find ("Player1").GetComponent<PlayerMovement>().isSwinging;
		Debug.Log(hit);
	}

	void TennisForce(Vector2 forceVector) {
		for(double i = 0; i < 5; i++)
		{
			rigidbody2D.AddForce(forceVector);
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Court"){
			Debug.Log("GROUND");
		}
		if(col.gameObject.name == "Player1" && hit) {
			TennisForce(new Vector2(50, 75));
			Debug.Log ("racket1");
		}
		if (col.gameObject.name == "racket2") {
			TennisForce(new Vector2(-100, 150));
			Debug.Log("Player2");
		}
	}
}

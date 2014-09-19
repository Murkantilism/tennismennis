using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void TennisForce(Vector2 forceVector) {
		for(int i = 3; i > 0; i--)
		{
			rigidbody2D.AddForce(forceVector);
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Court"){
			Debug.Log("GROUND");
		}
		if(col.gameObject.name == "Player1") {
			TennisForce(new Vector2(100, 100));
			Debug.Log ("PLAYER1");
		}
		if (col.gameObject.name == "Player2") {
			TennisForce(new Vector2(-100, 150));
			Debug.Log("Player2");
		}
	}
}

using UnityEngine;
using System.Collections;

public class Powerup_hugeRacket : MonoBehaviour {

	GameObject racket_p1;
	GameObject racket_p2;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Actives the power up on the player given (true = player 1, false = player 2)
	public void ActivatePowerup(bool player){
		Debug.Log ("HUGE RACKET ACTIVATED");
		
		racket_p1 = GameObject.Find("racket_p1");
		racket_p2 = GameObject.Find("racket_p2");
		
		if(player == true){
			racket_p1.transform.localScale = new Vector3(2, 2, 1);
		}else if(player == false){
			racket_p2.transform.localScale = new Vector3(2, 2, 1);
		}
	}
	
	public void DeactivatePowerup(bool player){
		if(player == true){
			racket_p1.transform.localScale = new Vector3(1, 1, 1);
		}else if(player == false){
			racket_p2.transform.localScale = new Vector3(1, 1, 1);
		}
	}
}

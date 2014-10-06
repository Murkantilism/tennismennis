using UnityEngine;
using System.Collections;

public class Powerup_powerhitter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Actives the power up on the player given (true = player 1, false = player 2)
	public void ActivatePowerup(bool player){
		Debug.Log("POWERHITTER ACTIVATED");
	}
}

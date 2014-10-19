using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool player; // True = player 1, false = player 2, set via inspector
	
	Player1 player_1;
	Player2 player_2;

	// Use this for initialization
	void Start () {
		if(player == true){
			gameObject.AddComponent<Player1>();
			player_1 = gameObject.GetComponent<Player1>();
		}else if(player == false){
			gameObject.AddComponent<Player2>();
			player_2 = gameObject.GetComponent<Player2>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

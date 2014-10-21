using UnityEngine;
using System.Collections;

public class ScoreKeeping : MonoBehaviour {
	public GUISkin guiSkin;

	public Texture2D pointLabel;
	int player1_score = 0;
	int player2_score = 0;

	float p1Point;
	float p2Point;

	// Use this for initialization
	void Start () {
		p1Point = 0.0f;
		p2Point = 0.0f;
	}

	// Increment player scores based on who scored!
	// Recieves calls from OutOfBoundsDetection.cs && Ball.cs
	void PointScored(string whoScored){
		if (whoScored == "Player1") {
			player1_score += 1;
			p1Point = 1;
		}else if(whoScored == "Player2"){
			player2_score += 1;
			p2Point = 1;
		}
	}

	void OnGUI(){
		// Assign this GUI's skin to the skin assigned via inspector
		GUI.skin = guiSkin;
		// Create a GUI style for the score based on the skin's label
		GUIStyle scoreStyle = GUI.skin.label;

		GUI.Label(new Rect (Screen.width * 12/32, Screen.height*7/64, 100, Screen.height*3/32), player1_score.ToString("D2"), scoreStyle);

		GUI.Label(new Rect (Screen.width * 30/32, Screen.height*7/64, 100, Screen.height*3/32), player2_score.ToString("D2"), scoreStyle);

		if (p1Point > 0){
			GUI.Label (new Rect(Screen.width*4/32, Screen.height*10/32,Screen.width*8/32, Screen.height*6/32), pointLabel);
		}
		if (p2Point > 0){
			GUI.Label (new Rect(Screen.width*20/32, Screen.height*10/32,Screen.width*8/32, Screen.height*6/32), pointLabel);
		}
	}
	void FixedUpdate(){
		if (p1Point > 0) {p1Point -= Time.deltaTime;}
		if (p2Point > 0) {p2Point -= Time.deltaTime;}

		// cheat buttons, auto-score points for UI testing
		if (Input.GetKeyDown("6")) {this.PointScored("Player1");}
		if (Input.GetKeyDown("7")) {this.PointScored("Player2");}
	}
}

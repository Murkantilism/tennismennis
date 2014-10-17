using UnityEngine;
using System.Collections;

public class ScoreKeeping : MonoBehaviour {
	public GUISkin guiSkin;

	public Texture2D pointLabel;
	public int player1_score = 0;
	public int player2_score = 0;

	float point;
	
	EndOfRound endOfRound;

	// Use this for initialization
	void Start () {
		endOfRound = GameObject.Find ("EndOfRound").GetComponent<EndOfRound> ();
		point = 0.0f;
	}

	// Increment player scores based on who scored!
	// Recieves calls from OutOfBoundsDetection.cs && Ball.cs
	public void PointScored(string whoScored){
		if (whoScored == "Player1") {
			player1_score += 1;
		}else if(whoScored == "Player2"){
			player2_score += 1;
		}
		point = 1;
		endOfRound.SendMessage ("ResetRound");
	}

	void OnGUI(){
		// Assign this GUI's skin to the skin assigned via inspector
		GUI.skin = guiSkin;
		// Create a GUI style for the score based on the skin's label
		GUIStyle scoreStyle = GUI.skin.label;

		GUI.Label(new Rect (Screen.width * 12/32, Screen.height*7/64, 100, Screen.height*3/32), player1_score.ToString("D2"), scoreStyle);

		GUI.Label(new Rect (Screen.width * 30/32, Screen.height*7/64, 100, Screen.height*3/32), player2_score.ToString("D2"), scoreStyle);

		if (point > 0){
			GUI.Label (new Rect(Screen.width*12/32, Screen.height*10/32,Screen.width*8/32, Screen.height*6/32), pointLabel);
		}
	}
	void FixedUpdate(){
		if (point > 0) {point -= Time.deltaTime;}
		// cheat button, auto-scores a point for P1 for UI testing
		if (Input.GetKeyDown("6")) {this.PointScored("Player1");}
	}
}

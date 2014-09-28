using UnityEngine;
using System.Collections;

public class ScoreKeeping : MonoBehaviour {
	public GUISkin guiSkin;

	int player1_score = 0;
	int player2_score = 0;

	// Use this for initialization
	void Start () {
	
	}

	// Increment player scores based on who scored!
	// Recieves calls from OutOfBoundsDetection.cs && Ball.cs
	void PointScored(string whoScored){
		if (whoScored == "Player1") {
			player1_score += 1;
		}else if(whoScored == "Player2"){
			player2_score += 1;
		}
	}

	void OnGUI(){
		// Assign this GUI's skin to the skin assigned via inspector
		GUI.skin = guiSkin;
		// Create a GUI style for the score based on the skin's label
		GUIStyle scoreStyle = GUI.skin.label;

		GUI.Label(new Rect (Screen.width / 2 - Screen.width/4, Screen.height - (Screen.height/4 * 3.5f), 100, 40), player1_score.ToString(), scoreStyle);

		GUI.Label(new Rect (Screen.width / 2 + Screen.width/4, Screen.height - (Screen.height/4 * 3.5f), 100, 40), player2_score.ToString(), scoreStyle);
	}
}

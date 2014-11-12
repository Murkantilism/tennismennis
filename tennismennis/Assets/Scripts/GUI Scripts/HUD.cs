
using UnityEngine;
using System.Collections;
using System;

public class HUD : MonoBehaviour {
	
	public GUISkin guiSkin;
	
	string p1Character;
	string p2Character;
	
	Vector2 p1Pos = new Vector2(Screen.width/32,Screen.height*2/32);
	Vector2 p2Pos = new Vector2(Screen.width*21/32, Screen.height*2/32);
	Vector2 size = new Vector2(Screen.width*10/32,Screen.height*8/32);
	
	float p1Mennis = 0;
	float p2Mennis = 0;
	float timeToStart = 4.0f;
	
	bool paused = false;
	
	public Texture2D threeLabel;
	public Texture2D twoLabel;
	public Texture2D oneLabel;
	public Texture2D startLabel;

	public Texture2D progressBarFull;
	public Texture2D swolesaurusTitle;
	public Texture2D fishTitle;
	public Texture2D shivaTitle;
	public Texture2D dennisTitle;
	
	public Texture2D swolePic;
	public Texture2D fishPic;
	public Texture2D shivaPic;
	public Texture2D dennisPic;

	public Texture2D ringOfPower;
	public Texture2D powerhitter;
	public Texture2D spaceWalk;
	public Texture2D hugeRacket;
	public Texture2D decoy;
	Texture2D p1Powerup;
	Texture2D p2Powerup;

	
	void Start()
	{
		p1Mennis = (float)(size.x * .4);
		p2Mennis = (float)(size.x * .4);
		PauseGame();

		p1Powerup = ringOfPower;
		p2Powerup = ringOfPower;
	}
	
	void OnGUI()
	{
		// Assign this GUI's skin to the skin assigned via inspector
		GUI.skin = guiSkin;
		// Create a GUI style for the score based on the skin's label
		GUIStyle scoreStyle = GUI.skin.label;
		
		//// Game Messages
		if (timeToStart == 3) {
			GUI.Label (new Rect(Screen.width*15/32, Screen.height*10/32,Screen.width*2/32, Screen.height*6/32), threeLabel);
		}else if (timeToStart == 2) {
			GUI.Label (new Rect(Screen.width*15/32, Screen.height*10/32,Screen.width*2/32, Screen.height*6/32), twoLabel);
		}else if (timeToStart == 1) {
			GUI.Label (new Rect(Screen.width*15/32, Screen.height*10/32,Screen.width*2/32, Screen.height*6/32), oneLabel);
		}else if (timeToStart == 0) {
			GUI.Label (new Rect(Screen.width*12/32, Screen.height*10/32,Screen.width*8/32, Screen.height*6/32), startLabel);
			paused = false;
			Time.timeScale = 1f;
		}
		
		//// Player 1 HUD
		GUI.BeginGroup (new Rect (p1Pos.x, p1Pos.y, size.x, size.y));
		GUI.DrawTexture(new Rect(0,0,Screen.width*3/32,Screen.height*5/32), swolePic);
		GUI.DrawTexture(new Rect(Screen.width*3/32,Screen.width*3/64,Screen.width*1/32,Screen.width*1/32), p1Powerup);
		GUI.DrawTexture(new Rect(Screen.width*3/32,Screen.width*3/64,Screen.width*1/32,Screen.width*1/32), ringOfPower);
		GUI.Label (new Rect(Screen.width*3/32,Screen.height*3/64,Screen.width*7/32,Screen.height*2/32), swolesaurusTitle);
		GUI.EndGroup ();
		
		//// Player 2 HUD
		GUI.BeginGroup (new Rect (p2Pos.x, p2Pos.y, size.x, size.y));
		GUI.DrawTexture(new Rect(Screen.width*7/32,0,Screen.width*3/32,Screen.height*5/32), fishPic);
		GUI.DrawTexture(new Rect(Screen.width*6/32,Screen.width*3/64,Screen.width*1/32,Screen.width*1/32), p2Powerup);
		GUI.DrawTexture(new Rect(Screen.width*6/32,Screen.width*3/64,Screen.width*1/32,Screen.width*1/32), ringOfPower);
		GUI.Label (new Rect(0,Screen.height*3/64,Screen.width*7/32,Screen.height*2/32), swolesaurusTitle);
		GUI.EndGroup ();
	} 
	
	void FixedUpdate()
	{
		p1Mennis += (float)0.35;
		p2Mennis += (float)0.35;
	}
	
	// Pause the game while we reset for the next round
	void PauseGame(){
		Time.timeScale = 0.0f;
		paused = true;
		StartCoroutine ("DelayedGameStart");
	}
	
	IEnumerator DelayedGameStart(){
		paused = true;
		
		// This is a workaround so we don't depend on WaitForSeconds while paused
		while(paused == true){
			float pauseEndTime = Time.realtimeSinceStartup + 1f;
			while (Time.realtimeSinceStartup < pauseEndTime){
				yield return 0;
			}
			Debug.Log(timeToStart);
			
			// Decrement the timer until it is zero, then unpause
			if (timeToStart > -2){ // Stop decrementing at -2
				timeToStart -= 1; 
			}else{
				// Stop the coroutine
				StopCoroutine ("DelayedGameStart");
			}
		}
	}

	public void activePowerup(string type, bool player) {
		if (type == "powerhitter"){
			if (player) {p1Powerup = powerhitter;}
			else{p2Powerup = powerhitter;}
		}else if (type == "spaceWalk"){
			if (player) {p1Powerup = spaceWalk;}
			else{p2Powerup = spaceWalk;}
		}else if (type == "hugeRacket"){
			if (player) {p1Powerup = hugeRacket;}
			else{p2Powerup = hugeRacket;}
		}else if (type == "decoy"){
			if (player) {p1Powerup = decoy;}
			else{p2Powerup = decoy;}
		}

	}

}
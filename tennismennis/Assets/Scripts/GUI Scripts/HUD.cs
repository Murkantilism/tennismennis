
using UnityEngine;
using System.Collections;
using System;

public class HUD : MonoBehaviour {
	
	public GUISkin guiSkin;

	SaveSelections savedSelections;
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

	Texture2D p1Title;
	Texture2D p2Title;
	Texture2D p1Pic;
	Texture2D p2Pic;
	Texture2D p1Powerup;
	Texture2D p2Powerup;


	void Start()
	{
		p1Mennis = (float)(size.x * .4);
		p2Mennis = (float)(size.x * .4);
		PauseGame();

		p1Powerup = ringOfPower;
		p2Powerup = ringOfPower;

		savedSelections = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();
		assignCharacters ();
	}
	
	void OnGUI()
	{
		// Assign this GUI's skin to the skin assigned via inspector
		GUI.skin = guiSkin;

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
		GUI.DrawTexture(new Rect(0,0,Screen.width*3/32,Screen.height*5/32), p1Pic);
		GUI.DrawTexture(new Rect(Screen.width*3/32,Screen.width*3/64,Screen.width*1/32,Screen.width*1/32), p1Powerup);
		GUI.DrawTexture(new Rect(Screen.width*3/32,Screen.width*3/64,Screen.width*1/32,Screen.width*1/32), ringOfPower);
		GUI.Label (new Rect(Screen.width*3/32,Screen.height*1/32,Screen.width*7/32,Screen.height*2/32), p1Title);
		GUI.EndGroup ();
		
		//// Player 2 HUD
		GUI.BeginGroup (new Rect (p2Pos.x, p2Pos.y, size.x, size.y));
		GUI.DrawTexture(new Rect(Screen.width*7/32,0,Screen.width*3/32,Screen.height*5/32), p2Pic);
		GUI.DrawTexture(new Rect(Screen.width*6/32,Screen.width*3/64,Screen.width*1/32,Screen.width*1/32), p2Powerup);
		GUI.DrawTexture(new Rect(Screen.width*6/32,Screen.width*3/64,Screen.width*1/32,Screen.width*1/32), ringOfPower);
		GUI.Label (new Rect(0,Screen.height*1/32,Screen.width*7/32,Screen.height*2/32), p2Title);
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

	void assignCharacters(){
		if(savedSelections.selected_p1 == "S. Racks"){
			p1Pic = swolePic;
			p1Title = swolesaurusTitle;
		}else if(savedSelections.selected_p1 == "SH1-V4"){
			p1Pic = shivaPic;
			p1Title = shivaTitle;
		}else if(savedSelections.selected_p1 == "Colonel Topspin"){
			p1Pic = fishPic;
			p1Title = fishTitle;
		}else{
			p1Pic = dennisPic;
			p1Title = dennisTitle;
		}

		if(savedSelections.selected_p2 == "S. Racks"){
			p2Pic = swolePic;
			p2Title = swolesaurusTitle;
		}else if(savedSelections.selected_p2 == "SH1-V4"){
			p2Pic = shivaPic;
			p2Title = shivaTitle;
		}else if(savedSelections.selected_p2 == "Colonel Topspin"){
			p2Pic = fishPic;
			p2Title = fishTitle;
		}else{
			p2Pic = dennisPic;
			p2Title = dennisTitle;
		}
	}
}
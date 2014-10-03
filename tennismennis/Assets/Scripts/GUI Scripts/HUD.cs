using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public GUISkin customSkin;

	string p1Character;
	string p2Character;

	Vector2 p1Pos = new Vector2(Screen.width/16,Screen.height/16);
	Vector2 p2Pos = new Vector2(Screen.width*11/16, Screen.height/16);
	Vector2 size = new Vector2(Screen.width*1/4,Screen.height*9/64);

//	float fullMennis = size.x;
	float p1Mennis = 0;
	float p2Mennis = 0;

	Texture2D progressBarEmpty;
	Texture2D progressBarFull;

	public Texture2D swolesaurusTitle;
	public Texture2D fishTitle;
	public Texture2D shivaTitle;
	public Texture2D dennisTitle;

	
	void OnGUI()
	{
	//	p1Mennis = (float)(.5*fullMennis);
		//// P1 and P2 Mennis Meters
		// draw the background:
		GUI.BeginGroup (new Rect (p1Pos.x, p1Pos.y, size.x, size.y));
		GUI.Label (new Rect(0,0,Screen.width/4,Screen.height/16), swolesaurusTitle);
		GUI.Box (new Rect (0,Screen.height/16, size.x, size.y/2),progressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * p1Mennis, size.y));
		GUI.Box (new Rect (0,Screen.height/16, size.x, size.y/2),progressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();

		// draw the background:
		GUI.BeginGroup (new Rect (p2Pos.x, p2Pos.y, size.x, size.y));
		GUI.Label (new Rect(0,0,Screen.width/4,Screen.height/16), shivaTitle);
		GUI.Box (new Rect (0,Screen.height/16, size.x, size.y/2),progressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * p2Mennis, size.y));
		GUI.Box (new Rect (0,Screen.height/16, size.x, size.y/2),progressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();



	} 
	
	void FixedUpdate()
	{
		// for this example, the bar display is linked to the current time,
		// however you would set this value based on your desired display
		// eg, the loading progress, the player's health, or whatever.
		p1Mennis = (float)(Time.time * 0.05);
		p2Mennis = (float)(Time.time * 0.05);
	}
}

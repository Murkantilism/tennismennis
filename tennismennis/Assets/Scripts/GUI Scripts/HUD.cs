using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public GUISkin guiSkin;

	string p1Character;
	string p2Character;

	Vector2 p1Pos = new Vector2(Screen.width/16,Screen.height/16);
	Vector2 p2Pos = new Vector2(Screen.width*17/32, Screen.height/16);
	Vector2 size = new Vector2(Screen.width*13/32,Screen.height*3/16);

	float p1Mennis = 0;
	float p2Mennis = 0;

	Texture2D progressBarEmpty;
	Texture2D progressBarFull;

	public Texture2D swolesaurusTitle;
	public Texture2D fishTitle;
	public Texture2D shivaTitle;
	public Texture2D dennisTitle;

	void Start()
	{
		p1Mennis = (float)(size.x * .4);
		p2Mennis = (float)(size.x * .4);
	}

	void OnGUI()
	{
		// Assign this GUI's skin to the skin assigned via inspector
		GUI.skin = guiSkin;
		// Create a GUI style for the score based on the skin's label
		GUIStyle scoreStyle = GUI.skin.label;

		//// Player 1 HUD
		GUI.BeginGroup (new Rect (p1Pos.x, p1Pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0,Screen.width*3/32,Screen.height*3/16), "Portrait");
		GUI.Label (new Rect(Screen.width*3/32,Screen.height*1/32,Screen.width/4,Screen.height/16), swolesaurusTitle);
		GUI.Box(new Rect(Screen.width*21/64,0,Screen.width*1/16,Screen.height*3/16), "100", scoreStyle);
	
		// mennis meter:
		GUI.Box (new Rect (Screen.width*3/32,Screen.height*3/32, Screen.width*7/32, size.y/3),progressBarEmpty);
		GUI.BeginGroup (new Rect (0, 0, p1Mennis, size.y));
		GUI.Box (new Rect (Screen.width*3/32,Screen.height*3/32, Screen.width*7/32, size.y/3),progressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();

		//// Player 2 HUD
		GUI.BeginGroup (new Rect (p2Pos.x, p2Pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0,Screen.width*3/32,Screen.height*3/16), "Portrait");
		GUI.Label (new Rect(Screen.width*3/32,Screen.height*1/32,Screen.width/4,Screen.height/16), fishTitle);
		GUI.Box(new Rect(Screen.width*21/64,0,Screen.width*1/16,Screen.height*3/16), "100", scoreStyle);
		
		// mennis meter:
		GUI.Box (new Rect (Screen.width*3/32,Screen.height*3/32, Screen.width*7/32, size.y/3),progressBarEmpty);
		GUI.BeginGroup (new Rect (0, 0, p2Mennis, size.y));
		GUI.Box (new Rect (Screen.width*3/32,Screen.height*3/32, Screen.width*7/32, size.y/3),progressBarFull);
		GUI.EndGroup ();

		GUI.EndGroup ();
	} 
	
	void FixedUpdate()
	{
		p1Mennis += (float)0.35;
		p2Mennis += (float)0.35;
	}
}

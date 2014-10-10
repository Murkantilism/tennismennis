﻿using UnityEngine;
using System.Collections;
using System;

public class HUD : MonoBehaviour {
	
	public GUISkin guiSkin;

	string p1Character;
	string p2Character;

	Vector2 p1Pos = new Vector2(Screen.width/32,Screen.height*2/32);
	Vector2 p2Pos = new Vector2(Screen.width*19/32, Screen.height*2/32);
	Vector2 size = new Vector2(Screen.width*10/32,Screen.height*5/32);

	float p1Mennis = 0;
	float p2Mennis = 0;

	public Texture2D progressBarFull;

	public Texture2D swolesaurusTitle;
	public Texture2D fishTitle;
	public Texture2D shivaTitle;
	public Texture2D dennisTitle;

	public Texture2D swolesaurusPic;

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
		GUI.DrawTexture(new Rect(0,0,Screen.width*3/32,Screen.height*5/32), swolesaurusPic);
		GUI.Label (new Rect(Screen.width*3/32,Screen.height*3/64,Screen.width*7/32,Screen.height*2/32), swolesaurusTitle);
	
		// mennis meter:
		GUI.BeginGroup (new Rect (0,0,p1Mennis, size.y));
		GUI.DrawTexture (new Rect (Screen.width*3/32,Screen.height*5/64, Screen.width*7/32, Screen.height*1/32),progressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();

		//// Player 2 HUD
		GUI.BeginGroup (new Rect (p2Pos.x, p2Pos.y, size.x, size.y));
		GUI.DrawTexture(new Rect(0,0,Screen.width*3/32,Screen.height*5/32), swolesaurusPic);
		GUI.Label (new Rect(Screen.width*3/32,Screen.height*3/64,Screen.width*7/32,Screen.height*2/32), swolesaurusTitle);

		// mennis meter:
		GUI.BeginGroup (new Rect (0,0,p2Mennis, size.y));
		GUI.DrawTexture (new Rect (Screen.width*3/32,Screen.height*5/64, Screen.width*7/32, Screen.height*1/32),progressBarFull);
		GUI.EndGroup ();

		GUI.EndGroup ();
	} 
	
	void FixedUpdate()
	{
		p1Mennis += (float)0.35;
		p2Mennis += (float)0.35;
	}
}
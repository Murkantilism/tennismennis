using UnityEngine;
using System.Collections;

public class CharacterSelectOverlay_P2 : MonoBehaviour {
	
	public GUISkin customSkin;
	public Texture2D DennisLabel;
	public Texture2D SwoleLabel;
	public Texture2D ShivaLabel;
	public Texture2D FishLabel;

	public Texture2D DennisIdle;
	public Texture2D SwoleIdle;
	public Texture2D ShivaIdle;
	public Texture2D FishIdle;

	public Texture2D DennisAlt;
	public Texture2D SwoleAlt;
	public Texture2D ShivaAlt;
	public Texture2D FishAlt;

	Vector2 buttonSize = new Vector2(Screen.width*8/32,Screen.height*17/64);
	Vector2 nameSize =   new Vector2(Screen.width*6/32,Screen.height*13/64);

	bool selectAlt = false;
	bool left = true;
	bool top = true;
	Texture2D selectedLabel;
	Texture2D idle;

	string tagline;
	string height;
	string weight;
	string stat;
	string bio;
	
	SaveSelections saveSelection;

	AudioSource asrc;
	public AudioClip intro;
	
	void Awake(){
		asrc = gameObject.GetComponent<AudioSource>();
	}
	
	void Start() {
		asrc.PlayOneShot(intro, 1.0f);
	}

	void FixedUpdate () {
		if (Input.GetKeyDown("d") || Input.GetKeyDown("a")) {left = !left;}
		if (Input.GetKeyDown("w") || Input.GetKeyDown("s")) {top = !top;}

		if (Input.GetKeyDown("left shift")) {selectAlt = !selectAlt;}
		if (Input.GetKeyDown("backspace")) { Application.LoadLevel("CharacterSelect"); }
	}

	void OnGUI () {
		GUI.skin = customSkin;
		GUIStyle p2Style = GUI.skin.GetStyle("P2Select");
		GUIStyle p2Background = GUI.skin.GetStyle("P2Background");
		GUIStyle bioStyle = GUI.skin.GetStyle("BioLabel");
		
		saveSelection = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();
		
		// Main box
		GUI.BeginGroup (new Rect (Screen.width*4/32, Screen.height*6/32, Screen.width/2, Screen.height*20/32));
		GUI.Box (new Rect(0,0, Screen.width/2, Screen.height*3/32), "", p2Background);
		GUI.Label (new Rect(Screen.width*1/32,Screen.height*1/64,Screen.width*14/32,Screen.height*4/32), "P2 Character");
		
		// Character Select buttons
		GUI.SetNextControlName("DennisButton");
		if(GUI.Button(new Rect(0,Screen.height*3/32,buttonSize.x, buttonSize.y), "", p2Style)) {
			characterButton("Dennis");
		}
		GUI.Label(new Rect(Screen.width*1/32,Screen.height*6/32,nameSize.x, nameSize.y), DennisLabel);

		GUI.SetNextControlName("SwoleButton");
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*3/32,buttonSize.x, buttonSize.y), "", p2Style)) {
			characterButton("S. Racks");
		}
		GUI.Label(new Rect(Screen.width*9/32,Screen.height*6/32,nameSize.x, nameSize.y), SwoleLabel);

		GUI.SetNextControlName("ShivaButton");
		if(GUI.Button(new Rect(0,Screen.height*23/64,buttonSize.x, buttonSize.y), "", p2Style)) {
			characterButton("SH1-V4");
		}
		GUI.Label(new Rect(Screen.width*1/32,Screen.height*29/64,nameSize.x, nameSize.y), ShivaLabel);

		GUI.SetNextControlName("FishButton");
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*23/64,buttonSize.x, buttonSize.y), "", p2Style)) {
			characterButton("Colonel Topspin");
		}
		GUI.Label(new Rect(Screen.width*9/32,Screen.height*29/64,nameSize.x, nameSize.y), FishLabel);

		GUI.EndGroup();

		// Character preview window
		assignSelection();
		GUI.BeginGroup (new Rect (Screen.width*21/32, Screen.height*3/32, Screen.width*10/32, Screen.height*26/32));
		GUI.Box(new Rect(0,0,Screen.width*10/32,Screen.height*26/32), "", p2Background);
		
		GUI.Label(new Rect(Screen.width *2/32, Screen.height*1/32, Screen.width*6/32, Screen.height*2/32), selectedLabel);
		GUI.Label(new Rect(Screen.width *3/32, Screen.height*3/32, Screen.width*4/32, Screen.height*7/32), idle);
		
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*10/32, Screen.width*8/32, Screen.height*2/32), "\"" + tagline + "\"", bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*13/32, Screen.width*8/32, Screen.height*1/32), "<color=#4798D5>Height:   </color>" + height, bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*14/32, Screen.width*8/32, Screen.height*1/32), "<color=#4798D5>Weight:   </color>" + weight, bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*15/32, Screen.width*8/32, Screen.height*1/32), stat, bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*17/32, Screen.width*8/32, Screen.height*7/32), "<color=#4798D5>The Story So Far:   </color>" + bio, bioStyle);
		
		GUI.EndGroup();
	}

	void characterButton(string name) {
		if(selectAlt) { saveSelection.WriteCharacterSelection_p2(name + " Alt"); }
		else { saveSelection.WriteCharacterSelection_p2(name); }
		DontDestroyOnLoad(saveSelection.gameObject);
		Application.LoadLevel("StageSelect");
	}

	void assignSelection() {
		if (top && left) {
//			GUI.FocusControl("DennisButton");
			if (selectAlt) { idle = DennisAlt; } 
			else { idle = DennisIdle; }
			
			selectedLabel = DennisLabel;
			tagline = "Just a regular guy, I guess";
			height = "5ft 10in";
			weight = "178lb";
			stat = "<color=#4798D5>Vision:   </color>20/20";
			bio = "Dennis is a regular guy. So regular, in fact, that it's almost obscene. He lives in a normal " +
				"house with his normal family and works a normal job. He plays tennis and enjoys vanilla ice cream.";
		}
		else if (top && !left) {
//			GUI.FocusControl("SwoleButton");
			if (selectAlt) { idle = SwoleAlt; } 
			else { idle = SwoleIdle; }
			
			selectedLabel = SwoleLabel;
			tagline = "Toughest dude this side of the Jurassic period.";
			height = "6ft 9in";
			weight = "387lb";
			stat = "<color=#4798D5>Max Bench:   </color>375lb";
			bio = "He's mean. He's green. He's over 200 million years old. And he has never missed leg day. Not even once.";
		}
		else if (!top && left) {
//			GUI.FocusControl("ShivaButton");
			if (selectAlt) { idle = ShivaAlt; } 
			else { idle = ShivaIdle; }
			
			selectedLabel = ShivaLabel;
			tagline = "The many-armed god of Tennis Destruction";
			height = "6ft 2in";
			weight = "210lb";
			stat = "<color=#4798D5>Number of Arms:   </color>4.00";
			bio = "Just exactly how and when 5H1-V4 came to be remains unknown. But we do know this: She has four arms. And " +
				"each one has the strength of 100 arms. That's like, 12 billion arms.";
		}
		else if (!top && !left) {
//			GUI.FocusControl("FishButton");
			if (selectAlt) { idle = FishAlt; } 
			else { idle = FishIdle; }
			
			selectedLabel = FishLabel;
			tagline = "He's a goldfish in tank. Not that kind of tank.";
			height = "2.7in";
			weight = ".07lb";
			stat = "<color=#4798D5>Barrel Diameter:   </color>120mm";
			bio = "Growing up, Colonel Topspin wasn't like all the other goldfish. He was born with a rare genetic defect" +
				" on his Y chromosome that confined him to live in a T36 tank for the rest of his life. This made it clear to" +
					"him from an early age that he was destined for tennis glory.";
		}
	}
}
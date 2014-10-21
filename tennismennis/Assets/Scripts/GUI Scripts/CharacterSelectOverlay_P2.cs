using UnityEngine;
using System.Collections;

public class CharacterSelectOverlay_P2 : MonoBehaviour {
	
	public GUISkin customSkin;
	public Texture2D DennisLabel;
	public Texture2D SwoleLabel;
	public Texture2D ShivaLabel;
	public Texture2D FishLabel;

	public Texture2D SwoleIdle;
	
	bool left = true;
	bool top = true;
	Texture2D selectedLabel;
	string tagline;
	string height;
	string weight;
	string stat;
	string bio;
	
	SaveSelections saveSelection;

	void FixedUpdate () {
		if (Input.GetKeyDown("d") || Input.GetKeyDown("a")) {left = !left;}
		if (Input.GetKeyDown("w") || Input.GetKeyDown("s")) {top = !top;}
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
		if(GUI.Button(new Rect(0,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), "", p2Style)) {
			saveSelection.WriteCharacterSelection_p2("Dennis");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("StageSelect");
		}
		GUI.Label(new Rect(Screen.width*1/32,Screen.height*6/32,Screen.width*6/32,Screen.height*13/64), DennisLabel);

		GUI.SetNextControlName("SwoleButton");
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), "", p2Style)) {
			saveSelection.WriteCharacterSelection_p2("S. Racks");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("StageSelect");
		}
		GUI.Label(new Rect(Screen.width*9/32,Screen.height*6/32,Screen.width*6/32,Screen.height*13/64), SwoleLabel);

		GUI.SetNextControlName("ShivaButton");
		if(GUI.Button(new Rect(0,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), "", p2Style)) {
			saveSelection.WriteCharacterSelection_p2("SH1-V4");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("StageSelect");
		}
		GUI.Label(new Rect(Screen.width*1/32,Screen.height*29/64,Screen.width*6/32,Screen.height*13/64), ShivaLabel);

		GUI.SetNextControlName("FishButton");
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), "", p2Style)) {
			saveSelection.WriteCharacterSelection_p2("Colonel Topspin");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("StageSelect");
		}
		GUI.Label(new Rect(Screen.width*9/32,Screen.height*29/64,Screen.width*6/32,Screen.height*13/64), FishLabel);

		GUI.EndGroup();

		// Character preview window
		assignSelection();
		GUI.BeginGroup (new Rect (Screen.width*21/32, Screen.height*3/32, Screen.width*10/32, Screen.height*26/32));
		GUI.Box(new Rect(0,0,Screen.width*10/32,Screen.height*26/32), "", p2Background);
		
		GUI.Label(new Rect(Screen.width *2/32, Screen.height*1/32, Screen.width*6/32, Screen.height*2/32), selectedLabel);
		GUI.Label(new Rect(Screen.width *3/32, Screen.height*3/32, Screen.width*4/32, Screen.height*7/32), SwoleIdle);
		
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*10/32, Screen.width*8/32, Screen.height*2/32), "\"" + tagline + "\"", bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*13/32, Screen.width*8/32, Screen.height*1/32), "<color=blue>Height:   </color>" + height, bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*14/32, Screen.width*8/32, Screen.height*1/32), "<color=blue>Weight:   </color>" + weight, bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*15/32, Screen.width*8/32, Screen.height*1/32), stat, bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*17/32, Screen.width*8/32, Screen.height*7/32), "<color=blue>The Story So Far:   </color>" + bio, bioStyle);
		
		GUI.EndGroup();
	}
	
	void assignSelection() {
		if (top && left) {
			GUI.FocusControl("DennisButton");
			selectedLabel = DennisLabel;
			tagline = "Just a regular guy, I guess";
			height = "5ft 10in";
			weight = "178lb";
			stat = "<color=blue>Vision:   </color>20/20";
			bio = "Dennis is a regular guy. So regular, in fact, that it's almost obscene. He lives in a normal " +
				"house with his normal family and works a normal job. He plays tennis and enjoys vanilla ice cream.";
		}
		else if (top && !left) {
			GUI.FocusControl("SwoleButton");
			selectedLabel = SwoleLabel;
			tagline = "Toughest dude this side of the Jurassic period.";
			height = "6ft 9in";
			weight = "387lb";
			stat = "<color=blue>Max Bench:   </color>375lb";
			bio = "He's mean. He's green. He's over 200 million years old. And he has never missed leg day. Not even once.";
		}
		else if (!top && left) {
			GUI.FocusControl("ShivaButton");
			selectedLabel = ShivaLabel;
			tagline = "The many-armed god of Tennis Destruction";
			height = "6ft 2in";
			weight = "210lb";
			stat = "<color=blue>Number of Arms:   </color>4.00";
			bio = "Just exactly how and when 5H1-V4 came to be remains unknown. But we do know this: She has four arms. And " +
				"each one has the strength of 100 arms. That's like, 12 billion arms.";
		}
		else if (!top && !left) {
			GUI.FocusControl("FishButton");
			selectedLabel = FishLabel;
			tagline = "He's a goldfish in tank. Not that kind of tank.";
			height = "2.7in";
			weight = ".07lb";
			stat = "<color=blue>Barrel Diameter:   </color>120mm";
			bio = "Growing up, Colonel Topspin wasn't like all the other goldfish. He was born with a rare genetic defect" +
				" on his Y chromosome that confined him to live in a T36 tank for the rest of his life. This made it clear to" +
					"him from an early age that he was destined for tennis glory.";
		}
	}
}
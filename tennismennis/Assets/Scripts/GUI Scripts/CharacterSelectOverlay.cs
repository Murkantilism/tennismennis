using UnityEngine;
using System.Collections;

public class CharacterSelectOverlay : MonoBehaviour {

	public GUISkin customSkin;
	public Texture2D DennisLabel;
	public Texture2D SwoleLabel;
	public Texture2D ShivaLabel;
	public Texture2D FishLabel;
	
	SaveSelections saveSelection;

<<<<<<< HEAD
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
		if (Input.GetKeyDown("backspace")) { Application.LoadLevel("MainMenu"); }
	}

=======
>>>>>>> 35f23c4e4b1f9cdb76eff83d5d0ae3b1d997190b
	void OnGUI () {
		GUI.skin = customSkin;

		saveSelection = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();

		// Main box
		GUI.BeginGroup (new Rect (Screen.width*8/32, Screen.height*6/32, Screen.width/2, Screen.height*20/32));
		GUI.Box (new Rect(0,0, Screen.width/2, Screen.height*20/32), "");
		GUI.Label (new Rect(Screen.width*1/32,Screen.height*1/64,Screen.width*14/32,Screen.height*4/32), "P1 Character");


<<<<<<< HEAD
		GUI.SetNextControlName("ShivaButton");
		if(GUI.Button(new Rect(0,Screen.height*23/64,buttonSize.x, buttonSize.y), "", p1Style)) {
			characterButton("SH1-V4");
=======
		// Character Select buttons
		if(GUI.Button(new Rect(0,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), "")) {
			saveSelection.WriteCharacterSelection("Dennis");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("CharacterSelect_P2");
>>>>>>> 35f23c4e4b1f9cdb76eff83d5d0ae3b1d997190b
		}
		GUI.Label(new Rect(Screen.width*1/32,Screen.height*6/32,Screen.width*6/32,Screen.height*13/64), DennisLabel);

		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), "")) {
			saveSelection.WriteCharacterSelection("S. Racks");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("CharacterSelect_P2");
		}
		GUI.Label(new Rect(Screen.width*9/32,Screen.height*6/32,Screen.width*6/32,Screen.height*13/64), SwoleLabel);

<<<<<<< HEAD
		GUI.Label(new Rect(Screen.width *2/32, Screen.height*1/32, Screen.width*6/32, Screen.height*2/32), selectedLabel);
		GUI.Label(new Rect(Screen.width *3/32, Screen.height*3/32, Screen.width*4/32, Screen.height*7/32), idle);

		GUI.Label(new Rect(Screen.width *1/32, Screen.height*10/32, Screen.width*8/32, Screen.height*2/32), "\"" + tagline + "\"", bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*13/32, Screen.width*8/32, Screen.height*1/32), "<color=#f2be2c>Height:   </color>" + height, bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*14/32, Screen.width*8/32, Screen.height*1/32), "<color=#f2be2c>Weight:   </color>" + weight, bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*15/32, Screen.width*8/32, Screen.height*1/32), stat, bioStyle);
		GUI.Label(new Rect(Screen.width *1/32, Screen.height*17/32, Screen.width*8/32, Screen.height*7/32), "<color=#f2be2c>The Story So Far:   </color>" + bio, bioStyle);
	
		GUI.EndGroup();
	}

	void characterButton(string name) {
		if(selectAlt) { saveSelection.WriteCharacterSelection(name + " Alt"); }
		else { saveSelection.WriteCharacterSelection(name); }
		DontDestroyOnLoad(saveSelection.gameObject);
		Application.LoadLevel("CharacterSelect_P2");
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
			stat = "<color=#f2be2c>Vision:   </color>20/20";
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
			stat = "<color=#f2be2c>Max Bench:   </color>375lb";
			bio = "He's mean. He's green. He's over 200 million years old. And he has never missed leg day. Not even once.";
		}
		else if (!top && left) {
//			GUI.FocusControl("ShivaButton");
			if (selectAlt) { idle = ShivaAlt; } 
			else { idle = ShivaIdle; }
=======
		if(GUI.Button(new Rect(0,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), "")) {
			saveSelection.WriteCharacterSelection("SH1-V4");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("CharacterSelect_P2");
		}
		GUI.Label(new Rect(Screen.width*1/32,Screen.height*29/64,Screen.width*6/32,Screen.height*13/64), ShivaLabel);
>>>>>>> 35f23c4e4b1f9cdb76eff83d5d0ae3b1d997190b

		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), "")) {
			saveSelection.WriteCharacterSelection("Colonel Topspin");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("CharacterSelect_P2");
		}
<<<<<<< HEAD
		else if (!top && !left) {
//			GUI.FocusControl("FishButton");
			if (selectAlt) { idle = FishAlt; } 
			else { idle = FishIdle; }
=======
		GUI.Label(new Rect(Screen.width*9/32,Screen.height*29/64,Screen.width*6/32,Screen.height*13/64), FishLabel);
>>>>>>> 35f23c4e4b1f9cdb76eff83d5d0ae3b1d997190b

		GUI.EndGroup();
	}
}
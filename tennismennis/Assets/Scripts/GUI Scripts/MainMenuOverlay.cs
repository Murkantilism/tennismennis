using UnityEngine;
using System.Collections;

public class MainMenuOverlay : MonoBehaviour {

	public GUISkin customSkin;
	SaveSelections saveSelection;
	AudioSource asrc;
	public AudioClip intro;
	int item = 0;
	string[] buttons = new string[]{"QuickButton", "CustomButton", "QuitButton"};


	void Awake(){
		asrc = gameObject.GetComponent<AudioSource>();
	}

	void Start() {
		asrc.PlayOneShot(intro, 1.0f);
	}

	void FixedUpdate () {
		if (Input.GetKeyDown("s")) {
			if (item < 2) {item += 1;}
			else {item = 0;} 
		}
		else if (Input.GetKeyDown("w")) {
			if (item > 0) {item -= 1;}
			else {item = 2;}
		}
	}

	void OnGUI () {
		GUI.skin = customSkin;

		saveSelection = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();

		// Background box
		GUI.BeginGroup (new Rect (Screen.width*8/32, Screen.height*6/32, Screen.width/2, Screen.height*20/32));
		GUI.Box(new Rect(0,0,Screen.width/2,Screen.height*20/32),"");
		GUI.Label (new Rect(Screen.width*3/32, Screen.height*3/32, Screen.width*10/32, Screen.height*4/32), "Tennis Mennis");

		// Load a random court with random characters
		GUI.SetNextControlName("QuickButton");
		if(GUI.Button(new Rect(Screen.width*5/32,Screen.height*7/32, Screen.width*6/32,Screen.height*3/32), "Quick Match")) {
			DontDestroyOnLoad(GameObject.Find("SaveSelections"));

			saveSelection.WriteCharacterSelection(randomPlayer());
			saveSelection.WriteCharacterSelection_p2(randomPlayer());
			Application.LoadLevel(randomCourt());
		}

		// Character Select -> Stage Select -> Begin match
		GUI.SetNextControlName("CustomButton");
		if(GUI.Button(new Rect(Screen.width*5/32,Screen.height*11/32,Screen.width*6/32,Screen.height*3/32), "Custom Match")) {
			DontDestroyOnLoad(GameObject.Find("SaveSelections"));
			Application.LoadLevel("CharacterSelect");
		}

		// Quit button
		GUI.SetNextControlName("QuitButton");
		if(GUI.Button(new Rect(Screen.width*5/32,Screen.height*15/32,Screen.width*6/32,Screen.height*3/32), "Quit")) {
			Application.Quit();
		}
		GUI.EndGroup ();

		//GUI.FocusControl(buttons[item]);
	}
	static string randomCourt() {
		int rando = Random.Range(1, 5);
		string court = "Court" + rando.ToString();
		return court;
	}
	static string randomPlayer() {
		int rando = Random.Range(1, 5);
		string player;
		if 		(rando == 1) {player = "Dennis";}
		else if (rando == 2) {player = "S. Racks";}
		else if (rando == 3) {player = "SH1-V4";}
		else 	{player = "Colonel Topspin";};

		return player;
	}
}

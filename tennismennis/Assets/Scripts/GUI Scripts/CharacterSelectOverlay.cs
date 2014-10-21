using UnityEngine;
using System.Collections;

public class CharacterSelectOverlay : MonoBehaviour {

	public GUISkin customSkin;
	public Texture2D DennisLabel;
	public Texture2D SwoleLabel;
	public Texture2D ShivaLabel;
	public Texture2D FishLabel;

	bool left = true;
	bool top = true;

	SaveSelections saveSelection;

	void FixedUpdate () {
		if (Input.GetKeyDown("d") || Input.GetKeyDown("a")) {left = !left;}
		if (Input.GetKeyDown("w") || Input.GetKeyDown("s")) {top = !top;}
	}

	void OnGUI () {
		GUI.skin = customSkin;
		GUIStyle p1Style = GUI.skin.GetStyle("P1Select");

		saveSelection = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();

		// Main box
		GUI.BeginGroup (new Rect (Screen.width*8/32, Screen.height*6/32, Screen.width/2, Screen.height*20/32));
		GUI.Box (new Rect(0,0, Screen.width/2, Screen.height*3/32), "");
		GUI.Label (new Rect(Screen.width*1/32,Screen.height*1/64,Screen.width*14/32,Screen.height*4/32), "P1 Character");
		
		// Character Select buttons
		GUI.SetNextControlName("DennisButton");
		if(GUI.Button(new Rect(0,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), "", p1Style)) {
			saveSelection.WriteCharacterSelection("Dennis");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("CharacterSelect_P2");
		}
		GUI.Label(new Rect(Screen.width*1/32,Screen.height*6/32,Screen.width*6/32,Screen.height*13/64), DennisLabel);

		GUI.SetNextControlName("SwoleButton");
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), "", p1Style)) {
			saveSelection.WriteCharacterSelection("S. Racks");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("CharacterSelect_P2");
		}
		GUI.Label(new Rect(Screen.width*9/32,Screen.height*6/32,Screen.width*6/32,Screen.height*13/64), SwoleLabel);

		GUI.SetNextControlName("ShivaButton");
		if(GUI.Button(new Rect(0,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), "", p1Style)) {
			saveSelection.WriteCharacterSelection("SH1-V4");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("CharacterSelect_P2");
		}
		GUI.Label(new Rect(Screen.width*1/32,Screen.height*29/64,Screen.width*6/32,Screen.height*13/64), ShivaLabel);

		GUI.SetNextControlName("FishButton");
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), "", p1Style)) {
			saveSelection.WriteCharacterSelection("Colonel Topspin");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("CharacterSelect_P2");
		}
		GUI.Label(new Rect(Screen.width*9/32,Screen.height*29/64,Screen.width*6/32,Screen.height*13/64), FishLabel);
		
		GUI.EndGroup();

		if (top && left) {GUI.FocusControl("DennisButton");}
		else if (top && !left) {GUI.FocusControl("SwoleButton");}
		else if (!top && left) {GUI.FocusControl("ShivaButton");}
		else if (!top && !left) {GUI.FocusControl("FishButton");}
	}
}
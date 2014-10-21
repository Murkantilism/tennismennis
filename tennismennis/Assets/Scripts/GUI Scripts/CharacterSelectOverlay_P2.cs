using UnityEngine;
using System.Collections;

public class CharacterSelectOverlay_P2 : MonoBehaviour {
	
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
		GUIStyle p2Style = GUI.skin.GetStyle("P2Select");
		GUIStyle p2Background = GUI.skin.GetStyle("P2Background");
		
		saveSelection = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();
		
		// Main box
		GUI.BeginGroup (new Rect (Screen.width*8/32, Screen.height*6/32, Screen.width/2, Screen.height*20/32));
		GUI.Box (new Rect(0,0, Screen.width/2, Screen.height*20/32), "", p2Background);
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

		if (top && left) {GUI.FocusControl("DennisButton");}
		else if (top && !left) {GUI.FocusControl("SwoleButton");}
		else if (!top && left) {GUI.FocusControl("ShivaButton");}
		else if (!top && !left) {GUI.FocusControl("FishButton");}
	}
}
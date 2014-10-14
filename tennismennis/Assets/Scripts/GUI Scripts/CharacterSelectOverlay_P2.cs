using UnityEngine;
using System.Collections;

public class CharacterSelectOverlay_P2 : MonoBehaviour {
	
	public GUISkin customSkin;
	public Texture2D DennisLabel;
	public Texture2D SwoleLabel;
	public Texture2D ShivaLabel;
	public Texture2D FishLabel;
	Rect backLayer;
	Rect mainLayer;
	
	SaveSelections saveSelection;
	
	void OnGUI () {
		GUI.skin = customSkin;
		
		saveSelection = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();
		
		// Main box
		GUI.BeginGroup (new Rect (Screen.width*8/32, Screen.height*6/32, Screen.width/2, Screen.height*20/32));
		GUI.Box (new Rect(0,0, Screen.width/2, Screen.height*20/32), "");
		GUI.Label (new Rect(Screen.width*1/32,Screen.height*1/64,Screen.width*14/32,Screen.height*4/32), "P2 Character");
		
		// Character Select buttons
		if(GUI.Button(new Rect(0,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), DennisLabel)) {
			saveSelection.WriteCharacterSelection_p2("Dennis");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("StageSelect");
		}
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), SwoleLabel)) {
			saveSelection.WriteCharacterSelection_p2("S. Racks");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("StageSelect");
		}
		if(GUI.Button(new Rect(0,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), ShivaLabel)) {
			saveSelection.WriteCharacterSelection_p2("SH1-V4");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("StageSelect");
		}
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), FishLabel)) {
			saveSelection.WriteCharacterSelection_p2("Colonel Topspin");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("StageSelect");
		}
		GUI.EndGroup();
	}
}
using UnityEngine;
using System.Collections;

public class CharacterSelectOverlay : MonoBehaviour {

	public GUISkin customSkin;
	Rect backLayer;
	Rect mainLayer;
	
	SaveSelections saveSelection;

	void OnGUI () {
		GUI.skin = customSkin;

		saveSelection = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();

		// Background box
		backLayer = new Rect(Screen.width/4,Screen.height/8 - 30,Screen.width/2,Screen.height*3/4 + 30);
		GUI.Box(backLayer, "P1 Select Character!");

		// Main box
		GUIStyle panelStyle =  GUI.skin.GetStyle("CharacterSelect");
		mainLayer = new Rect(Screen.width/4,Screen.height/8 + 30,Screen.width/2,Screen.height*3/4 - 30);
		GUI.Box(mainLayer, "", panelStyle);

		// Character Select buttons
		if(GUI.Button(new Rect(mainLayer.x,mainLayer.y,mainLayer.width/2,mainLayer.height/2), "S. Racks")) {
			saveSelection.WriteCharacterSelection("S. Racks");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(mainLayer.x + mainLayer.width/2,mainLayer.y,mainLayer.width/2,mainLayer.height/2), "Dennis")) {
			saveSelection.WriteCharacterSelection("Dennis");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(mainLayer.x,mainLayer.y + mainLayer.height/2,mainLayer.width/2,mainLayer.height/2), "SH1-V4")) {
			saveSelection.WriteCharacterSelection("SH1-V4");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(mainLayer.x + mainLayer.width/2,mainLayer.y + mainLayer.height/2,mainLayer.width/2,mainLayer.height/2), "Colonel Topspin")) {
			saveSelection.WriteCharacterSelection("Colonel Topspin");
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel(2);
		}
	}
}
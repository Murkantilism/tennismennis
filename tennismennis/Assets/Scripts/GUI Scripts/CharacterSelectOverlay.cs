using UnityEngine;
using System.Collections;

public class CharacterSelectOverlay : MonoBehaviour {

	public GUISkin customSkin;
	Rect backLayer;
	Rect mainLayer;

	void OnGUI () {
		GUI.skin = customSkin;


		// Background box
		backLayer = new Rect(Screen.width/4,Screen.height/8 - 30,Screen.width/2,Screen.height*3/4 + 30);
		GUI.Box(backLayer, "Select Character!");

		// Main box
		GUIStyle panelStyle =  GUI.skin.GetStyle("CharacterSelect");
		mainLayer = new Rect(Screen.width/4,Screen.height/8 + 30,Screen.width/2,Screen.height*3/4 - 30);
		GUI.Box(mainLayer, "", panelStyle);

		// Character Select buttons
		if(GUI.Button(new Rect(mainLayer.x,mainLayer.y,mainLayer.width/2,mainLayer.height/2), "S. Racks")) {
			Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(mainLayer.x + mainLayer.width/2,mainLayer.y,mainLayer.width/2,mainLayer.height/2), "Dennis")) {
			Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(mainLayer.x,mainLayer.y + mainLayer.height/2,mainLayer.width/2,mainLayer.height/2), "SH1-V4")) {
			Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(mainLayer.x + mainLayer.width/2,mainLayer.y + mainLayer.height/2,mainLayer.width/2,mainLayer.height/2), "Colonel Topspin")) {
			Application.LoadLevel(2);
		}
	}
}
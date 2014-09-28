using UnityEngine;
using System.Collections;

public class StageSelectOverlay : MonoBehaviour {
	
	public GUISkin customSkin;
	Rect backLayer;
	Rect mainLayer;
	
	void OnGUI () {
		GUI.skin = customSkin;

		// Background box
		backLayer = new Rect(Screen.width/4,Screen.height/8 - 30,Screen.width/2,Screen.height*3/4 + 30);
		GUI.Box(backLayer, "Select Court");
		
		// Main box
		GUIStyle panelStyle =  GUI.skin.GetStyle("CharacterSelect");
		mainLayer = new Rect(Screen.width/4,Screen.height/8 + 30,Screen.width/2,Screen.height*3/4 - 30);
		GUI.Box(mainLayer, "", panelStyle);
		
		// Character Select buttons
		if(GUI.Button(new Rect(mainLayer.x,mainLayer.y,mainLayer.width/2,mainLayer.height/2), "Racknasium")) {
			Application.LoadLevel(3);
		}
		if(GUI.Button(new Rect(mainLayer.x + mainLayer.width/2,mainLayer.y,mainLayer.width/2,mainLayer.height/2), "Dennis Tennis")) {
			Application.LoadLevel(3);
		}
		if(GUI.Button(new Rect(mainLayer.x,mainLayer.y + mainLayer.height/2,mainLayer.width/2,mainLayer.height/2), "5H1-V4 D0me")) {
			Application.LoadLevel(3);
		}
		if(GUI.Button(new Rect(mainLayer.x + mainLayer.width/2,mainLayer.y + mainLayer.height/2,mainLayer.width/2,mainLayer.height/2), "The Other Tank")) {
			Application.LoadLevel(3);
		}
	}
}
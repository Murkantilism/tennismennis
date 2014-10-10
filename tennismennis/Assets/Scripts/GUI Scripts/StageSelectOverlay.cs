using UnityEngine;
using System.Collections;

public class StageSelectOverlay : MonoBehaviour {
	
	public GUISkin customSkin;
	Rect backLayer;
	Rect mainLayer;
	
	SaveSelections saveSelection;
	
	void OnGUI () {
		GUI.skin = customSkin;
		
		saveSelection = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();

		// Background box
		backLayer = new Rect(Screen.width/4,Screen.height/8 - 30,Screen.width/2,Screen.height*3/4 + 30);
		GUI.Box(backLayer, "Select Court");
		
		// Main box
		GUIStyle panelStyle =  GUI.skin.GetStyle("CharacterSelect");
		mainLayer = new Rect(Screen.width/4,Screen.height/8 + 30,Screen.width/2,Screen.height*3/4 - 30);
		GUI.Box(mainLayer, "", panelStyle);
		
		// Court Select buttons
		if(GUI.Button(new Rect(mainLayer.x + mainLayer.width/2,mainLayer.y,mainLayer.width/2,mainLayer.height/2), "Dennis Tennis")) {
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel(4);
		}
		if(GUI.Button(new Rect(mainLayer.x,mainLayer.y,mainLayer.width/2,mainLayer.height/2), "Racknasium")) {
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel(5);
		}
		if(GUI.Button(new Rect(mainLayer.x,mainLayer.y + mainLayer.height/2,mainLayer.width/2,mainLayer.height/2), "5H1-V4 D0me")) {
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel(6);
		}
		if(GUI.Button(new Rect(mainLayer.x + mainLayer.width/2,mainLayer.y + mainLayer.height/2,mainLayer.width/2,mainLayer.height/2), "The Other Tank")) {
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel(7);
		}
	}
}
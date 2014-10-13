using UnityEngine;
using System.Collections;

public class MainMenuOverlay : MonoBehaviour {

	public GUISkin customSkin;

	void OnGUI () {
		GUI.skin = customSkin;

		// Background box
		GUI.Box(new Rect(Screen.width/4,Screen.height/8,Screen.width/2,Screen.height*3/4), "Tennis Mennis");

		// Load the default scene
		if(GUI.Button(new Rect(Screen.width*3/8,Screen.height*3/8, Screen.width/4,Screen.height/8), "Quick Match")) {
			DontDestroyOnLoad(GameObject.Find("SaveSelections"));
			Application.LoadLevel("Court1");
		}

		// Character Select -> Stage Select -> Begin match
		if(GUI.Button(new Rect(Screen.width*3/8,Screen.height*1/2 + 10,Screen.width/4,Screen.height/8), "Custom Match")) {
			DontDestroyOnLoad(GameObject.Find("SaveSelections"));
			Application.LoadLevel("CharacterSelect");
		}

		// Quit button
		if(GUI.Button(new Rect(Screen.width*3/8,Screen.height*5/8 + 20,Screen.width/4,Screen.height/8), "Quit")) {
			Application.Quit();
		}
	}
}

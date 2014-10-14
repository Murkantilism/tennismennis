using UnityEngine;
using System.Collections;

public class MainMenuOverlay : MonoBehaviour {

	public GUISkin customSkin;

	void OnGUI () {
		GUI.skin = customSkin;

		// Background box
		GUI.BeginGroup (new Rect (Screen.width*8/32, Screen.height*6/32, Screen.width/2, Screen.height*20/32));
		GUI.Box(new Rect(0,0,Screen.width/2,Screen.height*20/32),"");
		GUI.Label (new Rect(Screen.width*3/32, Screen.height*3/32, Screen.width*10/32, Screen.height*4/32), "Tennis Mennis");

		// Load the default scene
		if(GUI.Button(new Rect(Screen.width*5/32,Screen.height*7/32, Screen.width*6/32,Screen.height*3/32), "Quick Match")) {
			DontDestroyOnLoad(GameObject.Find("SaveSelections"));
			Application.LoadLevel("Court1");
		}

		// Character Select -> Stage Select -> Begin match
		if(GUI.Button(new Rect(Screen.width*5/32,Screen.height*11/32,Screen.width*6/32,Screen.height*3/32), "Custom Match")) {
			DontDestroyOnLoad(GameObject.Find("SaveSelections"));
			Application.LoadLevel("CharacterSelect");
		}

		// Quit button
		if(GUI.Button(new Rect(Screen.width*5/32,Screen.height*15/32,Screen.width*6/32,Screen.height*3/32), "Quit")) {
			Application.Quit();
		}
		GUI.EndGroup ();
	}
}

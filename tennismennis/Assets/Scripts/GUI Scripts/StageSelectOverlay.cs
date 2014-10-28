using UnityEngine;
using System.Collections;

public class StageSelectOverlay : MonoBehaviour {
	
	public GUISkin customSkin;
	Rect backLayer;
	Rect mainLayer;
	
	SaveSelections saveSelection;
	
	void OnGUI () {
		GUI.skin = customSkin;
		GUIStyle DennisStyle  =  GUI.skin.GetStyle("DennisOpenButton");
		GUIStyle GymStyle  =  GUI.skin.GetStyle("JurassicGymButton");
		GUIStyle DomeStyle  =  GUI.skin.GetStyle("ShivaDomeButton");
		GUIStyle TankStyle  =  GUI.skin.GetStyle("TankButton");

		
		saveSelection = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();

		// Main box
		GUI.BeginGroup (new Rect (Screen.width*8/32, Screen.height*6/32, Screen.width/2, Screen.height*20/32));
		GUI.Box (new Rect(0,0, Screen.width/2, Screen.height*20/32), "");
		GUI.Label (new Rect(Screen.width*1/32,Screen.height*1/64,Screen.width*14/32,Screen.height*4/32), "Select Court");
		
		// Court Select buttons
		if(GUI.Button(new Rect(0,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), "Dennis Open", DennisStyle)) {
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("Court1");
		}
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), "Jurassic Gym", GymStyle)) {
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("Court2");
		}
		if(GUI.Button(new Rect(0,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), "5H1-V4 D0me", DomeStyle)) {
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("Court3");
		}
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), "The Other Tank", TankStyle)) {
			DontDestroyOnLoad(saveSelection.gameObject);
			Application.LoadLevel("Court4");
		}
		GUI.EndGroup();
	}
}
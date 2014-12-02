using UnityEngine;
using System.Collections;

public class StageSelectOverlay : MonoBehaviour {
	
	public GUISkin customSkin;
<<<<<<< HEAD
//	Rect backLayer;
//	Rect mainLayer;

	bool left = true;
	bool top = true;

	AudioSource asrc;
	public AudioClip intro;
	public AudioClip dennisOpen;
	public AudioClip jurassicGym;
	public AudioClip mainframe;
	public AudioClip fishtank;

	SaveSelections saveSelection;

	void Awake(){
		asrc = gameObject.GetComponent<AudioSource>();
	}
	
	void Start() {
		asrc.PlayOneShot(intro, 1.0f);
	}


	void FixedUpdate () {
		if (Input.GetKeyDown("d") || Input.GetKeyDown("a")) {left = !left;}
		if (Input.GetKeyDown("w") || Input.GetKeyDown("s")) {top = !top;}

		if (Input.GetKeyDown("backspace")) { Application.LoadLevel("CharacterSelect_P2"); }
	}

=======
	Rect backLayer;
	Rect mainLayer;
	
	SaveSelections saveSelection;
	
>>>>>>> 35f23c4e4b1f9cdb76eff83d5d0ae3b1d997190b
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
		//	asrc.PlayOneShot(dennisOpen, 1.0f);
			Application.LoadLevel("Court1");
		}
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*3/32,Screen.width*8/32,Screen.height*17/64), "Jurassic Gym", GymStyle)) {
			DontDestroyOnLoad(saveSelection.gameObject);
		//	asrc.PlayOneShot(jurassicGym, 1.0f);
			Application.LoadLevel("Court2");
		}
		if(GUI.Button(new Rect(0,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), "5H1-V4 D0me", DomeStyle)) {
			DontDestroyOnLoad(saveSelection.gameObject);
		//	asrc.PlayOneShot(mainframe, 1.0f);
			Application.LoadLevel("Court3");
		}
		if(GUI.Button(new Rect(Screen.width*8/32,Screen.height*23/64,Screen.width*8/32,Screen.height*17/64), "The Other Tank", TankStyle)) {
			DontDestroyOnLoad(saveSelection.gameObject);
		//	asrc.PlayOneShot(fishtank, 1.0f);
			Application.LoadLevel("Court4");
		}
		GUI.EndGroup();
<<<<<<< HEAD
		/*
		if (top && left) {GUI.FocusControl("DennisButton");}
		else if (top && !left) {GUI.FocusControl("GymButton");}
		else if (!top && left) {GUI.FocusControl("ShivaButton");}
		else if (!top && !left) {GUI.FocusControl("TankButton");}
		*/
=======
>>>>>>> 35f23c4e4b1f9cdb76eff83d5d0ae3b1d997190b
	}
}
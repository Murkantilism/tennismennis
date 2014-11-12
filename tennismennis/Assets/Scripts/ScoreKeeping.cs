using UnityEngine;
using System.Collections;

public class ScoreKeeping : MonoBehaviour {
	public GUISkin guiSkin;
	
	public Texture2D pointLabel;
	public int player1_score = 0;
	public int player2_score = 0;
	int maxScore = 3;
	
	float point;
	
	EndOfRound endOfRound;
	
	SaveSelections savedSelections;
	
	AudioSource asrc;
	
	AudioClip player1_victory;
	AudioClip player2_victory;
	
	public AudioClip p1_pointScored0;
	public AudioClip p1_pointScored1;

	public AudioClip p2_pointScored0;
	public AudioClip p2_pointScored1;
		
	bool victory_played = false;
	
	void Awake(){
		asrc = gameObject.GetComponent<AudioSource>();
		
		savedSelections = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();
	}
	
	// Use this for initialization
	void Start () {
		endOfRound = GameObject.Find ("EndOfRound").GetComponent<EndOfRound> ();
		point = 0.0f;
		
		// Load the victory SFX for player 1
		if(player1_victory == null){
			if(savedSelections.selected_p1 == "S. Racks"){
				player1_victory = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_Victory", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "SH1-V4"){
				player1_victory = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_Victory", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "Colonel Topspin"){
				player1_victory = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_Victory", typeof(AudioClip)));
			}else{
				player1_victory = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_Victory", typeof(AudioClip)));
			}
		}
		// Load the victory SFX for player 2
		if(player2_victory == null){
			if(savedSelections.selected_p2 == "S. Racks"){
				player2_victory = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_Victory", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "SH1-V4"){
				player2_victory = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_Victory", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "Colonel Topspin"){
				player2_victory = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_Victory", typeof(AudioClip)));
			}else{
				player2_victory = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_Victory", typeof(AudioClip)));
			}
		}

		
		// Load the point scored FX for player 1
		if(p1_pointScored0 == null){
			if(savedSelections.selected_p1 == "S. Racks"){
				p1_pointScored0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_PointScored0", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "SH1-V4"){
				p1_pointScored0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_PointScored0", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "Colonel Topspin"){
				p1_pointScored0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_PointScored0", typeof(AudioClip)));
			}else{
				p1_pointScored0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_PointScored0", typeof(AudioClip)));
			}
		}
		if(p1_pointScored1 == null){
			if(savedSelections.selected_p1 == "S. Racks"){
				p1_pointScored1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_PointScored1", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "SH1-V4"){
				p1_pointScored1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_PointScored1", typeof(AudioClip)));
			}else if(savedSelections.selected_p1 == "Colonel Topspin"){
				p1_pointScored1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_PointScored1", typeof(AudioClip)));
			}else{
				p1_pointScored1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_PointScored1", typeof(AudioClip)));
			}
		}
		

		// Load the point scored FX for player 2
		if(p2_pointScored0 == null){
			if(savedSelections.selected_p2 == "S. Racks"){
				p2_pointScored0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_PointScored0", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "SH1-V4"){
				p2_pointScored0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_PointScored0", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "Colonel Topspin"){
				p2_pointScored0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_PointScored0", typeof(AudioClip)));
			}else{
				p2_pointScored0 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_PointScored0", typeof(AudioClip)));
			}
		}
		if(p2_pointScored1 == null){
			if(savedSelections.selected_p2 == "S. Racks"){
				p2_pointScored1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Swole/Swole_PointScored1", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "SH1-V4"){
				p2_pointScored1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Shiva/Female/ShivaF_PointScored1", typeof(AudioClip)));
			}else if(savedSelections.selected_p2 == "Colonel Topspin"){
				p2_pointScored1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Topspin/Topspin_PointScored1", typeof(AudioClip)));
			}else{
				p2_pointScored1 = (AudioClip)AudioClip.Instantiate(Resources.Load("SFX/Dennis/Dennis_PointScored1", typeof(AudioClip)));
			}
		}
	}
	
	// Increment player scores based on who scored!
	// Recieves calls from OutOfBoundsDetection.cs && Ball.cs
	public void PointScored(string whoScored){
		if (whoScored == "Player1") {
			player1_score += 1;
			// If the max score hasn't been reached, randomly play one of the two point scored SFX
			if(!(player1_score >= maxScore)){
				if(Random.Range(0, 2) == 0){
					asrc.PlayOneShot(p1_pointScored0, 1.0f);
				}else{
					asrc.PlayOneShot(p1_pointScored1, 1.0f);
				}
			}
		}else if(whoScored == "Player2"){
			player2_score += 1;
			// If the max score hasn't been reached, randomly play one of the two point scored SFX
			if(!(player2_score >= maxScore)){
				if(Random.Range(0, 2) == 0){
					asrc.PlayOneShot(p2_pointScored0, 1.0f);
				}else{
					asrc.PlayOneShot(p2_pointScored1, 1.0f);
				}
			}
		}
		point = 1;
		endOfRound.SendMessage ("ResetRound");
		StartCoroutine("DisplayPointText");
	}
	
	void OnGUI(){
		// Assign this GUI's skin to the skin assigned via inspector
		GUI.skin = guiSkin;
		// Create a GUI style for the score based on the skin's label
		GUIStyle scoreStyle = GUI.skin.label;
		
		GUI.Label(new Rect (Screen.width * 12/32, Screen.height*7/64, 100, Screen.height*3/32), player1_score.ToString("D2"), scoreStyle);
		
		GUI.Label(new Rect (Screen.width * 18/32, Screen.height*7/64, 100, Screen.height*3/32), player2_score.ToString("D2"), scoreStyle);
		
		if (point > 0){
			GUI.Label (new Rect(Screen.width*12/32, Screen.height*10/32,Screen.width*8/32, Screen.height*6/32), pointLabel);
		}
	}
	void Update(){
		if(player1_score >= maxScore){
			if(victory_played == false){
				victory_played = true;
				asrc.PlayOneShot(player1_victory, 1.0f);
				StartCoroutine(GameOver());
			}
		}
		if(player2_score >= maxScore){
			if(victory_played == false){
				victory_played = true;
				asrc.PlayOneShot(player2_victory, 1.0f);
				StartCoroutine(GameOver());
			}
		}
	}
	
	IEnumerator DisplayPointText(){
		// While we are paused, subtract from "point"
		while(Time.timeScale == 0.0f){
			float pauseEndTime = Time.realtimeSinceStartup + 1f;
			while (Time.realtimeSinceStartup < pauseEndTime){
				yield return 0;
			}
			point -= 1.0f; // Subtracting by 0.5 will put "Point" text up for 2 seconds
		}
	}
	
	IEnumerator GameOver(){
		yield return new WaitForSeconds(0.1f);
		Application.LoadLevel(0);
	}
}
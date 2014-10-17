using UnityEngine;
using System.Collections;

public class LoadSelections : MonoBehaviour {
	
	SaveSelections savedSelections;
	
	PlayerMovement playerMovement_p1;
	PlayerMovement playerMovement_p2;

	RuntimeAnimatorController anim1;
	RuntimeAnimatorController anim2;

	void Awake(){
		savedSelections = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();
		playerMovement_p1 = GameObject.Find("Player1").GetComponent<PlayerMovement>();
		playerMovement_p2 = GameObject.Find("Player2").GetComponent<PlayerMovement>();
		
		// Load player 1's character (default = Dennis)
		if(savedSelections.selected_p1 == "S. Racks"){
			anim1 = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Anims/SRacks", typeof(RuntimeAnimatorController)));
		}else if(savedSelections.selected_p1 == "SH1-V4"){
			anim1 = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Anims/SH1V4", typeof(RuntimeAnimatorController)));
		}else if(savedSelections.selected_p1 == "Colonel Topspin"){
			anim1 = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Anims/ColonelTopspin", typeof(RuntimeAnimatorController)));
		}else{
			anim1 = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Anims/Dennis", typeof(RuntimeAnimatorController)));
		}
		// Set the animator controller
		playerMovement_p1._animator.runtimeAnimatorController = anim1;
		
		// Load player 2's character (default = S. Racks)
		if(savedSelections.selected_p2 == "Dennis"){
			anim2 = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Anims/Dennis", typeof(RuntimeAnimatorController)));
		}else if(savedSelections.selected_p2 == "SH1-V4"){
			anim2 = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Anims/SH1V4", typeof(RuntimeAnimatorController)));
		}else if(savedSelections.selected_p2 == "Colonel Topspin"){
			anim2 = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Anims/ColonelTopspin", typeof(RuntimeAnimatorController)));
		}else{
			anim2 = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Anims/SRacks", typeof(RuntimeAnimatorController)));
		}
		// Set the animator controller
		playerMovement_p2._animator.runtimeAnimatorController = anim2;
		
		Debug.Log("P1 LOADED CHARACTER: " + savedSelections.selected_p1 + " | " + playerMovement_p1._animator.runtimeAnimatorController.ToString());
		Debug.Log("P2 LOADED CHARACTER: " + savedSelections.selected_p2 + " | " + playerMovement_p2._animator.runtimeAnimatorController.ToString());
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

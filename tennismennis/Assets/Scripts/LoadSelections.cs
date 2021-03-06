                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                using UnityEngine;
using System.Collections;

public class LoadSelections : MonoBehaviour {
	
	SaveSelections savedSelections;
	
	Player1 player_1;
	Player2 player_2;
	
	RuntimeAnimatorController anim1;
	RuntimeAnimatorController anim2;
	
	void Awake(){
		savedSelections = GameObject.Find("SaveSelections").GetComponent<SaveSelections>();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(player_1 == null){
			player_1 = GameObject.Find("Player1").GetComponent<Player1>();
		}
		
		if(player_2 == null){
			player_2 = GameObject.Find("Player2").GetComponent<Player2>();
		}
		
		if(anim1 == null){
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
		}else{
			// Set the animator controller
			player_1._animator.runtimeAnimatorController = anim1;
		}
	
		if(anim2 == null){
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
		}else{
			// Set the animator controller
			player_2._animator.runtimeAnimatorController = anim2;
		}
	}
}
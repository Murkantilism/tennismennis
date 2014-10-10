using UnityEngine;
using System.Collections;

public class SaveSelections : MonoBehaviour {

	public string selected_p1 = "Dennis"; //kappa
	public string selected_p2 = "S. Racks";
	
	public void WriteCharacterSelection(string character){
		selected_p1 = character;
	}
	
	public void WriteCharacterSelection_p2(string character){
		selected_p2 = character;
	}
}

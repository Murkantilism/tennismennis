using UnityEngine;
using System.Collections;
using InControl;

public class PlayerControls : MonoBehaviour {

	InputDevice inputDevice;

	GameObject player1;
	GameObject player2;

	GameObject racket1;
	GameObject racket2;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("Player1");
		player2 = GameObject.Find ("Player2");

		racket1 = GameObject.Find ("racket_p1");
		racket2 = GameObject.Find ("racket_p2");

		InvokeRepeating ("InputMessenger", 0, 1);
		InvokeRepeating ("DetectAttachDetach", 0, 1);
	}

	// Check if controllers are present and send the corresponding message
	void InputMessenger(){
		if (CheckIfKeyboardShouldBeEnabled () == true) {
			SendInput(true);
		}else{
			SendInput(false);
		}
	}

	// Check if there is controller input. If none are detected,
	// return true to enable keyboard input.
	public bool CheckIfKeyboardShouldBeEnabled(){
		try{
			if(InputManager.Devices.Count == 0){
				Debug.LogWarning("No Controller Detected");
				return true;
			}else if(InputManager.Devices.Count == 1){
				Debug.LogWarning("1 Controller Detected, Plug in 1 more");
				return true;
			}else if(InputManager.Devices.Count == 2){
				return false;
			}else if(InputManager.Devices.Count > 2){
				Debug.Log("More than 2 Controllers Detected, Unplug Extras");
				return false;
			}else{
				return true;
			}
		}catch(UnityException e){
			Debug.LogError(e.ToString());
			return true;
		}
	}

	public void DetectAttachDetach(){
		InputManager.OnDeviceAttached += inputDevice => Debug.Log ("Controller " + inputDevice.Name + " attached");
		InputManager.OnDeviceDetached += inputDevice => Debug.Log ("Controller " + inputDevice.Name + " detached");
	}

	// Send message to PlayerMovement and RacketToss scripts
	void SendInput(bool keyboard_input_enabled){
		player1.SendMessage ("GetInputSettings", keyboard_input_enabled);
		player2.SendMessage ("GetInputSettings", keyboard_input_enabled);

		racket1.SendMessage ("GetInputSettings", keyboard_input_enabled);
		racket2.SendMessage ("GetInputSettings", keyboard_input_enabled);
	}
}

using UnityEngine;
using System.Collections;

public class Powerup_decoy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Actives the power up on the player given (true = player 1, false = player 2)
	public void ActivatePowerup(bool player){
		Debug.Log("DECOY ACTIVATED");
		// Get the real ball's script to access the force vector
		BallMovement ballMvmt = GameObject.Find("Ball").GetComponent<BallMovement>();
		
		// Load a ball prefab from the Resources folder
		GameObject decoy = (GameObject)Instantiate(Resources.Load("Prefabs/Ball", typeof(GameObject)), ballMvmt.gameObject.transform.position, Quaternion.identity);
		
		// Rename it so it cannot affect score/be hit by anything like a real ball can
		decoy.name = "Decoy";
				
		// Get the decoy's script
		BallMovement decoyMvmt = decoy.GetComponent<BallMovement>();
		
		// Move the decoy based on the real ball's force vector
		if(player){
			decoyMvmt.TennisForce(new Vector3(ballMvmt.forceVector.x + Random.Range(0, 5), ballMvmt.forceVector.y + Random.Range(-5, 5)));
		}else{
			decoyMvmt.TennisForce(new Vector3(ballMvmt.forceVector.x + Random.Range(-5, 0), ballMvmt.forceVector.y + Random.Range(-5, 5)));
		}
	}
	
	public void DeactivatePowerup(bool player){
		Destroy(GameObject.Find("Decoy"));
		Destroy(this);
	}
}

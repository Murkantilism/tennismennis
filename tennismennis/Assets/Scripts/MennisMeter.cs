using UnityEngine;
using System.Collections;

public class MennisMeter : MonoBehaviour {
	
	int currentMennis = 0;
	int startingMennis = 40;
	int cost_racketToss = 10;
	int cost_superMove = 100;
	int cost_powerShot = 1; // To be multiplied by charge (1 to 10)
	
	int accumulationAmount = 1; // 1 mennis
	int accumulationRate = 1; // Per second

	public void DecrementMennis_racketToss(){
		currentMennis -= cost_racketToss;
	}
	
	public void DecrementMennis_superMove(){
		currentMennis -= cost_superMove;
	}
	
	public void DecrementMennis_powerShot(int charge){
		currentMennis -= cost_powerShot * charge;
	}

	// Use this for initialization
	void Start () {
		currentMennis += startingMennis;
		// Start accumulating 1 mennis per second
		InvokeRepeating("AccumulateMennis", 0, accumulationRate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Accumulate 1 mennis/second
	void AccumulateMennis(){
		currentMennis += accumulationAmount;
	}

}

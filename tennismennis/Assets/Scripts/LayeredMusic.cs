using UnityEngine;
using System.Collections;

public class LayeredMusic : MonoBehaviour {
	// Audio source for each layer
	public AudioSource layer0src;
	public AudioSource layer1src;
	public AudioSource layer2src;
	public AudioSource layer3src;

	// All of the audio layer clips (assigned via inspector)
	public AudioClip layer0_80bpm;
	public AudioClip layer0_100bpm;
	public AudioClip layer0_120bpm;
	
	public AudioClip layer1_80bpm;
	public AudioClip layer1_100bpm;
	public AudioClip layer1_120bpm;
	
	public AudioClip layer2_80bpm;
	public AudioClip layer2_100bpm;
	public AudioClip layer2_120bpm;
	
	public AudioClip layer3_80bpm;
	public AudioClip layer3_100bpm;
	public AudioClip layer3_120bpm;
	
	BallMovement ballMvnt;

	// Use this for initialization
	void Start () {
		ballMvnt = GameObject.Find("Ball").GetComponent<BallMovement>();
		
		// Get all of the the child go's audio sources
		AudioSource[] children_asrcs = GetComponentsInChildren<AudioSource>();
		// Assign them
		layer0src = children_asrcs[0];
		layer1src = children_asrcs[1];
		layer2src = children_asrcs[2];
		layer3src = children_asrcs[3];
		// Play the first layer at 80 bpm
		layer0src.PlayOneShot(layer0_80bpm, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		Volleys();
	}
	
	void Volleys(){
		// After 3, 6, and 9 volleys play the layered clips
		if((ballMvnt.numHits / 2 > 3) && (ballMvnt.numHits / 2 < 6)){
			// Wait until the first layer finishes, then play the next
			if(layer0src.isPlaying == false){
				Debug.Log("LAYER 1 80BPM");
				PlayClipOneTime(layer0src, layer0_80bpm, 1.0f);
				PlayClipOneTime(layer1src, layer1_80bpm, 1.0f);
			}
		}else if((ballMvnt.numHits / 2 > 6) && (ballMvnt.numHits / 2 < 9)){
			// Wait until the previous layer finishes, then play the next
			if(layer1src.isPlaying == false){
				Debug.Log("LAYER 2 100BPM");
				PlayClipOneTime(layer0src, layer0_100bpm, 1.0f);
				PlayClipOneTime(layer1src, layer1_100bpm, 1.0f);
				PlayClipOneTime(layer2src, layer2_100bpm, 1.0f);
			}
		}else if(ballMvnt.numHits / 2 > 9){
			// Wait until the previous layer finishes, then play the next
			if(layer2src.isPlaying == false){
				Debug.Log("LAYER 3 120BPM");
				PlayClipOneTime(layer0src, layer0_120bpm, 1.0f);
				PlayClipOneTime(layer1src, layer1_120bpm, 1.0f);
				PlayClipOneTime(layer2src, layer2_120bpm, 1.0f);
				PlayClipOneTime(layer3src, layer3_120bpm, 1.0f);
			}
		// If enough volleys haven't happened, keep playing base layer
		}else if(ballMvnt.numHits / 2 < 3){
		    Debug.Log("LAYER 0 80BPM");
			if(layer0src.isPlaying == false){
				PlayClipOneTime(layer0src, layer0_80bpm, 1.0f);
			}
		}
	}
	
	// Had to write my own version of AudioSource.PlayOneShot() because apparently that
	// function doesn't affect the value of AudioSource.isPlaying(), which is really 
	// fucking important. #ThanksObama
	void PlayClipOneTime(AudioSource asrc, AudioClip aclip, float volume){
		asrc.clip = aclip;
		asrc.volume = volume;
		asrc.Play();
	}
}
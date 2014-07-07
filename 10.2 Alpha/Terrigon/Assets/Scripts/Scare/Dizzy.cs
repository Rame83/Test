using UnityEngine;
using System.Collections;

public class Dizzy : MonoBehaviour {
	
// Private variables	
	private MotionBlur mBlur;
	private SanityManager sanityScript;

	void Start(){
// Assign variables through code
		sanityScript = GameObject.Find("Character").GetComponent<SanityManager>();
		mBlur = GameObject.Find("EmitterCamera").GetComponent<MotionBlur>();
		mBlur.enabled = false;
	}
		
	void Update() {
		ManageBlurAmount();
	}
	
	void ManageBlurAmount() {
// Turn mBlur on/off and set the blurAmount based on the currentSanity
		if (sanityScript.CurrentSanity >= 90 ) {
			mBlur.enabled = false;
		}
		
		if (sanityScript.CurrentSanity < 90 && sanityScript.CurrentSanity >= 80) {
			mBlur.enabled = true;
			mBlur.blurAmount = 0.2F;
		}
		
		if (sanityScript.CurrentSanity < 80 && sanityScript.CurrentSanity >= 60) {
			mBlur.enabled = true;
			mBlur.blurAmount = 0.3F;
		}
		
		if (sanityScript.CurrentSanity < 60 && sanityScript.CurrentSanity >= 40) {
			mBlur.enabled = true;
			mBlur.blurAmount = 0.4F;
		}
		if (sanityScript.CurrentSanity < 40 && sanityScript.CurrentSanity >= 20) {
			mBlur.enabled = true;
			mBlur.blurAmount = 0.5F;
		}
	}	
}

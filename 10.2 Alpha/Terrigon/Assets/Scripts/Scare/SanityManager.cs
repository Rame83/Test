using UnityEngine;
using System.Collections;

public class SanityManager : MonoBehaviour {

	private float currentSanity = 100.0F;
	private float maxSanity = 100.0F;
	private float minSanity = 0.0F;
	
	void Update () {
		ClampSanity();
	}
	
// Make sure sanity stays between 0-100;	
	void ClampSanity() {
		if (currentSanity > 100) {
			currentSanity = maxSanity;
		}
		
		if (currentSanity < 0) {
			currentSanity = minSanity;
		}
	}
	
// Getter and setter voor de currentSanity, so other scripts(DizzyScript.cs, ScareScript.cs) can access it	
	public float CurrentSanity {
		get {
			return currentSanity;
		}
		set {
			currentSanity = value;
		}
	}
}

using UnityEngine;
using System.Collections;

public class EmitterTimer : MonoBehaviour {

	private float emitterTime = 20.0F;
	private float emitterTimer;
	private bool timer;

	void Start () {
		timer = false;
	}
	
	void Update () {
		if (Emitter.IsActivated && !timer){
			timer = true;
		}

// Start timer		
		if (timer){
			emitterTimer -= Time.deltaTime;
		}
		
// Turn emitter off when timer is 0
		if (emitterTimer <= 0.0F){
			Emitter.IsActivated = false;
			timer = false;
		}
	}
	
// Public function so othr script (Emitter.cs) can reset the timer
	public void ResetTimer() {
		emitterTimer = emitterTime;
	}
}

using UnityEngine;
using System.Collections;

public class EmitterAnimation : MonoBehaviour {

    private Transform bottomTransform;
	private Transform bottomTransform2;
	private Transform middleTransform;
	private Transform middleTransform2;
    private Transform topTransform;
	private Transform topTransform2;
	public GameObject emitterPart;
	
	// Emittersounds
	public AudioClip charge;
	public AudioClip discharge;
	public AudioClip activated;
	private bool playDischarge = false;
	private bool playActivated = false;

	// Use this for initialization
	void Start () {
        emitterPart.SetActive(false);
		bottomTransform = gameObject.transform.FindChild("Emitter rev7").transform.FindChild("ring_1").FindChild("polySurface9");
		bottomTransform2 = gameObject.transform.FindChild("Emitter rev7").transform.FindChild("ring_1").FindChild("polySurface10");
		middleTransform = gameObject.transform.FindChild("Emitter rev7").transform.FindChild("ring_2").FindChild("polySurface11");
		middleTransform2 = gameObject.transform.FindChild("Emitter rev7").transform.FindChild("ring_2").FindChild("polySurface12");
		topTransform = gameObject.transform.FindChild("Emitter rev7").transform.FindChild("ring_3").FindChild("polySurface13");
		topTransform2 = gameObject.transform.FindChild("Emitter rev7").transform.FindChild("ring_3").FindChild("polySurface14");
	}
	
	// Update is called once per frame
	void Update () {
		if(Emitter.IsActivated){
			emitterPart.SetActive(true);
			playDischarge = true;
	        bottomTransform.Rotate(new Vector3(0, 2, 0), 1.5f);
			bottomTransform2.Rotate(new Vector3(0, 2, 0), 1.5f);
			middleTransform.Rotate(new Vector3(0, -2, 0), 2.0f);
			middleTransform2.Rotate(new Vector3(0, -2, 0), 2.0f);
	        topTransform.Rotate(new Vector3(0, 2, 0), 1.5f);
			topTransform2.Rotate(new Vector3(0, 2, 0), 1.5f);
			PlaySound();
	    }else{
	    	emitterPart.SetActive(false);
	    	if (playDischarge){
	    		audio.PlayOneShot(discharge);
	    		playDischarge = false;
	    	}	
	    }
	}
	
	void PlaySound(){
		if (playActivated){ 
			audio.PlayOneShot(activated);
			playActivated = false;
		}
	}
	
	public bool PlayActivated {
		set {
			playActivated = value;
		}
	}
	
	
}

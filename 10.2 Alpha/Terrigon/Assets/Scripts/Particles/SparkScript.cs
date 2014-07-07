using UnityEngine;
using System.Collections;

public class SparkScript : MonoBehaviour {

	private ParticleRenderer particle;
	
	void Awake () {
		particle = gameObject.transform.FindChild("Sparks").GetComponent<ParticleRenderer>();
	}
	
	void Start () {
		particle.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void BreakDown() {
		particle.enabled = true;
	}
}

using UnityEngine;
using System.Collections;

public class Scare : MonoBehaviour {

// Private variables
	private GameObject target;
	private SanityManager sanityScript;

// Declare and assign public variables
	public float sanityDistance = 20;

	void Start () {
// Assign variables through code
		target = GameObject.Find("Character");
		sanityScript = target.GetComponent<SanityManager>();
	}
	
	void Update () {
// To see if the enemy and player are on the same floor
		float verticalDistance = Mathf.Abs(target.transform.position.y - transform.position.y);
// Distance as the crow flies between the enemy and the player		
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
// Do sanity when enemy is on the same floor as the player, within range of the player and not fleeing
		if (verticalDistance < 2 && distance < sanityDistance && !Emitter.IsActivated){
			sanityScript.CurrentSanity = distance * 5;	
		}else{
			sanityScript.CurrentSanity = 100;
		}
	}
}

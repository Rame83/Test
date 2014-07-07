using UnityEngine;
using System.Collections;

public class CloseDoorTrigger : MonoBehaviour {
	public Door door;
	
	//public GameObject character;
	// Use this for initialization
	void Start () {
		//character = GameObject.Find("Character");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider ob) {
		if (ob.name == "Character") {
			door.OnPlayerCloseDoorInside();
		}
	}
}

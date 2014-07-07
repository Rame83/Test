using UnityEngine;
using System.Collections;

public class Raycasting : MonoBehaviour {

	public int damage = 100;
	public float meleeRange = 5.0f;
	private float range = 5.0f;
	
	public GameObject emitter;
	
	public Door script1;
	public GameObject DoorCollider1;
	public Door script2;
	public GameObject DoorCollider2;
	
	private string openDoor;
	private string doorMalfunction;
	public GUIStyle style;
	private bool showGui;
	private bool showGUI2;	
	
	void Start () {
		openDoor = "Press E to open door";
		doorMalfunction = "Door is locked";
		showGui = false;
		showGUI2 = false;
	}
	
	void Update () {
		// Raycast for interaction with InteractiveObjects(machines & emitters)
		InteractRay();
	
		if (Input.GetButton("Fire2")) {
			// Raycast for interaction with InteractiveObjects(machines & emitters)
			ActivateRay();
		}
		
		if (Input.GetButton("Fire1")) {
			// Raycast for attacking the enemies
			DamageRay();
		}
	}
	
	void ActivateRay() {
	
		// Set origin of Raycast to centre of the screen
		Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		// Place to store all the Raycast information
		RaycastHit hitInfo;	
	
		// Do Raycast(from, store info, range)
		if (Physics.Raycast(rayOrigin, out hitInfo, range)) {
		
			// If Raycast hits object with a specific 'tag'
			if (hitInfo.collider.tag == "Machine" || hitInfo.collider.tag == "EmitterStation"){
			
				// Store info in variables
				GameObject collisionObject = hitInfo.collider.gameObject;
				// When Raycasted object is a machine, get access to the machine script
				InteractiveObject item = collisionObject.GetComponent<InteractiveObject> ();
				
				if (item != null) {
					// Execute Interaction function
					item.Interaction();
				}
			}
		}
	}
	
	void DamageRay() {	
		//play aniamtion
		emitter.animation.Play("Swing");
		
		// Set origin of Raycast to centre of the screen
		Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		// Place to store all the Raycast information
		RaycastHit hitInfo;	
		
		
		// Do Raycast(from, store info, meleeRange)
		if (Physics.Raycast(rayOrigin, out hitInfo, meleeRange)) {
		
			// If Raycast hits object with a specific 'tag'
			if (hitInfo.collider.tag == "Enemy") {
			
				// Store info in variables
				GameObject collisionObject = hitInfo.collider.gameObject;
				// When Raycasted object is a machine, get access to the machine script
				StateMachine enemy = collisionObject.GetComponent<StateMachine> ();
			
				if (enemy != null) {
					// If Raycasted object is fleeing
					if (Emitter.IsActivated) {
						// Send a message to enemy to execute ApplyDamage	
						hitInfo.transform.SendMessage("ApplyDamage", damage);
					}
				}
			}	
		}
	}
	
	void InteractRay() {
		
		// Set origin of Raycast to centre of the screen
		Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		// Place to store all the Raycast information
		RaycastHit hitInfo;	
		
		// Do Raycast(from, store info, range)
		if (Physics.Raycast(rayOrigin, out hitInfo, range)) {
			if(hitInfo.collider.gameObject.CompareTag("Door1")){
				showGui = true;
				if (Input.GetButton("Fire2")) {
					script1.OpenDoor();
				}
			}
			else if(hitInfo.collider.gameObject.CompareTag("Door2")){
				showGui = true;
				if (Input.GetButton("Fire2")) {
					script2.OpenDoor();
				}
			}
			else if (hitInfo.collider.gameObject.CompareTag("EndDoor1")){
				showGUI2 = true;
				Debug.Log("FICK");
			}
			else if (hitInfo.collider.gameObject.CompareTag("EndDoor2")){
				showGUI2 = true;
			}
			else{
				showGui = false;
				showGUI2 = false;
			}
		}else{
			showGui = false;
			showGUI2 = false;
		}
	}
	
	void OnGUI(){
		if(showGui) {
			//	Only show when player is still in the bridge
			if (!TriggerLevel.LevelStarted){
				GUI.Label (new Rect(Screen.width/2 - openDoor.Length/2, Screen.height - (Screen.height/3), openDoor.Length, 20), openDoor, style);
			}
			//	Only show when player is in the level
			if (TriggerLevel.LevelStarted) {
				GUI.Label (new Rect(Screen.width/2 - openDoor.Length/2, Screen.height - (Screen.height/3), openDoor.Length, 20), doorMalfunction, style);
			}	
			
		}
		
		if (showGUI2) {
			//	Only show when player hasn't activated all the machines
			if (!MachineCount.Win){
				GUI.Label (new Rect(Screen.width/2 - openDoor.Length/2, Screen.height - (Screen.height/3), openDoor.Length, 20), doorMalfunction, style);
			}
		}
	}
}


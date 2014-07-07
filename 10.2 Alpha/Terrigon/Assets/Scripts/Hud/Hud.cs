using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hud : MonoBehaviour {

	public GUIStyle styleHUD;
	public GUIStyle styleScreen;
	public GUIStyle styleWin;
	
	Machine machine;
	Machine machines;
	
	string allMachinesText; 
	
	GameObject stamina;
	CharacterMovement staminaBar;
	float staminaFloat;
	int staminaPercentage;
    int showPressKey;
	
	string getText;
	
	string machineTextHUD;
	string machineTextActivatedHUD;
	string machineActivate;
	string machineActivated;
	string emitterActivate;
	string emitterActivated;
	string staminaText;
	string staminaTextHUD;
    bool enterPressed = false;
    bool wonGame;
	
	private float range = 5.0f;
	
	private PlayerTraits playerTraits;
	
	void Awake() {
        showPressKey = 0;
	}
	
	void Start () {
		// Get connections with MachineScript and CharacterMovementScript
		machine  = GameObject.Find("Machine").GetComponent<Machine>();
//		machines = machine.GetComponent<Machine>();
		stamina = GameObject.Find("Character");
		staminaBar = stamina.GetComponent<CharacterMovement>();
		playerTraits = GameObject.Find("Character").GetComponent<PlayerTraits>();
		
		// All possible strings (but 2)
		machineTextActivatedHUD = "All Machines activated, you can escape";
		machineActivate = "Press E to activate machine";
		machineActivated = "Machine is already activated";
        
		emitterActivate = "Press E to charge emitter";
		emitterActivated = "Charger has already been used";
		
		// Push all machines in List
		
        allMachinesText = MachineCount.AllMachines.ToString();
	}
	
	void Update () {
		wonGame = TriggerLevel.WonGame;
       
		getText = machine.Machines.ToString();
		
		staminaFloat = staminaBar.stamina/20*100;
		staminaPercentage = (int)staminaFloat;
		staminaText = staminaPercentage.ToString();
		
		// last 2 string that needs to be updated every frame
		machineTextHUD = "Machines activated: " + getText + " of " + allMachinesText;
		staminaTextHUD = "Stamina: " + staminaText + " %";

        if (machine.Machines == MachineCount.AllMachines) { if (Input.GetKeyDown(KeyCode.F)) enterPressed = true;}
	}
	
	void OnGUI() {
		//When reaching the end of the level
		if (wonGame) {
			string youWonTheGame = "Congratulations, you have escaped";
			GUI.Label(new Rect(Screen.width / 2 - youWonTheGame.Length / 2, Screen.height - (Screen.height / 2), youWonTheGame.Length, 20), youWonTheGame, styleWin);
			
		}
		// StaminaBar in topRight
		GUI.Label(new Rect(Screen.width - 150, 0, 27, 20), staminaTextHUD, styleHUD);
	
		// When NOT all machines are activated
		if(machine.Machines < MachineCount.AllMachines) {
			GUI.Label(new Rect(200, 0, 27, 20), machineTextHUD, styleHUD);
		}
		
		if (!playerTraits.Alive) {	
			// When player is dead, show text
			string diedText =  "YOU DIED!";
			Rect rect = new Rect(Screen.width/2, Screen.height/2, diedText.Length, 20);
			GUI.Label (rect, diedText, styleScreen);
		}
		
		// When all machines are activated
		if(machine.Machines == MachineCount.AllMachines) {
			GUI.Label(new Rect(300, 0, machineTextActivatedHUD.Length, 20), machineTextActivatedHUD, styleHUD);
            string pressKey = "Press F to show the to the end";
            showPressKey++;
            
            
            if (showPressKey < 300 && !enterPressed) {
            	GUI.Label(new Rect(Screen.width / 2 - pressKey.Length / 2, Screen.height - (Screen.height / 2), pressKey.Length, 20), pressKey, styleHUD);
            }
		}
		
		// Set origin of Raycast at the centre of the screen
		Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		// Place to store all the Raycast information
		RaycastHit hitInfo;
		// Do Raycast(from, store info, range)
		if (Physics.Raycast(rayOrigin, out hitInfo, range)) {
		
			// If Raycast hits object with a specific 'tag'	
			if (hitInfo.collider.tag == "Machine"){
				
				// Store info in variables
				GameObject collisionObject = hitInfo.collider.gameObject;
				// When Raycasted object is a machine, get access to the machine script
				Machine item = collisionObject.GetComponent<Machine> ();
				
				if (item != null) {
					// If Raycasted object isn't active
					if (!item.GetActivated()) {
						GUI.Label(new Rect(Screen.width/2 - machineActivate.Length/2, Screen.height - (Screen.height/3), machineActivate.Length, 20), machineActivate, styleHUD);
					}
					// If Raycasted object is activated
					if (item.GetActivated()) {
						GUI.Label(new Rect(Screen.width/2 - machineActivated.Length/2, Screen.height - (Screen.height/3), machineActivated.Length, 20), machineActivated, styleHUD);
					}
				}
			}
			
			if (hitInfo.collider.tag == "EmitterStation"){
			
				// Store info in variables
				GameObject collisionObject = hitInfo.collider.gameObject;
				// When Raycasted object is a emitter, get access to the machine script
				Emitter item2 = collisionObject.GetComponent<Emitter> ();
				if (item2 != null) {
				
					// If Raycasted object isn't active
					if (!item2.GetActivated()) {
						GUI.Label(new Rect(Screen.width/2 - emitterActivate.Length/2, Screen.height - (Screen.height/3), emitterActivate.Length, 20), emitterActivate, styleHUD);
					}
					// If Raycasted object is activated
					if (item2.GetActivated()) {
						GUI.Label(new Rect(Screen.width/2 - emitterActivated.Length/2, Screen.height - (Screen.height/3), emitterActivated.Length, 20), emitterActivated, styleHUD);
					}
				}
			}
		}
	}
}

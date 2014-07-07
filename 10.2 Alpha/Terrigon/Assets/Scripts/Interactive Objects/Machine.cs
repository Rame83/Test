using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Machine : InteractiveObject {

	private static int machines;
	
	private bool machineActivated;
	
	public AudioClip sound;
	
	// Used for spawning enemies
	private GameObject enemies;
	private EnemySpawn script;
	private static int enemyCounter;
	
	
	// For creating a icon, shown on the miniMap
	public GameObject iconPrefab;
	private GameObject iconInstance;
	
	// Animate machines
	public GameObject animateMachine;
	public Color color;
	private Transform machineLight;
	private Transform machineHandle;
	
	
	void Awake(){
		machines = 0;
		machineActivated = false;
		enemies = GameObject.Find("Enemies");
		script = enemies.GetComponent<EnemySpawn>();
		enemyCounter = -1;
		
		// Instantiate machineIcon, as child of machine, for showing on miniMap;
		iconInstance = (GameObject)Instantiate(iconPrefab);
		iconInstance.transform.parent = gameObject.transform;
		iconInstance.transform.localPosition = new Vector3(0.0f, 51f, 0.0f);
		iconInstance.transform.localScale = new Vector3(3,0.1f,3);
		
		machineLight = gameObject.transform.FindChild("machine handle rev6").FindChild("Light");
		machineHandle = gameObject.transform.FindChild("machine handle rev6").FindChild("Handle");
	}
	
	override public void Update () {
        if (Input.GetKeyDown(KeyCode.O)) machines = 12;
	}
	
	
// Override interactionFunction of InteractiveObject
	override public void Interaction () {
		if (!machineActivated) {
			animateMachine.animation.Play("Activate");
			machineActivated = true;
			machineLight.renderer.material.color = color;
			machineHandle.renderer.material.color = color;
			gameObject.GetComponent<Light>().color = color;
			machines++;
			enemyCounter++;
			PlaySound();
			script.SpawnEnemies(enemyCounter);
			
			// Destroy the icon, so remove activated machine from the miniMap
			Destroy(iconInstance);
		}
        
	}
	
// Returns a bool, so another script(HUD.cs) knows if a machines is activated	
	public bool GetActivated() {
		return machineActivated;
	}
	
	void PlaySound() {
		audio.PlayOneShot(sound);
	}
	
// Returns a int, so another script(HUD.cs) knows how many machines are activated	

    public int Machines {
        get { return machines; }
    }
	
	
}

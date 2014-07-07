using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Emitter : InteractiveObject {

// For creating a icon, shown on the miniMap
	public GameObject iconPrefab;
	private GameObject iconInstance;
	
// List of all enemies
	private static List <GameObject> enemies;
	
// Bool for each emitter
	private bool emitterActivated;
// Global bool 
	private static bool isActivated;

// Stores the activation sound	
	public AudioClip activationSound;
	
// Get access to the timer
	private EmitterTimer timerScript;
	
	private Shader shader;
	private Transform emitterLight;
	public EmitterAnimation emitterAnimationScript;
	
	
	void Awake () {
// Instantiate icon and set as child of emitter
		iconInstance = (GameObject)Instantiate(iconPrefab);
		iconInstance.transform.parent = gameObject.transform;
		iconInstance.transform.localPosition = new Vector3(0.0f, 51.5f, 0.0f);
	
		enemies = new List<GameObject>();
		
		emitterActivated = false;
		isActivated = false;
		
		timerScript = GameObject.Find("Machines").GetComponent<EmitterTimer>();
		
		shader = Shader.Find("Diffuse");
		emitterLight = gameObject.transform.FindChild("Charging dock rev7");
	}
	

	
	override public void Update () {
	}
	
// Override interactionFunction of InteractiveObject
	override public void Interaction () {
		if (!emitterActivated) {
			emitterAnimationScript.PlayActivated = true;
			emitterActivated = true;
			isActivated = true;
			emitterLight.renderer.material.shader = shader;
			gameObject.GetComponent<Light>().range = 0;
			PlaySound();
// Make sure timer is reset to emitterTime (so also resets when emitter is activated during an activated machine)
			timerScript.ResetTimer();
			
// Destroy the icon, so remove activated emitter from the miniMap
			Destroy(iconInstance);
			ChangeState();
		}
	}

// Returns bool to another script(HUD.cs), so it knows when an emitter is activated	
	public bool GetActivated () {
		return emitterActivated;
	}
	
	void ChangeState() {
// For each enemy in the List enemies get the StateMachine script and execute the StateFlee function
		foreach (GameObject enemy in enemies) {
			StateMachine script = enemy.GetComponent<StateMachine>();
			script.StateFlee();
		}
	}
	
	void PlaySound(){
		audio.PlayOneShot(activationSound);
	}
	
//	Add enemies to the List by other script(EnemySpawnScript.cs)
	public static void AddToEnemies(GameObject obj) {
		enemies.Add(obj);
	}
	
// Getter so other scripts(OnCollisionScript.cs, RaycastingScript.cs, ScareScript.cs, SoundScript.cs, EmitterTimer	) see if an emitter is activated
// Setter so other script (EmitterTimer.cs) can set the bool if necessary
	public static bool IsActivated {
		get {
			return isActivated;
		}
		set {
			isActivated = value;
		}
	}
	
}

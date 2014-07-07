using UnityEngine;
using System.Collections;

public class TriggerLevel : MonoBehaviour {
	
	// Private variables
	private GameObject gameTriggerFF;
	private GameObject gameTriggerSF;
	
	
	// Public variables	
	public Door door;
	public EndDoor endDoor;
	
	// static variables
	private static bool levelStarted;
	private static bool wonGame;
	
	void Start () {
		gameTriggerFF = GameObject.Find("GameStartTrigger1");
		gameTriggerSF = GameObject.Find("GameStartTrigger2");
		levelStarted = false;
		wonGame = false;
	}
	
	void OnTriggerEnter(Collider ob) {
		GameStartTrigger(ob);
		GameEndTrigger(ob);
		
	}
	private void GameStartTrigger(Collider ob) {
		if (gameObject.name == "GameStartTrigger1" || gameObject.name == "GameStartTrigger2") {
			if (ob.name == "Character") {
				Destroy(gameTriggerFF);
				Destroy(gameTriggerSF);
				door.OnPlayerCloseDoorOutside();
				levelStarted = true;
				SparkScript script1 = GameObject.Find("Spark1stFloor1").GetComponent<SparkScript>();
				SparkScript script2 = GameObject.Find("Spark1stFloor2").GetComponent<SparkScript>();
				SparkScript script3 = GameObject.Find("Spark2ndFloor1").GetComponent<SparkScript>();
				SparkScript script4 = GameObject.Find("Spark2ndFloor2").GetComponent<SparkScript>();
				script1.BreakDown();
				script2.BreakDown();
				script3.BreakDown();
				script4.BreakDown();
			}
		}  
	}
	private void GameEndTrigger(Collider ob) {
		if ((gameObject.name == "EndNode1" || gameObject.name == "EndNode2") && MachineCount.Win) {
			if(ob.name == "Character"){
				wonGame = true;
				endDoor.OpenPlayerDoor();
				Emitter.IsActivated = true;
			}
		}
	}
	
	//	Getter so other scripts know when the game is finished and needs to show endscreen
	public static bool WonGame{
		get {
			return wonGame; 
		}
		
	}
	// Getter so other script(PlayerDoorCollide) knows when the level is started     
	public static bool LevelStarted {
		get {
			return levelStarted;
		}
	}
}

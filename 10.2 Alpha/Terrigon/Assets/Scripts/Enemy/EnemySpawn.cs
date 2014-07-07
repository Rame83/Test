using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	
// Private variables
	private GameObject enemy1AInstance;
	private GameObject enemy1BInstance;
	private GameObject enemy1CInstance;
	private GameObject enemy2AInstance;
	private GameObject enemy2BInstance;
	private GameObject enemy2CInstance;
	
	private Vector3 spawnPoint;
	
	private Transform groundTwo;

// Public variables
	public GameObject enemy1APrefab;
	public GameObject enemy1BPrefab;
	public GameObject enemy1CPrefab;
	public GameObject enemy2APrefab;
	public GameObject enemy2BPrefab;
	public GameObject enemy2CPrefab;
	
	
	public Transform respawnFFOne;
	public Transform respawnFFTwo;
	public Transform respawnSFOne;
	public Transform respawnSFTwo;
	
	public Transform character;
	
	public AudioClip firstEnemySpawn;
	
	
	

	void Start() {
		// Assign variables through code
		groundTwo = GameObject.Find("Ground2").transform;
	}
	
// Public function so other scripts(TriggerLevelScript.cs, Machine.cs) can use SpawnEnemies
	public void SpawnEnemies(int counter) {
		if (character.position.y > groundTwo.position.y) {
			if (character.transform.position.z > -6) {
				spawnPoint = respawnSFOne.position;
			}
			else if (character.transform.position.z <= -6) {
				spawnPoint = respawnSFTwo.position;
			}
			
		}
		else {
			if (character.transform.position.z > -6) {
				spawnPoint = respawnFFOne.position;
			}
			else if (character.transform.position.z <= -6) {
				spawnPoint = respawnFFTwo.position;
			}
			
		}
        
		if (counter == 0) {
            enemy1AInstance = (GameObject)Instantiate(enemy1APrefab, spawnPoint, new Quaternion(0, 0, 0, 0));
			enemy1AInstance.transform.parent = gameObject.transform;
			Emitter.AddToEnemies(enemy1AInstance);
			PauseMenu.AddToEnemies(enemy1AInstance);
			PlaySFX(firstEnemySpawn);
		}
		
		if (counter == 1){
            enemy1BInstance = (GameObject)Instantiate(enemy1BPrefab, spawnPoint, new Quaternion(0, 0, 0, 0));
			enemy1BInstance.transform.parent = gameObject.transform; 
			Emitter.AddToEnemies(enemy1BInstance);
			PauseMenu.AddToEnemies(enemy1BInstance);
		}
		
		if (counter == 2){
            enemy1CInstance = (GameObject)Instantiate(enemy1CPrefab, spawnPoint, new Quaternion(0, 0, 0, 0));
			enemy1CInstance.transform.parent = gameObject.transform;
			Emitter.AddToEnemies(enemy1CInstance);
			PauseMenu.AddToEnemies(enemy1CInstance);
		}
		
		if (counter == 3) {
            enemy2AInstance = (GameObject)Instantiate(enemy2APrefab, spawnPoint, new Quaternion(0, 0, 0, 0));
			enemy2AInstance.transform.parent = gameObject.transform;
			Emitter.AddToEnemies(enemy2AInstance);
			PauseMenu.AddToEnemies(enemy2AInstance);
		}
		
		if (counter == 4){
            enemy2BInstance = (GameObject)Instantiate(enemy2BPrefab, spawnPoint, new Quaternion(0, 0, 0, 0));
			enemy2BInstance.transform.parent = gameObject.transform;
			Emitter.AddToEnemies(enemy2BInstance);
			PauseMenu.AddToEnemies(enemy2BInstance);
		}
		
		if (counter == 5){
            enemy2CInstance = (GameObject)Instantiate(enemy2CPrefab, spawnPoint, new Quaternion(0, 0, 0, 0));
			enemy2CInstance.transform.parent = gameObject.transform;
			Emitter.AddToEnemies(enemy2CInstance);
			PauseMenu.AddToEnemies(enemy2CInstance);
		}
		
		if (counter > 5) {
			return;
		}
	}
	
	private void PlaySFX(AudioClip clip){
		audio.PlayOneShot(clip);
	}
	
}
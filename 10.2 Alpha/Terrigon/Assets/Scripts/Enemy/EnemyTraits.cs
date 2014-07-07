using UnityEngine;
using System.Collections;

public class EnemyTraits : MonoBehaviour {
	
	private Vector3 respawnPositionFFOne;
	private Vector3 respawnPositionSFOne;
    private Vector3 respawnPositionFFTwo;
    private Vector3 respawnPositionSFTwo;
	private Vector3 spawnPoint;
	
	private Transform character;
	private Transform graphic;
	
	private GameObject deathSmokeInstance;
	private GameObject deathSmoke;

    private GameObject enemyIcon;
	private GameObject enemyIconInstance;
	private bool playDeathSmoke;
    
	private PlayerTraits scriptPlayer;
    
	int health = 100;
	
	void Start () {
		scriptPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTraits>();
        playDeathSmoke = true;
        character = GameObject.Find("Character").transform;
        respawnPositionFFOne = GameObject.Find("RespawnPointFirstFloor1").transform.position;
        respawnPositionSFOne = GameObject.Find("RespawnPointSecondFloor1").transform.position;
        respawnPositionFFTwo = GameObject.Find("RespawnPointFirstFloor2").transform.position;
        respawnPositionSFTwo = GameObject.Find("RespawnPointFirstFloor2").transform.position;
        enemyIcon = Resources.Load("Prefabs/Enemies/EnemyIcon") as GameObject;
        enemyIconInstance = GameObject.Instantiate(enemyIcon) as GameObject;
		graphic = transform.FindChild("CreatureExport01");
	}	
	
	// When health falls below zero, enemy should respawn	
	void Update () {
        
        if (enemyIconInstance.renderer.material.color.a == 1.0f) {
            if (this.transform.position.y < 7.0f) enemyIconInstance.layer = 13;
            if (this.transform.position.y > 7.0f) enemyIconInstance.layer = 14;
            enemyIconInstance.transform.position = new Vector3(transform.position.x,transform.position.y + 52, transform.position.z);
        }
		if (health <= 0) {
            
            StartCoroutine(DeathAnimation());
			
		}
		
	}
	
	// Executed when an enemy hits the player	
	void OnTriggerEnter(Collider col){
		if(col.CompareTag("Player") && !Emitter.IsActivated){
			scriptPlayer.Alive = false;
		}
	}
	
	// Substract damage from health
	void ApplyDamage(int damage){
		health -= damage;
	}
	
	// Reset health, move enemy to spawnPoint
	void Respawn() {
        if (character.position.y > 4.65f && character.position.z > 0) {
            spawnPoint = respawnPositionSFOne;
        }
        if (character.position.y > 4.65f && character.position.z <= 0) {
			spawnPoint = respawnPositionSFTwo;
		}
        if (character.position.y < 4.65f && character.position.z > 0) {
            spawnPoint = respawnPositionFFOne;
        }
        if (character.position.y < 4.65f && character.position.z <= 0) {
            spawnPoint = respawnPositionFFTwo;
        }

		this.transform.position = spawnPoint;

		if (this.transform.position == spawnPoint) {
            graphic.transform.localScale = new Vector3(1, 0.5F, 1);
            playDeathSmoke = true;
			health = 100;
		}
		
	}
	
	private void DeathSmoke() {
		deathSmoke = Resources.Load("Prefabs/Enemies/whirlwindBase") as GameObject;
		Vector3 deathSmokePosition = this.transform.position;
		deathSmokeInstance = (GameObject)Instantiate(deathSmoke, deathSmokePosition, Quaternion.identity);
	}

    private IEnumerator DeathAnimation() {
        yield return new WaitForSeconds(0.4f);
        FadeAway();
        if (playDeathSmoke) {
            DeathSmoke();
            playDeathSmoke = false;
        }
       
        yield return new WaitForSeconds(1.5f);
        FadeAway();
        Respawn();

    }
    
    private void FadeAway() {
        //transform.Rotate(new Vector3(0, 40, 0), 30);
        graphic.transform.localScale = new Vector3(graphic.transform.localScale.x * 0.96f, graphic.transform.localScale.y * 1.05f, graphic.transform.localScale.z * 0.96f);
        gameObject.GetComponent<NavMeshAgent>().speed = 0;
    }
}

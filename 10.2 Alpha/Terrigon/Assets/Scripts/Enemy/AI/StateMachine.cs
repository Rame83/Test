using UnityEngine;
using System.Collections;


public class StateMachine : MonoBehaviour {	
//	All the states
	private State PATROL;
	private State CHASE;
	private State ATTACK;
	private State FLEE;
	private State currentState;
	private State lastState;
	
// Private variables	
	private GameObject target;
	private NodeLink startNode;
	private float chaseTimer;
	private float patrolTimer;
	
// Public variables	
	public string startNodeName;
	public float chaseTime;
	public float patrolTime;
	public float attackDistance;
	
	public enum States {
		PATROL,
		CHASE,
		ATTACK,
		FLEE
	}
	
	public States startState;
	
	void Awake () {
		PATROL = new Patrol();
		CHASE = new Chase();
		ATTACK = new Attack();
		FLEE = new Flee();
		
		target = GameObject.FindGameObjectWithTag("Player");
		startNode = GameObject.Find(startNodeName).GetComponent<NodeLink>();
	}
	void Start () {
//	Switch statement (only set if emitter isn't active)
		switch(startState){
			case States.CHASE:
				if (Emitter.IsActivated) {
					currentState = FLEE;
				}else{
					currentState = CHASE;
					chaseTimer = chaseTime;
				}
				break;
			case States.PATROL:
				if (Emitter.IsActivated) {
					currentState = FLEE;
				}else{	
					currentState = PATROL;
					patrolTimer = patrolTime;
				}
				break;
			case States.ATTACK:
				currentState = ATTACK;
				break;
			case States.FLEE:
				currentState = FLEE;
				break;
		}
	}
	
	void Update () {
		StateChange();	
		ExcecuteStep();
	}
	
	void ExcecuteStep() {
// When the enemies have a state, excute the step function of that state
		if (currentState != null) {
			currentState.Step (gameObject, target);
			currentState.Step (gameObject, startNode);
			
		}
	}
	
	void StateChange() {
// Only update lastState when enemy is chasing or patrolling
		if (currentState != ATTACK && currentState != FLEE){
			lastState = currentState;
		}
		
		fleeStateManagement();

		float distance = Vector3.Distance(target.transform.position, transform.position);
		float verticalDistance = Mathf.Abs(target.transform.position.y - transform.position.y);
// When enemy is on the samefloor as the player and within attackDistance, the enemy will attack unless the currentState is fleeing
		if (verticalDistance < 2 && distance < attackDistance) {
			if (currentState != FLEE) {
				currentState = ATTACK;
			}
		}	
// When the enemy is on a different floor as the player or out of attackDistance
		if (verticalDistance > 2 || (verticalDistance < 2 && distance > attackDistance)) {
			chaseStateManagement();
			patrolStateManagement();
			attackStateManagement();
		}
	}
	
	void fleeStateManagement() {
// What happens if enemy is fleeing
		if (currentState == FLEE) {
// What happens if emitter is off again and there is no lastState(happens when enemy spawns during emitterActivation)
			if (!Emitter.IsActivated){
				if (lastState == null){
					currentState = CHASE;
					chaseTimer = chaseTime;
				}else{
					currentState = lastState;
				}
			}	
		}
	}
	
	void chaseStateManagement() {
// When currenState is CHASE, it should countDown to zero then the enemy should PATROL
		if (currentState == CHASE) {
			chaseTimer -= Time.deltaTime;
			if (chaseTimer <= 0.0f) {
				patrolTimer = patrolTime;
				currentState = PATROL;
			}
		}
	}
	
	void patrolStateManagement() {
// When currentState is PATROL, it should countDown to zero then the enemy should CHASE
		if (currentState == PATROL) {
			patrolTimer -= Time.deltaTime;
			if (patrolTimer <= 0.0f) {
				chaseTimer = chaseTime;
				currentState = CHASE;
			}
		}
	}
	
	void attackStateManagement() {
// When enemy was attacking and goes out of range, the currentState should go back to the lastState
		if (currentState == ATTACK) {
			currentState = lastState;
		}
	}
	
// Function to be called by anotherscript to set the currentState of the enemy to FLEE
	public void StateFlee () {
		currentState = FLEE;
	}
}

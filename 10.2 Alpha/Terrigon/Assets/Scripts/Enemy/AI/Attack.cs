using UnityEngine;
using System.Collections;

public class Attack : State {

	private float runSpeed = 9.0f;
	private Vector3 point;

	public Attack () {
		
	}
	
	// Override stepFunction of State Class
	override public void Step(GameObject source, GameObject target) {
		NavMeshAgentSettings(source, target);
	}
	
	void NavMeshAgentSettings(GameObject source, GameObject target) {
	
		// Let the enemy alsways look at the player, when he is Attacking
		point = source.GetComponent<NavMeshAgent>().steeringTarget;
		point.y = source.transform.position.y;
		
		source.transform.LookAt(point);		
		
		// Set the destination and travelspeed of the enemy
		source.GetComponent<NavMeshAgent>().destination = target.transform.position;
		source.GetComponent<NavMeshAgent>().speed = runSpeed;
	}
}

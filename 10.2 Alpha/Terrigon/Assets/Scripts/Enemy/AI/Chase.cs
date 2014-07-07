using UnityEngine;
using System.Collections;

public class Chase : State {

	private float chaseSpeed = 8.0f;
	private Vector3 point;


	public Chase () {
		
	}
	
	// Override stepFunction of State Class
	override public void Step(GameObject source, GameObject target) {
		NavMeshAgentSettings(source, target);
		
	}
	
	void NavMeshAgentSettings(GameObject source, GameObject target) {
		
		// Set the destination and the travelspeed of the enemy
		source.GetComponent<NavMeshAgent>().destination = target.transform.position;
		source.GetComponent<NavMeshAgent>().speed = chaseSpeed;
		
		// Let the enemy alsways look at the player, when he is Chasing
		point = source.GetComponent<NavMeshAgent>().steeringTarget;
		point.y = source.transform.position.y;
		
		source.transform.LookAt(point);		
		
	}
}

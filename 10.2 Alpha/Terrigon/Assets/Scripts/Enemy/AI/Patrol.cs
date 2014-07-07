using UnityEngine;
using System.Collections;

public class Patrol : State {

	NodeLink targetNode;
	
	private float range = 0.5f;
	private float patrolSpeed = 8.0f;
	private Vector3 point;

	public Patrol () {
	
	}
	
	// Override stepFunction of State Class
	override public void Step (GameObject source, NodeLink startNode) {
		Travel(source, startNode);
	}
	
	void Travel(GameObject source, NodeLink startNode) {
	
// When enemy starts patrolling, make first node the targetNode
		if (targetNode == null) targetNode = startNode;
		
// If enemy is out of range travel to targetNode,
// If enemy is within range set a new targetNode from linkedNodes 
		float distance = Vector3.Distance(targetNode.transform.position, source.transform.position);
		if (distance > range) {
			SetNode(source, targetNode);
		}else{
			targetNode = ChooseNode(targetNode.Links);
		}
	}
	
	NodeLink ChooseNode(NodeLink[] options) {
	
// If there are no linkedNodes, then stop the function
		if (options.Length < 1) return null;
		
// Choose a random node from the linkedNodes
		int index = Mathf.FloorToInt(Random.value * options.Length);
		return options[index];
	}
	
	void SetNode(GameObject source, NodeLink currentNode) {
	
// Let the enemy look at the node he is traveling to
		point = source.GetComponent<NavMeshAgent>().steeringTarget;
		point.y = source.transform.position.y;
		
		source.transform.LookAt(point);		
	
// Set the destination and speed of the enemy	
		source.GetComponent<NavMeshAgent>().destination = currentNode.transform.position;
		source.GetComponent<NavMeshAgent>().speed = patrolSpeed;
	}
}

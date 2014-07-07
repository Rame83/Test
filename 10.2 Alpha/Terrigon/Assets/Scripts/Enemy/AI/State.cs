using UnityEngine;
using System.Collections;

public class State {	
	
	public State() {
	
	}
	
	virtual public void Step(GameObject source, GameObject target) {
		// To be overridden
	}
	
	virtual public void Step(GameObject source, NodeLink startNode) {
		// To be overridden
	}
}

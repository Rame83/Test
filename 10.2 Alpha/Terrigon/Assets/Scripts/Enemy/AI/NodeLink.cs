using UnityEngine;
using System.Collections;

public class NodeLink : MonoBehaviour {

// Public variables
	public NodeLink[] m_link;
	public Color m_color;

// Return the linkNodes, so other classes (Patrol, Flee) have access to the LinkedNodes 
	public NodeLink[] Links {
		get {
			return m_link;
		}
	}
	
	void OnDrawGizmos() {
// Draw all the links in the scene
		for (int i=0; i < m_link.Length; i++){
			Gizmos.color = new Color (m_color.r, m_color.g, m_color.b);
			Gizmos.DrawLine(gameObject.transform.position, m_link[i].transform.position);
		}
		
// Draw the nodes in the scene
		Gizmos.DrawSphere(transform.position, 0.5f);
	}
}

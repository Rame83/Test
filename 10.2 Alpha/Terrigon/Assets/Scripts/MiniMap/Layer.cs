using UnityEngine;
using System.Collections;

public class Layer : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
		// Set layer, When enemyIcon(for MiniMap) is on first floor
        if (!transform.parent) return;
		if (transform.parent.position.y < 10.0F) gameObject.layer = 13;
		// Set layer, When enemyIcon(for MiniMap) is on second floor
		if (transform.parent.position.y > 10.0F) gameObject.layer = 14;
	}
}

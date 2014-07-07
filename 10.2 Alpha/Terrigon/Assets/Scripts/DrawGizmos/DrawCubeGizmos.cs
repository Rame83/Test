using UnityEngine;
using System.Collections;

public class DrawCubeGizmos : MonoBehaviour {

    void OnDrawGizmos() {
        if (gameObject.tag == "MachinePosition") {
            if (gameObject.transform.childCount == 0) {
                Gizmos.color = new Color(255,0,0);
                Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
           }
        }
        if (gameObject.tag == "EmitterPosition") {
            if (gameObject.transform.childCount == 0) {
                Gizmos.color = new Color(0,255,255);
                Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
            }
        }
        if (gameObject.tag != "MachinePosition" && gameObject.tag != "EmitterPosition") {
            Debug.Log("This object does not have a tag MachinePosition or EmitterPosition");
            Debug.Log(this.gameObject);
        }
    }
}

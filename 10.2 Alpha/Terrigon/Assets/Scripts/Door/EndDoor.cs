using UnityEngine;
using System.Collections;

public class EndDoor : MonoBehaviour {

	public GameObject handle;
	
	void Update(){
	
//	Only show OPEN of endDoors when you can use the door() after all machines are activated
		if (gameObject.CompareTag("EndDoor1") || gameObject.CompareTag("EndDoor2")) {
			// bool from MachineCount.cs
			if (MachineCount.Win){
				handle.renderer.enabled = true;
			}else{
				handle.renderer.enabled = false;
			}
		}
	}

	public void OpenPlayerDoor() {
		animation.Play("Open Door");
	}
	
}	

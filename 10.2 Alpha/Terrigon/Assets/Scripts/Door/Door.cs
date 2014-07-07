using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	private bool openDoor = true;
	private bool closeDoor = false;
	public Door secondDoor;
	private BoxCollider parentCollider;
	
	public AudioClip doorOpenClip;
	public AudioClip doorCloseClip;
	private bool playSFX;
	
	void Awake() {
		parentCollider = transform.parent.GetComponent<BoxCollider>();
		parentCollider.enabled = true;
		playSFX = true;
		
	}
	
	void Update() {
	
		if (animation.isPlaying){
			parentCollider.enabled = true;
		}else{
			parentCollider.enabled = false;
		}
	}
	
	public void OnPlayerOpenDoor(){
		if(!closeDoor && openDoor && !secondDoor.closeDoor){
			animation.Play("Open Door");
			openDoor = false;
			closeDoor = true;
		}
	}
	
	public void OnPlayerCloseDoorInside(){
		if(closeDoor && !openDoor ){
			animation.Play("Close Door");
			audio.PlayOneShot(doorCloseClip, 0.2F);
			playSFX = true;
			closeDoor = false;
			openDoor = true;
		}
	}
	
	public void OnPlayerCloseDoorOutside(){
		if(closeDoor){
			secondDoor.closeDoor = true;
			animation.Play("Close Door");
			audio.PlayOneShot(doorCloseClip, 0.2F);
		}
	}
	
	public IEnumerator DelayOpenDoor() {
		yield return new WaitForSeconds(2);
		
		OnPlayerOpenDoor();
	}
	
	public void OpenDoor(){
		if (playSFX){
			audio.PlayOneShot(doorOpenClip, 0.2F);
			playSFX = false;
		}
		StartCoroutine(DelayOpenDoor());
	}
}

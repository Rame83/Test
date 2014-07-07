using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    public float walkSpeed = 6.0F;
    public float runSpeed = 8.0F;
    public float gravity = 20.0F;
    public float stamina = 20.0F;
    public float regen = 0.1F;
    public float drain = 0.05F;

    private float speed = 5.0F;

    private Vector3 moveDirection = Vector3.zero;
    
    public AudioClip breathingClip;
//    private bool playSFX;

    void Awake() {
        rigidbody.freezeRotation = true;
//        playSFX = false;
    }
    
    void Update() {
    	Debug.Log(stamina);
    	
    	if (stamina < 7.0F){
    		if (!audio.isPlaying){	
    			PlaySFX();
    		}
    	}else{
    		audio.Stop();
    	}
    
    
        //Make RaycastHit
        RaycastHit hitInfo;
        //Debug.DrawRay draws a line down form the point(vector3) Just to debug.
        Debug.DrawRay(transform.position, -Vector3.up, Color.red, 2.0f);
        //Check if you are on the ground or not
        if (Physics.SphereCast(transform.position + Vector3.up * 0.5f, 0.5f, -Vector3.up, out hitInfo, 1.0f, ~(1 << 23))) {
            //vector3 Input horizontal are the buttons A and D and vertical is W and S
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //TransformDirection to get the rotation and move in the way its rotated
            moveDirection = transform.TransformDirection(moveDirection);
            //Reset gravity
            moveDirection.y = 0;
            //Get input sprint(Shift) 
            if (Input.GetButton("Sprint")) {
                if (stamina > 0) {
                    //Only change the speed to runspeed if you are moving forward(W)
                    if (Input.GetAxis("Vertical") > 0) {
                        stamina -= drain;
                        speed = runSpeed;
                    }
                    //If not input vertical > 0, nothing happens and speed = Walkspeed
                    else {
                        speed = walkSpeed;
                    }
                }
                //If the stamina is under or equal to 0, you can't run 
                else if (stamina <= 0) {
                    speed = walkSpeed;
                }

            }
            else {
                //If you are not pressing shift the speed is walkspeed
                speed = walkSpeed;
                //If stamina under 20 you are regenerating stamina
                if (stamina < 20) {
                    stamina += regen;
                }
            }
            //moveDirection is either a value between -1 and 1 so you multiply it by a float so you will be moving faster
            moveDirection *= speed;
        }
        //If you are not grounded, gravity should be applied
        else {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        //Apply all the movement to the rigidbody
        rigidbody.velocity = moveDirection;
    }
    
	private void PlaySFX(){
		audio.clip = breathingClip;
		audio.volume = 2.0F;
		audio.Play();
	}

    //Getter to get the speed in other scripts(firstpersoneffects and headbobber)
    public float GetSpeed() {
        return speed;
    }
}
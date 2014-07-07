using UnityEngine;
using System.Collections;

public class HeadBobber : MonoBehaviour {

    private CharacterMovement characterMovement;
    private GameObject m_firstPersonController;

    private float timer = 0.0f;
    float bobbingSpeed = 0.18f;
    
    float bobbingAmount = 0.1f;
    float midpoint = 1.2f;

    public float bobbingSpeedWalk;
    public float bobbingSpeedRun;

	public AudioClip footsteps;
    public AudioClip footstepRight;

    void Update() {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Get camera local position
        Vector3 m_camera = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) {
            timer = 0.0f;
        }
        //Change the timer count
        else {
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;
            if (timer > Mathf.PI * 2) {
                timer = timer - (Mathf.PI * 2);
                if (Random.value > 0.5f) {
                    PlaySound(1);
                }
                else {
                    PlaySound(2);
                }
            }
        }
        //If the waveslice not is 0, then change the camera height
        if (waveslice != 0) {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            m_camera.y = midpoint + translateChange;
            //if(!audio.isPlaying){
            //    PlaySound();
            //}
        }
        //if the waveslice is 0, the camera height is the camera midpoint
        else {
            m_camera.y = midpoint;
        }

        transform.localPosition = m_camera;
        //Change the bobbingSpeed according to character is running or walking
        if (characterMovement.GetSpeed() > characterMovement.walkSpeed) {
            bobbingSpeed = bobbingSpeedRun;
        }
        else {
            bobbingSpeed = bobbingSpeedWalk;
        }
    }

    void Start() {
        //Find gameObject and component of it
        m_firstPersonController = GameObject.Find("Character");
        characterMovement = m_firstPersonController.GetComponent<CharacterMovement>();
    }
    
    //Play audio footsteps
    void PlaySound(int audioclip){
        if (audioclip == 1) audio.PlayOneShot(footsteps);
        if (audioclip == 2) audio.PlayOneShot(footsteps);
    }

}
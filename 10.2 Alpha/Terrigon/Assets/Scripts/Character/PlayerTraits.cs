using UnityEngine;
using System.Collections;

public class PlayerTraits : MonoBehaviour {

	public GameObject m_pathToEnd;
	private GameObject m_instance;
	
	private bool created;
	private bool alive;
	
	public GameObject characterItems;
	private Quaternion from;
	private Quaternion to;
	
	private CharacterMovement characterMovement;
	private Camera m_camera;
	public float m_fovWalk = 60.0F;
	public float m_fovRun = 80.0F;

	void Start () {
		from = new Quaternion(50, 0, 0, 90);
		to = new Quaternion(0, 0, 0, 90);
	
		alive = true;
		created = false;
		
		//Get the Camera and CharacterMovement components in the gameObject this script is attached to.
		m_camera = Camera.main;
		characterMovement = GetComponent<CharacterMovement>();
		m_camera.fieldOfView = m_fovWalk;
	}
	
	void Update () {
	
	//	Show Navigation Path to end of the level
		if (Input.GetKeyDown(KeyCode.F) && MachineCount.Win) {
			if (created) {
				Destroy(m_instance);
				created = false;
			}
			if(!created) {
				CreateAgent();
				created = true;
			}
		}
		
	//	Rotate CharacterItem when Level starts	
		characterItems.transform.localRotation = Quaternion.Slerp(from, to, Time.time * 1.2f);
		
	//	If the speed of the character is the same as the character runspeed then increase the
	//	Field of view, else the field of view is the m_fovWalk
		if (characterMovement.GetSpeed() == characterMovement.runSpeed) {
			m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, m_fovRun, Time.deltaTime * 10);
		}
		else {
			m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, m_fovWalk, Time.deltaTime * 10);
		}
	}
	
//	Create Agent that shows you the way to the end	
	void CreateAgent() {
		m_instance = GameObject.Instantiate(m_pathToEnd, this.transform.position,new Quaternion(0,0,0,0))as GameObject;
	}
	
// Getter and setter for the bool alive, to be accessed by other scripts (OnCollisionScript.cs, SceneFadeInOut.cs)
	public bool Alive {
		get {
			return alive;
		}
		set {
			alive = value;
		}
	}
}

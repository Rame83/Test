using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	private GameObject target;
	public string targetName;
	public AudioClip[] audioClip;
	public AudioClip theme;
	public AudioClip win;

	private bool playRobotVoice;
	private float distance;
	private float screamDistance = 10.0f;
//	private Hud script;
	
	private bool secondSound = false;
	
	void Awake () {
		target = GameObject.Find(targetName);
		playRobotVoice = false;
//		script = GameObject.Find("Main Camera").GetComponent<Hud>();
	}
	
	void Start () {
	
	}
	
	void Update () {
			if (target.name == "Character" && !Emitter.IsActivated) {	
			distance = Vector3.Distance (transform.position, target.transform.position);

// Only play screams when player is on the same floor as the enemy and is within range
			if (Mathf.Abs(target.transform.position.y - transform.position.y) < 2) {
				if (distance < screamDistance && !audio.isPlaying) {
// Pick a random sound from the array
					PlaySound (Random.Range(0, audioClip.Length));
				}
			}
		}
		
		if (target.name == "Main Camera"){
			if (!audio.isPlaying)PlayTheme();
			if (!secondSound && MachineCount.Win){
				audio.Stop ();
				secondSound = true;
			}
			
		}
	}

	void PlaySound(int clip) {
		audio.volume = 0.2F;
		audio.PlayOneShot(audioClip[clip]);
	}
	
	void PlayTheme() {
		if (!MachineCount.Win){
			audio.clip = theme;
			audio.volume = 0.5F;
			audio.Play();
		}
		if (MachineCount.Win){
			if (!playRobotVoice) {
				audio.PlayOneShot(win);
				playRobotVoice = true;
			}
		}
		
	}
	
	
	public void PlaySFX(AudioClip audioSFX){
		audio.PlayOneShot(audioSFX);
	}
	
}

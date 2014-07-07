using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

	public float fadeSpeed = 1.5F;
	private bool sceneStarting = true;
	
	private bool playGameSound;
	private GameObject enemy;
	public PlayerTraits scriptPlayer;
	
	public AudioClip gameStartSound;
	
	void Awake() {
		playGameSound = false;
		guiTexture.pixelInset = new Rect(0F, 0F, Screen.width, Screen.height);
	}
	
    
	void Update() {
	
        if (sceneStarting){
        	StartScene();
        	if (!playGameSound){
        		audio.PlayOneShot(gameStartSound);
        		playGameSound = true;
        	}
        	
        }
        
        if (!scriptPlayer.Alive){
       		EndScene();
       	}
	}
	
	void FadeToClear() {
		guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	void FadeToBlack() {
		guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	
	void StartScene() {
		
		FadeToClear();
	
		if (guiTexture.color.a <= 0.05F) {
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			sceneStarting = false;
		}
	}
	
	public void EndScene() {
		guiTexture.enabled = true;
		FadeToBlack();
		
		if (guiTexture.color.a >= 0.95F){
			Application.LoadLevel(0);
		}
	}
}

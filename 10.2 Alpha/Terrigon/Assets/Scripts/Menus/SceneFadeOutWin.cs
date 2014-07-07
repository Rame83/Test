using UnityEngine;
using System.Collections;

public class SceneFadeOutWin : MonoBehaviour {

	private float fadeSpeed = 0.8F;

	void Awake() {
		guiTexture.pixelInset = new Rect(0F, 0F, Screen.width, Screen.height);
		guiTexture.color = Color.clear;
	}
	void Start() {
		guiTexture.enabled = false;
	}
	
	void Update() {
		
		if (TriggerLevel.WonGame){
			EndWinScene();
		}
	}

	void FadeToWhite() {
		guiTexture.color = Color.Lerp(guiTexture.color, Color.white, fadeSpeed * Time.deltaTime);
	}
	void EndWinScene() {
		guiTexture.enabled = true;
		FadeToWhite();
		
		if (guiTexture.color.a >= 0.95F){
			Application.LoadLevel(0);
		}
	}
}

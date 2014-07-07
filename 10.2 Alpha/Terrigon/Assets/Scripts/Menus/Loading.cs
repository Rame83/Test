using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	public GUIStyle style;
	

	// Use this for initialization
	void Start () {
		Application.LoadLevel("LevelScene01");
		guiTexture.pixelInset = new Rect(0F, 0F, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		GUI.Label(new Rect(Screen.width/2, Screen.height/2, 20, 20), "Loading...", style);
	}
}

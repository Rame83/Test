using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour {
	
	private bool paused = false;
	
	private float posX;
	private float posY;
	private float width;
	private float height;
	
	private int fontSize;
	
	public string[] m_gameObjects;
	public string[] m_components;
	private static List<GameObject> enemies;
	
	public GUIStyle anyButton;
	public GUIStyle specialButton;
	public GUIStyle anyText;
	public GUIStyle windowStyle;
	
	//window variable
	public Rect window0;
	// axis variables
	public static float xAxis;
	public static float yAxis;
	//boolean to display the window
	private bool displayWindow = false;
	
	private string xAxisString = "";
	private string yAxisString = "";
	private float prevXSensitivity = 0.0f;
	private float prevYSensitivity = 0.0f;
	
	void Start () {
		xAxis = MouseLook.sensitivityX;
		yAxis = MouseLook.sensitivityY;
		width = Screen.width / 2.5f;
		height = Screen.height / 4;
		posX = Screen.width / 2;
		posY = Screen.height / 2.5f;
		fontSize = Mathf.FloorToInt(Screen.width / 45.5f);
		Screen.showCursor = false;
		//set size for the options window 
		window0 = new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2);
		
		enemies = new List<GameObject>();
	}
	
	void Update () {
		// When "P" is pressed, execute GamePaused
		if(Input.GetKeyDown(KeyCode.Escape)){
			Time.timeScale = 0.0f;
			paused = true;
			Screen.showCursor = true;
			GamePause();
		}
	}
	void GamePause(){
		// When game is paused, disable all selected components
		if(paused){
			for (int i = 0; i < m_gameObjects.Length; i++) (GameObject.Find(m_gameObjects[i]).GetComponent(m_components[i])as Behaviour).enabled = false;
			foreach (GameObject obj in enemies) {
				(obj.GetComponent("StateMachine") as Behaviour).enabled = false;
				}
		}else{
			for (int i = 0; i < m_gameObjects.Length; i++) (GameObject.Find(m_gameObjects[i]).GetComponent(m_components[i])as Behaviour).enabled = true;
			foreach (GameObject obj in enemies) {
				(obj.GetComponent("StateMachine") as Behaviour).enabled = true;
			}
		}
	}
	void OnGUI(){
		if(paused){
			// Make the group of buttons
			//GUI.BeginGroup (new Rect (posX, posY, Screen.width, Screen.height));
			// Make the font size
			anyButton.fontSize = fontSize;
			// Allign to center
			anyButton.alignment = TextAnchor.MiddleCenter;
			if(GUI.Button(new Rect(posX - width / 2, posY - 3.5f*height /6, width, height / 1.5f),"Resume", anyButton) ){
				Time.timeScale = 1.0f;
				paused = false;
				Screen.showCursor = false;
				GamePause();
			}
			// Create button quite that will cancle the game
			anyButton.fontSize = Mathf.FloorToInt(fontSize / 1.8f);
			if(GUI.Button(new Rect(posX - width / 3, posY + 1*height / 6, width/1.5f, height / 2.5f), "Restart", anyButton)){
				Time.timeScale = 1.0f;
				paused = false;
				Screen.showCursor = false;
				GamePause();
				Application.LoadLevel(Application.loadedLevel);
			}
			if(GUI.Button(new Rect(posX - width / 3,  posY + 4*height / 6, width/1.5f, height / 2.5f), "Options", anyButton)){
				displayWindow = true;
			}
			if(GUI.Button(new Rect(posX - width / 3, posY + 7*height / 6, width/1.5f, height / 2.5f), "Quit", anyButton)){
				Application.Quit();
			}
			if(displayWindow){
				window0 = GUI.Window(0, window0, ChangeConfigurations, "Game Configurations", windowStyle);
			}
			//GUI.EndGroup ();
		}
	}
	void ChangeConfigurations(int windowID){
		//Setup font size
		anyText.fontSize = Mathf.FloorToInt(fontSize / 1.5f);
		
		//Display text of the options sequence
		GUI.Label (new Rect (window0.width/15, window0.height/8.5f, window0.width/3, 30), "Mouse sensitivity", anyText);
		
		//Setup smaller font size
		anyText.fontSize = Mathf.FloorToInt(fontSize / 2.5f);
		
		//Display text of the options
		GUI.Label (new Rect (window0.width/15, window0.height/3.5f, window0.width/4, 30), "X Axis", anyText);
		GUI.Label (new Rect (window0.width/15, window0.height/2.5f, window0.width/4, 30), "Y Axis", anyText);
		
		//Display and calculate the sensitivity acording to the slider
		xAxis = GUI.HorizontalSlider (new Rect (window0.width/2.5f, window0.height/3.5f, window0.width - window0.width/2.5f - 75, 30), xAxis, 0.0f, 100.0f);
		yAxis = GUI.HorizontalSlider (new Rect (window0.width/2.5f, window0.height/2.5f, window0.width - window0.width/2.5f - 75, 30), yAxis, 0.0f, 100.0f);
		
		//Check is slider changed sensitivity
		if(prevXSensitivity != xAxis || prevYSensitivity !=yAxis){
			prevXSensitivity = xAxis;
			prevYSensitivity = yAxis;
			xAxisString = xAxis.ToString();
			yAxisString = yAxis.ToString();
		}
		
		//Create adjustable by user textfield for the sensitivity 
		//xAxisString = xAxis.ToString();
		xAxisString = GUI.TextField(new Rect(window0.width - 50, window0.height/3.5f, 37 , 20), xAxisString, 3);
		yAxisString = GUI.TextField(new Rect(window0.width - 50, window0.height/2.5f, 37 , 20), yAxisString, 3);
		
		//check if the Xtextfields is not empty 
		if(xAxisString != "" ){
			float myLocalXFloat = float.Parse(xAxisString);
			
			//Check if textfield changed Xsensitivity
			if(prevXSensitivity != myLocalXFloat){
				xAxis = myLocalXFloat;
			}
		}
		//check if the Ytextfields is not empty
		if(yAxisString != ""){
			float myLocalYFloat = float.Parse(yAxisString);
			
			//Check if textfield changed Ysensitivity
			if( prevYSensitivity != myLocalYFloat){
				yAxis = myLocalYFloat;
			}
		}
		
		//create the button to close the window
		if(GUI.Button(new Rect(window0.width - 125 - 15, window0.height-40, 125, 30), "Back", specialButton)){
			displayWindow = false;
		}
		
		//Aply calculations of the sensitivity to the MouseLook script
		MouseLook.sensitivityX = xAxis;
		MouseLook.sensitivityY = yAxis;
	}
	
	public static void AddToEnemies(GameObject obj){
		enemies.Add(obj);
	}
}

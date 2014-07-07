using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public Texture menu;
	public Texture2D customeCursor;
	private CursorMode cursorMode = CursorMode.Auto;
	static bool drawHelpScreen = true;
	
	public GUIStyle anyButton;
	public GUIStyle specialButton;
	public GUIStyle anyText;
	public GUIStyle specialText;
	public GUIStyle windowStyle;
	
	public Rect optionsWindow;
	public Rect creditWindow;
	private bool displayOptionsWindow = false;
	private bool displayCreditsWindow = false;
	
	public static float xAxis = 15.0f;
	public static float yAxis = 15.0f;
	private string xAxisString = "";
	private string yAxisString = "";
	private float prevXSensitivity = 0.0f;
	private float prevYSensitivity = 0.0f;
	
	float width;
	float height;
	float posX;
	float posY;
	
	int fontSize;
	
	public string lastTooltip = " ";
	public AudioClip onMouseHover;
	public AudioClip onMouseClick;
	
	private Rect playRect;
	private Rect optionsRect;
	private Rect creditsRect;
	private Rect quitRect;
	private Rect backRectCredit;
	private Rect backOptionRect;
	
	void Start(){
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
		width = Screen.width / 2.5f;
		height = Screen.height / 4;
		posX = Screen.width / 2;
		posY = Screen.height / 2.5f;
		fontSize = Mathf.FloorToInt(Screen.width / 45.5f);
		optionsWindow = new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2);
		creditWindow = new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2);
		Screen.showCursor = true;
		//rects;
		playRect = new Rect(posX - width / 2, posY + height /6, width, height / 1.5f);
		optionsRect = new Rect(posX - width / 3, posY + 5*height / 6, width/1.5f, height / 2.5f);
		creditsRect = new Rect(posX - width / 3,  posY + 8*height / 6, width/1.5f, height / 2.5f);
		quitRect = new Rect(posX - width / 3, posY + 11*height / 6, width/1.5f, height / 2.5f);
		backRectCredit = new Rect(creditWindow.width - 125 - 15, creditWindow.height-40, 125, 30);
		backOptionRect = new Rect(optionsWindow.width - 125 - 15, optionsWindow.height-40, 125, 30);
	}
	
	// Use this for initialization
	void OnGUI() {
		//Display background texture
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), menu, ScaleMode.StretchToFill, true, 10.0F);
		//Display the buttons
		//GUI.BeginGroup (new Rect (posX, posY, Screen.width, Screen.height));
		if(displayOptionsWindow){
			optionsWindow = GUI.Window(0, optionsWindow, ChangeConfigurations, "Game Configurations", windowStyle);
		}
		if(displayCreditsWindow){
			creditWindow = GUI.Window(0, creditWindow, SeeCredits, "Credit List", windowStyle);
		}
		//set the font size
		anyButton.fontSize = fontSize;
		//center the text in the buttons
		anyButton.alignment = TextAnchor.MiddleCenter;
		//create button play that will elad you to the game
		if(GUI.Button(playRect, new GUIContent("Play", "Play"), anyButton)){
			audio.PlayOneShot(onMouseClick);
			if(drawHelpScreen){
				Application.LoadLevel("HelpScreenScene01"); 
				drawHelpScreen = false;
			}else{
				Vector2 posMouse = new Vector2(Event.current.mousePosition.x - 16,Event.current.mousePosition.y - 16);
				Cursor.SetCursor(customeCursor, posMouse, cursorMode);
//				Application.LoadLevel ("LevelScene01");
				Application.LoadLevel("LoadingScreen");
			}
		}
		//create button quite that will cancle the game
		anyButton.fontSize = Mathf.FloorToInt(fontSize / 1.8f);
		if(GUI.Button(optionsRect, new GUIContent("Options", "Options"), anyButton)){
			audio.PlayOneShot(onMouseClick);
			displayOptionsWindow = true;
			Debug.Log("Access Options");
		}
		if(GUI.Button(creditsRect,  new GUIContent("Credits", "Credits"), anyButton)){
			audio.PlayOneShot(onMouseClick);
			displayCreditsWindow = true;
			Debug.Log("Access Credits");
		}
		if(GUI.Button(quitRect,new GUIContent("Quit", "Quit"), anyButton)){
			audio.PlayOneShot(onMouseClick);
			Application.Quit();
		}
		//GUI.EndGroup ();
		//play music on mouse over
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTooltip) {
			if (lastTooltip != ""){
				
			}
			
			if (GUI.tooltip != ""){
				Debug.Log("OnMouseOver");
				audio.PlayOneShot(onMouseHover);
			}
			
			lastTooltip = GUI.tooltip;
		}
		
	}
	
	void SeeCredits(int windowID){
		//anyText.fontSize = Mathf.FloorToInt(fontSize / 0.5f);
		//anyButton.alignment = TextAnchor.MiddleCenter;
		//GUI.Label (new Rect (creditWindow.width/15, creditWindow.height/8.5f, creditWindow.width/3, 30), "Credit List", anyText);
		specialText.fontSize = Mathf.FloorToInt(fontSize / 1.5f);
		GUI.Label (new Rect (creditWindow.width/2.8f, creditWindow.height/6.5f, creditWindow.width/4, 30), "ART: ", specialText);
		GUI.Label (new Rect (creditWindow.width/2.8f, creditWindow.height/6.5f + 30, creditWindow.width/4, 25), "Jonathan van Immerzeel", specialText);
		GUI.Label (new Rect (creditWindow.width/2.8f, creditWindow.height/6.5f + 60, creditWindow.width/4, 25), "Long Zhang", specialText);
		GUI.Label (new Rect (creditWindow.width/2.8f, creditWindow.height/6.5f + 90, creditWindow.width/4, 25), "Ulvis Bariss ", specialText);
		GUI.Label (new Rect (creditWindow.width/2.8f, creditWindow.height/6.5f + 150, creditWindow.width/4, 25), "PROGRAMMING: ", specialText);
		GUI.Label (new Rect (creditWindow.width/2.8f, creditWindow.height/6.5f + 180, creditWindow.width/4, 25), "Alexandra Cunetchi", specialText);
		GUI.Label (new Rect (creditWindow.width/2.8f, creditWindow.height/6.5f + 210, creditWindow.width/4, 25), "Michel Nijland", specialText);
		GUI.Label (new Rect (creditWindow.width/2.8f, creditWindow.height/6.5f + 240, creditWindow.width/4, 25), "Raymond Meiring", specialText);
		specialButton.alignment = TextAnchor.MiddleCenter;
		if(GUI.Button(backRectCredit, new GUIContent("Back", "Back"), specialButton)){
			audio.PlayOneShot(onMouseClick);
			displayCreditsWindow = false;
		}
		
	}
	void ChangeConfigurations(int windowID){
		//Setup font size
		anyText.fontSize = Mathf.FloorToInt(fontSize / 1.5f);
		
		//Display text of the options sequence
		GUI.Label (new Rect (optionsWindow.width/15, optionsWindow.height/8.5f, optionsWindow.width/3, 30), "Mouse sensitivity", anyText);
		
		//Setup smaller font size
		anyText.fontSize = Mathf.FloorToInt(fontSize / 2.5f);
		
		//Display text of the options
		GUI.Label (new Rect (optionsWindow.width/15, optionsWindow.height/3.5f, optionsWindow.width/4, 30), "X Axis", anyText);
		GUI.Label (new Rect (optionsWindow.width/15, optionsWindow.height/2.5f, optionsWindow.width/4, 30), "Y Axis", anyText);
		
		//Display and calculate the sensitivity acording to the slider
		xAxis = GUI.HorizontalSlider (new Rect (optionsWindow.width/2.5f, optionsWindow.height/3.5f, optionsWindow.width - optionsWindow.width/2.5f - 75, 30), xAxis, 0.0f, 100.0f);
		yAxis = GUI.HorizontalSlider (new Rect (optionsWindow.width/2.5f, optionsWindow.height/2.5f, optionsWindow.width - optionsWindow.width/2.5f - 75, 30), yAxis, 0.0f, 100.0f);
		
		//Check is slider changed sensitivity
		if(prevXSensitivity != xAxis || prevYSensitivity !=yAxis){
			prevXSensitivity = xAxis;
			prevYSensitivity = yAxis;
			xAxisString = xAxis.ToString();
			yAxisString = yAxis.ToString();
		}
		
		//Create adjustable by user textfield for the sensitivity 
		//xAxisString = xAxis.ToString();
		xAxisString = GUI.TextField(new Rect(optionsWindow.width - 50, optionsWindow.height/3.5f, 37 , 20), xAxisString, 3);
		yAxisString = GUI.TextField(new Rect(optionsWindow.width - 50, optionsWindow.height/2.5f, 37 , 20), yAxisString, 3);
		
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
		specialButton.alignment = TextAnchor.MiddleCenter;
		//create the button to close the window
		if(GUI.Button(backOptionRect, new GUIContent("Back", "Back"), specialButton)){
			audio.PlayOneShot(onMouseClick);
			displayOptionsWindow = false;
		}
		
		//Aply calculations of the sensitivity to the MouseLook script
		MouseLook.sensitivityX = xAxis;
		MouseLook.sensitivityY = yAxis;
	}
}

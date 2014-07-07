using UnityEngine;
using System.Collections;

public class HelpMenu : MonoBehaviour {
	
	public Texture help1;
	public Texture help2;
	public GUIStyle anyButton;
	private bool drawNext = false;
	private bool drawFirst= true; 
	public Texture2D customeCursor;
	private CursorMode cursorMode = CursorMode.Auto;
	int fontSize;
	float width;
	float height;
	
	// Use this for initialization
	void Start () {
		width = Screen.width / 7.5f;
		height = Screen.height / 12.5f;
		fontSize = Mathf.FloorToInt(Screen.width / 45.5f);
		Screen.showCursor = true;
	}
	
	void OnGUI () {
		if(drawFirst){
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), help1);
			anyButton.fontSize = Mathf.FloorToInt(fontSize / 1.5f);;
			anyButton.alignment = TextAnchor.MiddleCenter;
			if(GUI.Button(new Rect(Screen.width- width - 20, Screen.height - height - 25, width, height), "Next", anyButton)){
				drawFirst = false;
				drawNext = true;
			}
		}
		if(drawNext){
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), help2);
			if(GUI.Button(new Rect(Screen.width- width - 20, Screen.height - height - 25, width, height), "Start", anyButton)){
				Vector2 posMouse = new Vector2(Event.current.mousePosition.x - 16,Event.current.mousePosition.y - 16);
				Cursor.SetCursor(customeCursor, posMouse, cursorMode);
//				Application.LoadLevel ("LevelScene01");
				Application.LoadLevel("LoadingScreen");
			}
		}
	}
}

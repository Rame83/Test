using UnityEngine;
using System.Collections;

public class FadeIcon : MonoBehaviour {

    private Color color;
    private MeshRenderer m_meshRender;
    float counter;
   	public float counterSpeed;
	// Use this for initialization
	void Start () {
        color = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
        if (color.a > 0.1f) { 
        	color.a -= 0.011f; 
        	
        }else{
        	color.a = 1.0f;
      	}
        
        renderer.material.color = color;
	}
}

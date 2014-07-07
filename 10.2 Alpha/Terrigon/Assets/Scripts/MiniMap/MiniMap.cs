using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
	
	public GameObject m_FirstPersonController;
	public Transform m_target;
	
	public float m_height;
	
	public float m_depth;
	
	public float m_rect_width;
	public float m_rect_height;
	
	public float m_size;

	
	private float m_rot = 90f;

    Texture2D minimapTexture;
	
	void Awake () {
		createMiniMap ();
        
	}
	
	void Update () {
		
//		if (this.transform.position != null) {
			transform.eulerAngles = new Vector3(m_rot, m_target.localEulerAngles.y, 0);
			transform.position = new Vector3 (m_FirstPersonController.transform.position.x, 
			                                                  m_height, 
			                                                  m_FirstPersonController.transform.position.z);
//		}
		
		// Set layers when player is on firstfloor
		if (m_FirstPersonController.transform.position.y < 7.0f) {
			camera.cullingMask |= 1 << LayerMask.NameToLayer("Floor1");
			camera.cullingMask |= 1 << LayerMask.NameToLayer("MiniMapFirst");
			camera.cullingMask |= 1 << LayerMask.NameToLayer("Fader");
			camera.cullingMask &= ~(1<< LayerMask.NameToLayer("Floor2"));
			camera.cullingMask &= ~(1<< LayerMask.NameToLayer("MiniMapSecond"));
			camera.cullingMask &= ~(1<< LayerMask.NameToLayer("MainCameraRender"));
            
		}
		
		// Set layers when player is on secondfloor
		if (m_FirstPersonController.transform.position.y > 7.0f) {
			camera.cullingMask &= ~(1<< LayerMask.NameToLayer("Roof"));
			camera.cullingMask |= 1 << LayerMask.NameToLayer("Fader");
			camera.cullingMask &= ~(1<< LayerMask.NameToLayer("Floor1"));
			camera.cullingMask &= ~(1<< LayerMask.NameToLayer("MiniMapFirst"));
			camera.cullingMask &= ~(1<< LayerMask.NameToLayer("MainCameraRender"));
			camera.cullingMask |= 1 << LayerMask.NameToLayer("Floor2");
			camera.cullingMask |= 1 << LayerMask.NameToLayer("MiniMapSecond");
		}
	}
	
	void createMiniMap(){
		// Set limitations on MiniMap size
		
		if (m_rect_width < 0.1f)
			m_rect_width = 0.2f;
		if (m_rect_height < 0.1f)
			m_rect_height = 0.35f;

		camera.depth = m_depth;
		camera.orthographic = true;
		camera.rect = new Rect (0f, 0f, m_rect_width, m_rect_height);
		camera.orthographicSize = m_size;
		camera.cullingMask &= ~(1<< LayerMask.NameToLayer("Roof"));
		camera.cullingMask |= 1 << LayerMask.NameToLayer("Fader");
	}
}

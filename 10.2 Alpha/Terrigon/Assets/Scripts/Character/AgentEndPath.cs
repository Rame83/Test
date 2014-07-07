using UnityEngine;
using System.Collections;


public class AgentEndPath : MonoBehaviour {

    Vector3 characterPosition;
    public Transform[] m_endNodes;
    NavMeshAgent m_agent;
    public float speed; 
	// Use this for initialization
	void Start () {
        
        if (speed == 0) { speed = 9; }
        m_agent = gameObject.GetComponent<NavMeshAgent>();
        characterPosition = GameObject.Find("Character").transform.position;
        SetDestination();
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(this.transform.position, m_agent.steeringTarget);
		if (distance < 10) { m_agent.speed = 6; }
		else { m_agent.speed = 40; }
		if (this.transform.position == m_endNodes[0].position || this.transform.position == m_endNodes[1].position) {
			Destroy(this);
		}
	}

    void SetDestination() {
        transform.position = characterPosition;
        if (characterPosition.y < 7.0f) m_agent.SetDestination(m_endNodes[0].position);
        if (characterPosition.y > 7.0f) m_agent.SetDestination(m_endNodes[1].position);
        m_agent.speed = speed;
    }
}

using UnityEngine;
using System.Collections;

public class MachineCount : MonoBehaviour {

    private static int allMachines;
    private Machine machine;
    private static bool win;
	// Use this for initialization
	void Awake () {
        win = false;
        machine = GameObject.Find("Machine").GetComponent<Machine>();
        allMachines = GameObject.FindGameObjectsWithTag("Machine").Length;
	}
	
	// Update is called once per frame
	void Update () {
        if (machine.Machines == allMachines) {
            win = true;
        }
	}

    public static int AllMachines {
        get { return allMachines; }
    }
    public static bool Win {
        get { return win; }
    }
}

using UnityEngine;
using System.Collections;

public class startMenu : MonoBehaviour {
    public globalData data;
	// Use this for initialization
	void Start () {
        Object.DontDestroyOnLoad(data);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, 100, 90), "Loader Menu");

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(20, 40, 80, 20), "Start Game"))
        {
            Application.LoadLevel("Mission");
        }
    }
}

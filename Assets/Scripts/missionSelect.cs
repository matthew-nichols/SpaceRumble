using UnityEngine;
using System.Collections;


public class missionSelect : MonoBehaviour {
	// Use this for initialization
    public globalData data;
    public AllyUnit baseAlly;
    private Object[] units = new Object[20];
    private int numUnits = 0;
 
	void Start () {
	    //set globalData to not be destroyed.
        //populate list from global
        GameObject p = GameObject.Find("GlobalData");
        data = (globalData)p.GetComponent("globalData");
        //every time it starts parse the list to get all units, and gets the number of units.
        for (int i = 0; i < data.allyUnits.Length; i++)
        {
            if (data.allyUnits[i] != null)
            {
                units[numUnits] = data.allyUnits[i];
                numUnits++;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
	}

    //function that builds the unit output.
    void displayUnits()
    {
        //figure out where to start;
        //try screen width over/2
        int w = Screen.width;
        int h = Screen.height;
        int x = w / 2;
        int y = h - h / 4;
        int dx = x / 10;
        int dy = (h - y) / 2;
        for (int i = 0; i < 20; i++)//20 should be numUnits
        {
            int k;//to help with rows;
            int ry;
            if (i >= 10)
            {
                k = i - 10;
                ry = y + dy;
            }
            else
            {
                ry = y;
                k = i;
            }
            string s;
            if (units[i] != null)
            {
                AllyUnit a = (AllyUnit)units[i];

                s = a.name + "\n Dmg: " + a.attackDmg + "\n Rng: " + a.attackRange + "\n HP: " + a.health;
            }
            else
            {
                s = "no unit";
            }
            if (GUI.Button(new Rect(x+k*dx, ry, dx, dy), s))
            {
               // Application.LoadLevel("FirstMap");
            }
        }
    }
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");
        displayUnits();
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Level 1")) {
            AllyUnit a = baseAlly;
            //for testing no units in ally
            for(int i = 0; i<5; i++){
                a.attackDmg += 10;
                data.allyUnits[i] = a;
            }
            Application.LoadLevel("FirstMap");
		}

		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
            AllyUnit a = baseAlly;
            //for testing no units in ally
            for (int i = 0; i < 10; i++)
            {
                a.attackDmg += 10;
                data.allyUnits[i] = a;
            }
          
            Application.LoadLevel("FirstMap");
		}
	}

}

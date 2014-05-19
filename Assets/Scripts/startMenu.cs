using UnityEngine;
using System.Collections;

public class startMenu : MonoBehaviour
{
        public AllyUnit baseAlly;
        
		public globalData data;
		// Use this for initialization
		void Start ()
		{
				Object.DontDestroyOnLoad (data);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnGUI ()
		{
				// Make a background box
//				GUI.Box (new Rect (10, 10, 100, 90), "Loader Menu");
                int w = Screen.width;
                int h = Screen.height;
                int x = w / 3;
                int y = h / 3;
                int dy = h / 9;
                int mw = w / 3;
				// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
				if (GUI.Button (new Rect (x, y, mw, dy), "Start Game(Easy)")) {
                    AllyUnit a = baseAlly;
                    //for testing no units in ally
                    for (int i = 0; i < 10; i++)
                    {
                        data.allyUnits[i] = a;
                    }
						Application.LoadLevel ("Mission");
				}

                // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
                if (GUI.Button(new Rect(x, y+dy, mw, dy), "Start Game(hard)"))
                {
                    AllyUnit a = baseAlly;
                    //for testing no units in ally
                    for (int i = 0; i < 5; i++)
                    {
                        data.allyUnits[i] = a;
                    }
                    Application.LoadLevel("Mission");
                }
        }
}

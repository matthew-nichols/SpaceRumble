using UnityEngine;
using System.Collections;

public class startMenu : MonoBehaviour
{
        public AllyUnitStats baseAlly;
        public Texture2D test;
		public globalData data;
		// Use this for initialization
		void Start ()
		{
				Object.DontDestroyOnLoad (data);
            //not sure if needed.
                for (int i = 0; i < 10; i++)
                {
                    //Object.DontDestroyOnLoad(data.weapons[i]);
                    //Object.DontDestroyOnLoad(data.accessories[i]);
                    //Object.DontDestroyOnLoad(data.armors[i]);
                    Object.DontDestroyOnLoad(data.secondaries[i]);
                    Object.DontDestroyOnLoad(data.mains[i]);
                }
		}

        // Update is called once per frame
        void Update()
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
				if (GUI.Button (new Rect (x, y, mw, dy), new GUIContent("Start Game(Easy)", test))) {

                    for (int i = 0; i < 10; i++)
                    {
                        AllyUnitStats a = Instantiate(baseAlly) as AllyUnitStats; 
               
                        a.UnitName = "Bob " + i;//change to randomized name.
                        a.attackDmg += i * 10;
                        //a.accessory = data.accessories[0];
                        //a.weapon = data.weapons[0];
                        a.secondary = data.secondaries[0];
                        //a.armor = data.armors[0];
                        a.mainslot = data.mains[0];
                        data.allyUnits[i] = a;
                        DontDestroyOnLoad(a);
                    }
						Application.LoadLevel ("Mission");
				}

                // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
                if (GUI.Button(new Rect(x, y+dy, mw, dy), "Start Game(hard)"))
                {

                    //for testing no units in ally
                    for (int i = 0; i < 5; i++)
                    {
                        //m3 = Instantiate(defaultmission) as missionSettings;
                        AllyUnitStats a = Instantiate(baseAlly) as AllyUnitStats;
                        a.UnitName = "Bob " + i;//change to randomized name.
                        a.attackDmg += i * 10;
                        DontDestroyOnLoad(a);
                        data.allyUnits[i] = a;
                    }
                    Application.LoadLevel("Mission");
                }
        }
}

using UnityEngine;

public class missionSelect : MonoBehaviour
{
		enum states {DEFAULT, UNIT_SELECT, UNIT_EQUIP, SHOP};
		public globalData data;
		public AllyUnitStats baseAlly;
		AllyUnitStats[] units = new AllyUnitStats[20];
		Object[] items = new Object[40];//for when we have inventory
		int numUnits = 0;
		public missionSettings defaultmission;
		missionSettings m1;
		missionSettings m2;
		missionSettings m3;
        AllyUnitStats[] selectedUnits = new AllyUnitStats[10];//at most 10 ally units per mission
		private states state = states.DEFAULT;
		private int numSelected = 0;
		private missionSettings selected;
		void Start ()
		{
				//populate list from global
				data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
				//every time it starts parse the list to get all units, and gets the number of units.
				for (int i = 0; i < data.allyUnits.Length; i++) {
						if (data.allyUnits [i] != null) {
								units [numUnits] = data.allyUnits [i];
								numUnits++;
						}
				}
                data.allyUnits = units;//clear dead units
				//build missions
				m1 = Instantiate (defaultmission) as missionSettings;
				m1.difficulty = Random.Range (1, 4);
				m1.allowedUnits = 10 - m1.difficulty;
				m1.goldReward = 100 + m1.difficulty * 20;

				m2 = Instantiate (defaultmission) as missionSettings;
				m2.difficulty = Random.Range (1, 4);
				m2.allowedUnits = 10 - m2.difficulty;
				m2.goldReward = 100 + m2.difficulty * 20;

				m3 = Instantiate (defaultmission) as missionSettings;
				m3.difficulty = Random.Range (1, 4);
				m3.allowedUnits = 10 - m3.difficulty;
				m3.goldReward = 100 + m3.difficulty * 20;
                //by default selected mission is m1;
                selected = m1;
		}
		void selectUnits(){
            numSelected = 0;
			state = states.UNIT_SELECT;
		}
		void Update ()
		{
            switch (state)
            {
                case states.DEFAULT:
                    break;
                case states.SHOP:
                    break;
                case states.UNIT_EQUIP:
                    break;
                case states.UNIT_SELECT://ends if numSelected = selecttedmission.allowedUnits
                    if (numSelected == selected.allowedUnits||numSelected==numUnits)
                    {
                        state = states.DEFAULT;
                    }
                    break;
            }
		}

		//function that builds the unit output.
		void displayUnits ()
		{
				//figure out where to start;
				//try screen width over/2
				int w = Screen.width;
				int h = Screen.height;
				int x = w / 2;
				int y = h - h / 4;
				int dx = x / 10;
				int dy = (h - y) / 2;
				for (int i = 0; i < 20; i++) {//20 should be numUnits
						int k;//to help with rows;
						int ry;
						if (i >= 10) {
								k = i - 10;
								ry = y + dy;
						} else {
								ry = y;
								k = i;
						}
						string s;
						if (units [i] != null) {
								AllyUnitStats a = units [i];
								s = a.UnitName + "\n Dmg: " + a.attackDmg + "\n Rng: " + a.attackRange + "\n HP: " + a.health;
						} else {
								s = "no unit";
						}
						if (GUI.Button (new Rect (x + k * dx, ry, dx, dy), s)) {
								if(state==states.UNIT_SELECT){
                                    AllyUnitStats a = units[i];

                                    selectedUnits[numSelected] = a;
									numSelected++;
								}
						}
				}
		}

		string buildMission (missionSettings m)
		{
				string s;
				if (m == null)
						return "No mission";
				//Difficulty
				if (m.difficulty == 0) {//Difficulty intepreted by gamecontrol possibly
						s = "Mission difficulty: Easy";
				} else if (m.difficulty == 1) {
						s = "Mission difficulty: Medium";
				} else if (m.difficulty == 2) {
						s = "Mission difficulty: Hard";
				} else {
						s = "Mission difficulty: Impossible";
				}
				s += "\n";
				//other constraints(unit number)
				s += "You can bring " + m.allowedUnits + " units on this Mission\n";
				//gold value
				s += "Expected Rewards: \n";
				s += "Gold: " + m.goldReward;
				s += "\nOtherstuffs";

				return s;
		}
		void displayContext ()
		{
				//start of context menu y = 0; x = 1/3 Screen.width;
				int w = Screen.width;
				int h = Screen.height;		
				int x = w/3;
				int y = 0;

				if(state == states.DEFAULT){
					return;
				}else if(state == states.SHOP){

				}else if(state == states.UNIT_EQUIP){

				}else if (state ==states.UNIT_SELECT){
						//display selected units
						int dx = w / 20;
						int dy = (h /4) / 2;
						for(int i = 0; i<numSelected; i++){
								string s;
								if (selectedUnits [i] != null) {
										AllyUnitStats a = (AllyUnitStats)selectedUnits [i];

										s = a.UnitName + "\n Dmg: " + a.attackDmg + "\n Rng: " + a.attackRange + "\n HP: " + a.health;
								} else {
										s = "no unit";
								}
								if (GUI.Button (new Rect (x + i * dx, 0, dx, dy), s)) {
										// Application.LoadLevel("FirstMap");
								}			
						}
				}
		}
		void displayMissions ()
		{
				//have 3 missions on the left side of the 
				int w = Screen.width;
				int h = Screen.height;
				//start in top left corner
				int width = w / 3;
				int dy = h / 3;
				//mission 1;
				string s;
				s = buildMission (m1);
				if (GUI.Button (new Rect (0, 0, width, dy), s)) {
						selected = m1;
				}  
				//mission 2;
				s = "";
				s = buildMission (m2);
				if (GUI.Button (new Rect (0, dy, width, dy), s)) {
						selected = m2;
				}
				//mission 3;
				s = "";
				s = buildMission (m3);
				if (GUI.Button (new Rect (0, 2 * dy, width, dy), s)) {
						selected = m3;
				}
		}


		void displayInventory ()
		{
				int w = Screen.width;
				int h = Screen.height;
				int x = w - w / 4;
				int width = w / 4;
				int dy = h - h / 4;
				string msg = "This is the inventory box!";
				GUI.Box (new Rect (x, 0, width, dy), msg);
		}

		void OnGUI ()
		{
				int w = Screen.width;
				int h = Screen.height;
				int x = w / 3;
				int y = h - h / 4;
				int height = h / 4;
				int width = w / 2 - w / 3;
				// Make a background box
				GUI.Box (new Rect (x, y, width, height), "Mission Menu");
				displayUnits ();
				displayMissions ();
				displayInventory ();
                displayContext();
                if (GUI.Button(new Rect(x + width / 10, y + height / 8, width * 8 / 10, height / 7), "Start Mission"))
                {//should be grayed out if no mission is selected
                        if (state == states.DEFAULT)
                        {
                            //populate list of units.
                            if (numSelected == 0)
                            {
                                for (int i = 0; (i < selected.allowedUnits) && (i < numUnits); i++)//Either goes to the number of units allowed, or the current number of units
                                {
                                    selectedUnits[i] = units[i];
                                    numSelected = i;
                                }

                                data.selectedUnits = selectedUnits;
                                data.numUnits = numSelected;
                            }
                            else
                            {
                                data.selectedUnits = selectedUnits;
                                data.numUnits = numSelected;
                            }
                            
                            Application.LoadLevel("FirstMap");
                        }
                }
                if (state != states.UNIT_SELECT)
                {
                    if (GUI.Button(new Rect(x + width / 10, y + 2 * height / 8, width * 8 / 10, height / 7), "Select Units"))
                    {//should be grayed out if no mission is selected
                        if (state == states.DEFAULT&&selected!=null)
                        {
                            selectUnits();
                        }
                    }
                }
		}

}
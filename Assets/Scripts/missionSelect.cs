using UnityEngine;

public class missionSelect : MonoBehaviour
{
		enum states {DEFAULT, UNIT_SELECT, UNIT_EQUIP, SHOP};
		public globalData data;
        private AllyUnitStats selectedUnit;
		public AllyUnitStats baseAlly;
		AllyUnitStats[] units = new AllyUnitStats[20];
		Object[] items = new Object[40];//for when we have inventory
		int numUnits = 0;
        int[] selectedIndexes = new int[10];
		public missionSettings defaultmission;
		missionSettings m1;
		missionSettings m2;
		missionSettings m3;
        public Texture2D test;
        AllyUnitStats[] selectedUnits = new AllyUnitStats[10];//at most 10 ally units per mission
		private states state = states.DEFAULT;
		private int numSelected = 0;
		private missionSettings selected;
        private int itemEquip;//0 if main 1 if second
        private int invDisp;//0 main, 1 secondary
        bool contains(int n)//function to check if unit is selected
        {
            for (int i = 0; i < selectedUnits.Length; i++)
            {
                if (selectedIndexes[i] == n) return true;
            }
            return false;
        }
        void clearIndexes()
        {
            for (int i = 0; i < selectedIndexes.Length; i++) selectedIndexes[i] = -1;
        }
        void Start()
		{
                for (int i = 0; i < selectedIndexes.Length; i++)
                {
                    selectedIndexes[i] = -1;
                }
                numUnits = 0;
				//populate list from global
				data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
				//every time it starts parse the list to get all units, and gets the number of units.
                if (data.selectedUnits[0] == null && data.unselectedUnits[0] == null)
                {
                    for (int i = 0; i < data.allyUnits.Length; i++)
                    {
                        if (data.allyUnits[i] != null)
                        {
                            units[numUnits] = data.allyUnits[i];
                            units[numUnits].index = numUnits;
                            numUnits++;
                        }
                        //if( numUnits == 0) game over
                    }
                }
                else
                {//build list of units selected then unselected
   
                    for (int i = 0; i < data.selectedUnits.Length; i++)
                    {
                        if (data.selectedUnits[i] != null)
                        {
                            units[numUnits] = data.selectedUnits[i];
                            units[numUnits].index = numUnits;
                            numUnits++;
                        }
                        //if( numUnits == 0) game over
                    }
                    for (int i = 0; i < data.unselectedUnits.Length; i++)
                    {
                        if (data.unselectedUnits[i] != null)
                        {
                            units[numUnits] = data.unselectedUnits[i];
                            units[numUnits].index = numUnits;
                            numUnits++;
                        }
                        //if( numUnits == 0) game over
                    }
                }
                data.allyUnits = units;//clear dead units
				//build missions
				m1 = Instantiate (defaultmission) as missionSettings;
				m1.difficulty = Random.Range (1, 4);
				m1.allowedUnits = 10 - m1.difficulty;
				m1.goldReward = 100 + m1.difficulty * 20;
                m1.type = "Attack";

				m2 = Instantiate (defaultmission) as missionSettings;
				m2.difficulty = Random.Range (1, 4);
				m2.allowedUnits = 10 - m2.difficulty;
				m2.goldReward = 100 + m2.difficulty * 20;
                m2.type = "Find";
				m3 = Instantiate (defaultmission) as missionSettings;
				m3.difficulty = Random.Range (1, 4);
				m3.allowedUnits = 10 - m3.difficulty;
				m3.goldReward = 100 + m3.difficulty * 20;
                m3.type = "Defend";    
                //by default selected mission is m1;
                selected = m1;
		}
		void selectUnits(){
            numSelected = 0;
            clearIndexes();
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
        string buildItem(Item t)
        {
            string s = "";
            //Item
            //Name, Slot, SellValue
            // DMGmod, RNGmod, ATR, Healthmod, EnergyMod,
            if (t != null)
            {
                s = " exists";
                s = t.itemName + " " + t.slot + " Sells for:" + t.sellValue + "\n";
                if (t.slot == "Main")
                {//Possibly set each one to have at most 3 bonuses???
                    mainSlot w = (mainSlot)t;
                    s += "Damage:" + w.damage + " Range:" + w.range + " Rate:" + w.atkRate + " Health:" + w.healthBoost + " Energy:" + w.energyBoost;
                }
                else if (t.slot == "Secondary")
                {
                    Secondary w = (Secondary)t;
                    s += "Damage:" + w.damageBoost + " Range:" + w.rangeBoost + " Rate:" + w.atkRtBoost + " Health:" + w.healthBoost + " Energy:";// +  w.energyBoost;
                }
            }
            return s;
        //    return new GUIContent(t.icon, s);
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
                    //only build if unit exists
                        if (units[i] != null)
                        {
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
                            string n = "";
                            if (units[i] != null)
                            {
                                AllyUnitStats a = units[i];
                                s = a.UnitName + "\nDamage: " + a.attackDmg + " Range: " + a.attackRange + " Rate: " + a.attackRate + "\nHealth: " + a.health + " Energy: " + a.energy;
                                //s = a.UnitName + "\n" + buildItem((Item)a.weapon) + "\n" + buildItem((Item)a.secondary) + "\n" + buildItem((Item)a.armor) + "\n" + buildItem((Item)a.accessory);
                                n = a.UnitName;
                            }
                            else
                            {
                                s = "no unit";
                            }
                            Rect t = new Rect(x + k * dx, ry, dx, dy);
                            if (GUI.Button(t, new GUIContent(test)))
                            {
                                if (state == states.UNIT_SELECT && !(contains(i)))
                                {
                                    AllyUnitStats a = units[i];
                                    selectedUnits[numSelected] = a;
                                    selectedIndexes[numSelected] = a.index;
                                    numSelected++;
                                }
                                if (state == states.DEFAULT)
                                {
                                    state = states.UNIT_EQUIP;
                                    selectedUnit = units[i];
                                    itemEquip = 0;
                                    invDisp = 0;
                                }
                            }
                            GUI.BeginGroup(new Rect(x+k*dx+ dx/4, ry + dy/10, dx, dy), n);
                            GUI.EndGroup();
                            GUI.tooltip = s;
                            if (t.Contains(Event.current.mousePosition))
                            {
                                GUI.Label(new Rect(x, ry-dy, dx*5, dy), GUI.tooltip);
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
                    s = "Mission difficulty: Easy\t\t\t\t Mission type: " + m.type;
				} else if (m.difficulty == 1) {
                    s = "Mission difficulty: Medium\t\t\t\t Mission type: " + m.type;
				} else if (m.difficulty == 2) {
						s = "Mission difficulty: Hard\t\t\t\t Mission type: " + m.type;
				} else {
                    s = "Mission difficulty: Impossible\t\t\t\t Mission type: " + m.type;
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
                    //DISPLAY SELECTED UNITS STUFFS
                                     //DISPLAY NAME
                    string info;
                    string m;
                    if(itemEquip == 0){
                        m = "Main";
                    }else{
                        m = "Secondary";
                    }

                    info = "Changing Units equipment... Either select the currently equiped " + m + " \nor select a "+m+" slot item from your inventory\nName: " + selectedUnit.UnitName;
                    GUIStyle s = new GUIStyle();
                    s.normal.textColor = Color.white;
                    s.fontSize = 12;
                    GUI.BeginGroup(new Rect(x, y, x, h / 14), info, s);

                    GUI.EndGroup();
                    s.normal.textColor = Color.white;
                    s.fontSize = 20;
                    s.alignment = TextAnchor.MiddleLeft;
                    GUI.BeginGroup(new Rect(x, y + h / 14, x, h / 14), "Main:", s);
                    GUI.EndGroup();
                    if (GUI.Button(new Rect(x + 110, y + h / 14, x, h / 14), buildItem(selectedUnit.mainslot)))
                    {
                        if (itemEquip == 0)
                        {
                            invDisp = 1;
                            itemEquip++;//don't need to change anything as the currently selected armor is left as is.
                        }
                    }
                    s.normal.textColor = Color.white;
                    s.fontSize = 20;
                    s.alignment = TextAnchor.MiddleLeft;
                    GUI.BeginGroup(new Rect(x, y + h / 14+h/14, x, h / 14), "Secondary:", s);
                    GUI.EndGroup();
                    if (GUI.Button(new Rect(x + 110, y + h / 14 + h/14, x, h / 14), buildItem(selectedUnit.secondary)))
                    {
                        if (itemEquip == 1)
                        {
                            state = states.DEFAULT;
                            //Do not need to change the item at all since it is left as what was equiped.
                        }
                    }

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
                Color c = GUI.backgroundColor;
                if (m1 == selected)
                {
                    GUI.backgroundColor = Color.green;
                }
				if (GUI.Button (new Rect (0, 0, width, dy), s)) {
						selected = m1;
				}
                GUI.backgroundColor = c;
				//mission 2;
				
                s = "";
				s = buildMission (m2);
                if (m2 == selected)
                {
                    GUI.backgroundColor = Color.green;
                }	

                if (GUI.Button (new Rect (0, dy, width, dy), s)) {
						selected = m2;
				}
                GUI.backgroundColor = c;
                //mission 3;
				s = "";
				s = buildMission (m3);
                if (m3 == selected)
                {
                    GUI.backgroundColor = Color.green;
                }
                if (GUI.Button (new Rect (0, 2 * dy, width, dy), s)) {
						selected = m3;
				}
                GUI.backgroundColor = c;
		}


        //SWAPS TWO ITEMS
        void swap(Item a, Item b)
        {
            Item temp;
            temp = a;
            a = b;
            b = temp;
        }

		void displayInventory ()
		{
				int w = Screen.width;
				int h = Screen.height;
				int x = w - w / 4;
				int width = w / 4;
				int dy = h - h / 4;
                //two states display main, or secondary
				string msg = "This is the inventory box!";
				GUI.Box (new Rect (x, 0, width, dy), msg);
                if (GUI.Button(new Rect(x, 20, width / 2, 20), "Main"))
                {
                    invDisp = 0;
                }
                if (GUI.Button(new Rect(x + width/2, 20, width / 2, 20), "Secondary"))
                {
                    invDisp = 1;
                }
                
                //now display default items
                int y = dy/12;
                /*if (GUI.Button(new Rect(x, y, width, y), buildItem(data.mains[0])))
                {
                }

                if (GUI.Button(new Rect(x, y * 3, width, y), buildItem(data.secondaries[0])))
                {
                }*/
                //two states 
                if (invDisp == 0)//display main items
                {
                    int j = 0;
                    for (int i = 0; i < data.mainInv.Length; i++)
                    {
                        if (data.mainInv[i])
                        {
                            if (GUI.Button(new Rect(x, y + y * j, width, y), buildItem(data.mainInv[i])))
                            {
                                //TODO Code for when state is equip select
                                mainSlot temp = selectedUnit.mainslot;
                                selectedUnit.mainslot = data.mainInv[i];
                                data.mainInv[i] = temp;
                                itemEquip = 1;
                                invDisp = 1;
                            }
                            j++;
                        }
                    }
                }
                else if (invDisp == 1)//display secondary items
                {
                    int j = 0;
                    for (int i = 0; i < data.secondaryInv.Length; i++)
                    {
                        if (data.secondaryInv[i])
                        {
                            if (GUI.Button(new Rect(x, y+ y * j, width, y), buildItem(data.secondaryInv[i])))
                            {
                                //TODO Code for when state is equip select
                                state = states.DEFAULT;
                                Secondary temp = selectedUnit.secondary;
                                selectedUnit.secondary = data.secondaryInv[i];
                                data.secondaryInv[i] = temp;
                            }
                            j++;
                        }
                    }
                }

            //  GUI.Box(new Rect (x, y, width, y), buildItem(data.weapons[0]));
                //GUI.EndGroup();
             /*   GUI.BeginGroup(new Rect (x, y*2, width, y), buildItem(data.secondaries[0]));
                GUI.EndGroup();
                GUI.BeginGroup(new Rect (x, y*3, width, y), buildItem(data.armors[0]));
                GUI.EndGroup();
        		GUI.BeginGroup(new Rect (x, y*4, width, y), buildItem(data.accessories[0]));
                GUI.EndGroup();*/
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
                            //set missions
                            //populate list of units.
                            if (numSelected == 0)
                            {
                                for (int i = 0; (i < selected.allowedUnits) && (i < numUnits); i++)//Either goes to the number of units allowed, or the current number of units
                                {
                                    selectedUnits[i] = units[i];
                                    selectedUnits[i].index = i;
                                    selectedIndexes[i] = i;
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
                            //build unselected units list;
                            int unselected = 0;
                            for (int i = 0; i < units.Length; i++)
                            {
                                if (contains(i))//if i is selected
                                {
                                }
                                else if (units[i] !=null)
                                {
                                    data.unselectedUnits[unselected] = units[i];
                                    unselected++;
                                }
                            }
                            data.gameMode = selected.type;
                            data.difficulty = selected.difficulty;
                            
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
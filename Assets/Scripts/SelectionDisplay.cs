using UnityEngine;

public class SelectionDisplay : MonoBehaviour
{
		public AllyUnit selected;
		public EnemyUnit selectedEnemy;
		public bool disp = false;
		// variables for size of UI
		public int width = 200;
		public int height = 200;
		int menuX;
		int menuY;
		string health, energy, UnitName, attack, toDisplay;

		void Start ()
		{
				menuX = Screen.width - width;
				menuY = Screen.height - height;
		}

		void Update ()
		{
				// update display info
				if (selected != null) {
						UnitName = selected.UnitName;
						// display health info(max current)
						health = "Health: " + selected.currentHealth + "/" + selected.health;
						// display energy info
						energy = "Energy: " + selected.currentEnergy + "/" + selected.energy;

						// display attack info
						attack = "Attack Damage:" + selected.attackDmg + "\nAttack Range:" + selected.attackRange;

						// TODO?: display on cursor during MOVEMENT phase stamina cost

						// Display during DEFEND phase current target.
						toDisplay = "Selection Info for Unit; " + UnitName + "\n" + health + "\n" + energy + "\n" + attack;
						// TODO: ADD INFO ABOUT EQUPIMENT
				}
		}

		void OnGUI ()
		{
				// Make a background box
				if (disp) {
						GUI.Box (new Rect (Screen.width - width, Screen.height - height, width, height), toDisplay);

						// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
						//  GUI.Box(new Rect(menuX, menuY+20, width, 20), name);
						// Make the second button.
						//   if (GUI.Button(new Rect(20, 70, 80, 20), "Level 2"))
						//   {
						//    Application.LoadLevel(2);
						//   }
				}
		}
}

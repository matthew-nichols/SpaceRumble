using UnityEngine;

public class GameControl : MonoBehaviour
{
		public AllyUnitStats[] allyStats = new AllyUnitStats[10];
		public AllyUnit[] allies = new AllyUnit[10];
		public EnemySpawn[] spawners = new EnemySpawn[5]; // at most 5 enemy spawners (for now)
		public  GameObject enemy;
		public EnemySpawn spawner;
		public AllyUnit baseUnit;
		public bool inWave; // true in wave mode, false in setup mode
		public Music musicObj;
		public int currentEnemies;
		public int wave = 1; // holds the wave number
		bool win;
		// variables that determine enemy stats;
		public int health, damage, spawnNumber;
		public double range;
		public float rate;
		public Vector3 enemyPos;
		public Rect end;
		public int maxDist;
		public globalData data;
		public Vector3 allySpawnLocation;
		private string mode;
		private int difficulty;
		public int wavesLeft;

		void Start ()
		{
				//populate list from global
				data = GameObject.Find ("GlobalData").GetComponent<globalData> ();

				//find spawn points
				spawners = GameObject.Find ("Spawns").GetComponentsInChildren<EnemySpawn> ();

				//build ally units
				for (int i = 0; i < data.selectedUnits.Length; i++) {
						allyStats [i] = data.selectedUnits [i];
				}
				inWave = false;
				musicObj.gameState = inWave;
				//spawn allies
				for (int i = 0; i < allyStats.Length; i++) {
						if (allyStats [i] != null) {
                                
								//code to place ally, will need some sort of offset so not placed on top of each other
								allies [i] = Instantiate (baseUnit, allySpawnLocation + new Vector3 (i * 20.0f, transform.position.y, 5), Quaternion.identity) as AllyUnit;
								allies [i].SendMessage ("SetInfo", allyStats [i]);
						}
				}
				for (int i = 0; i < allies.Length; i++) {
						if (allies [i] != null)
								allies [i].canMove = true;

				} 
				//get mode info
				mode = data.gameMode;
				difficulty = data.difficulty;

				//add start up code for different game modes
				switch (mode) {
				case "Defend"://defend game mode
						wavesLeft = difficulty + 5;
						break;
				case "Attack"://attack game mode
						break;
				case "Find"://Find game mode(unit something etc.
						break;
				}
		}

		//creates a spawner for eneimes with these stats
		EnemySpawn createSpawner (int h, int d, float r, double rng, Vector3 pos, int n, float t, int m)
		{
				//MODE SPECIFIC CODE GOES HERE
				switch (mode) {
				case "Defend"://defend game mode
						break;
				case "Attack"://attack game mode
						break;
				case "Find"://Find game mode(unit something etc.
						break;
				}
				//range might be better as int 
				//create unit
				GameObject e = enemy;
				EnemyUnit unit = e.GetComponent<EnemyUnit> ();
				//unit.attackDmg = d;
				//unit.attackRange = (int)rng;
				//unit.attackRate = r;
				//unit.health = h;
				//unit.currentHealth = h;//can change to reduce dificulty
				unit.control = this;
				//unit.maxDist = m;
        
				EnemySpawn s = spawner;
				s.control = GetComponent<GameControl> ();
				s.sTime = t;
				s.totalEnemies = n;
 
				spawner.unitType = e;
        
				return Instantiate (s, pos, Quaternion.identity) as EnemySpawn;
		}
		//function that checks if all units are alive.
		bool checkUnits ()
		{
				bool f = false;
				for (int i = 0; i < allies.Length; i++) {
						if (allies [i] != null)
								f = true;
				}
				return f;
		}

		void Update ()
		{
				//MODE SPECIFIC CODE GOES HERE
				switch (mode) {
				case "Defend"://defend game mode
						if (wavesLeft == 0) {
								win = true;
								missionOver ();
						}
						break;
				case "Attack"://attack game mode
						if(!GameObject.Find("Objective1")){
								win = true;
								//missionOver ();
						}
						break;
				case "Find"://Find game mode(unit something etc.
						break;
				}
				if (inWave) {
						//wait for player to finish moving units
						//if all enemies are dead call waveEnd
						/*if (spawners [0] && spawners [0].done) {
								Destroy (spawners [0]);
								Destroy (spawners [0].gameObject);
								spawners [0] = null;
						}*/
						bool done = true;
						foreach (EnemySpawn e in spawners)
								done = done && e.done;
						if (currentEnemies == 0 && done) {
								waveEnd ();
						}
				} 
				if (!checkUnits ()) {
						missionFail ();
				}

		}

		void waveStart ()
		{
				//might not be needed
				//MODE SPECIFIC CODE GOES HERE
				switch (mode) {
				case "Defend"://defend game mode
						break;
				case "Attack"://attack game mode
						break;
				case "Find"://Find game mode(unit something etc.
						break;
				}
				inWave = true;
				musicObj.gameState = inWave;
				//gets called by GUI button
				//after the wave starts
				//set all allies to stop movement.
				for (int i = 0; i < allies.Length; i++) {
						if (allies [i] != null) {
								allies [i].canMove = false;
						}
				}
        
				wave++;
				//logic for enemy spawn types go here.
				//spawners [0] = createSpawner (health, damage, rate, range, enemyPos, spawnNumber + wave, 1, maxDist);
		}

		void waveEnd ()
		{
				switch (mode) {
				case "Defend"://defend game mode
						break;
				case "Attack"://attack game mode
						break;
				case "Find"://Find game mode(unit something etc.
						break;
				}
				inWave = false;
				musicObj.gameState = inWave;
				for (int i = 0; i < allies.Length; i++) {
						if (allies [i] != null) {
								AllyUnit ally = allies [i];
								ally.canMove = true;
								ally.currentEnergy = ally.energy;
						}
				}
				if (mode == "Defend") {
						wavesLeft--;
				}
				foreach (EnemySpawn e in spawners)
						e.Reset ();
		}

		void GetInfo2 (AllyUnitStats a, AllyUnit b)//calling member function isn't working
		{
				a.energy = b.energy;
				a.currentEnergy = b.currentEnergy;
				a.mainslot = b.mainslot;
				a.secondary = b.secondary;
				a.health = health;
				a.currentHealth = b.currentHealth;
				a.attackDmg = b.attackDmg;
				a.attackRange = b.attackRange;
				a.attackRate = b.attackRate;
				a.UnitName = b.UnitName;
		}

		void missionOver ()//do mission overy stuff
		{
				//might not be needed
				//MODE SPECIFIC CODE GOES HERE
				switch (mode) {
				case "Defend"://defend game mode
						break;
				case "Attack"://attack game mode
						break;
				case "Find"://Find game mode(unit something etc.
						break;
				}
				//clear list of allyStats

				AllyUnitStats[] selectedUnits = new AllyUnitStats[10];
				for (int i = 0; i < allies.Length; i++) {
						if (allies [i] != null) {
								GetInfo2 (data.selectedUnits [i], allies [i]);
								// allies[i].SendMessage("GetInfo", selectedUnits[numAlive]);

						} else {
								data.selectedUnits [i] = null;
						}
				}
				Application.LoadLevel ("mission");
		}

		void missionFail ()
		{
				//might not be needed
				//MODE SPECIFIC CODE GOES HERE
				switch (mode) {
				case "Defend"://defend game mode
						break;
				case "Attack"://attack game mode
						break;
				case "Find"://Find game mode(unit something etc.
						break;
				}
				for (int i = 0; i < data.selectedUnits.Length; i++) {
						data.selectedUnits [i] = null;
				}
				Application.LoadLevel ("mission");
		}

		void OnGUI ()
		{
				//might not be needed
				//MODE SPECIFIC CODE GOES HERE
				switch (mode) {
				case "Defend"://defend game mode
						GUIStyle s = new GUIStyle ();
						s.normal.textColor = Color.white;
						s.fontSize = 20;
						s.alignment = TextAnchor.MiddleLeft;

						string end = "Waves left: " + wavesLeft;
						GUI.BeginGroup (new Rect (Screen.width - 160, 0, 160, 20), end, s);
						GUI.EndGroup ();
						break;
				case "Attack"://attack game mode
						break;
				case "Find"://Find game mode(unit something etc.
						break;
				}
				if (win)
						GUI.Box (new Rect (Screen.width / 2, Screen.height / 2, 200, 200), "You Win !!!");
				// Make a background box
				if (!inWave) {
						if (GUI.Button (new Rect (20, 70, 160, 20), ("Start Wave: " + wave))) {
								waveStart ();
						}
				}
				if (GUI.Button (new Rect (20, 30, 160, 20), ("Return to Mission Select"))) {
						missionOver ();
				}

		}
}

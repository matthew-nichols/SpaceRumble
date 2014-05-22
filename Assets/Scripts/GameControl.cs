using UnityEngine;

public class GameControl : MonoBehaviour
{
        public AllyUnitStats[] allyStats = new AllyUnitStats[10];
        public AllyUnit[] allies = new AllyUnit[10];

		public EnemySpawn[] spawners = new EnemySpawn[5]; // at most 5 enemy spawners (for now)
		public GameObject enemy;
		public EnemySpawn spawner;
        public AllyUnit baseUnit;
		bool gameState; // true in wave mode, false in setup mode
		public Music musicObj;
		public int currentEnemies;
		public int wave = 1; // holds the wave number
		bool win;
		// variables that determine enemy stats;
		public int health, damage, spawnNumber;
		public double rate, range;
		public Vector3 enemyPos;
		public Rect end;
		public int maxDist;
		public globalData data;
		public Vector3 allySpawnLocation;

		void Start ()
		{
				//populate list from global
				data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
				//build ally units
                for (int i = 0; i < data.selectedUnits.Length; i++)
                {
                    allyStats[i] = data.selectedUnits[i];
                }
				gameState = false;
				musicObj.gameState = gameState;
				//spawn allies
				for (int i = 0; i < allyStats.Length; i++) {
						if (allyStats [i] != null) {
                                
								//code to place ally, will need some sort of offset so not placed on top of each other
								allies [i] = Instantiate (baseUnit, allySpawnLocation + new Vector3(i * 20.0f, transform.position.y, 0), Quaternion.identity) as AllyUnit;
                                allies[i].SendMessage("SetInfo",allyStats[i]);
						}
				}
				for (int i = 0; i < allies.Length; i++) {
						if (allies [i] != null)
								allies [i].canMove = true;

				} 
		}

		//creates a spawner for eneimes with these stats
		EnemySpawn createSpawner (int h, int d, double r, double rng, Vector3 pos, int n, float t, int m)
		{
				//range might be better as int 
				//create unit
				GameObject e = enemy;
				EnemyUnit unit = e.GetComponent<EnemyUnit> ();
				unit.attackDmg = d;
				unit.attackRange = (int)rng;
				unit.attackRate = r;
				unit.health = h;
				unit.currentHealth = h;//can change to reduce dificulty
				unit.control = this;
				unit.maxDist = m;
        
				EnemySpawn s = spawner;
				s.control = GetComponent<GameControl> ();
				s.sTime = t;
				s.totalEnemies = n;
 
				spawner.unitType = e;
        
				return Instantiate (s, pos, Quaternion.identity) as EnemySpawn;
		}
        //function that checks if all units are alive.
        bool checkUnits()
        {
            bool f = false;
            for (int i = 0; i < allies.Length; i++)
            {
                if (allies[i] != null) f = true;
            }
            return f;
        }
        void Update()
		{
				if (gameState) {
						//wait for player to finish moving units
						//if all enemies are dead call waveEnd
						if (spawners [0] && spawners [0].done) {
								Destroy (spawners [0]);
								Destroy (spawners [0].gameObject);
								spawners [0] = null;
						}
						if (currentEnemies == 0 && spawners [0] == null) {
								waveEnd ();
						}
				} else {
						//what needs to be done during the wave
						//need to check if units are in end zone.
						for (int i = 0; i < allies.Length; i++) {
								if (allies [i] != null) {
										Vector3 p;
										Vector3 pos = allies [i].transform.position;
										p = new Vector3 (pos.x, pos.z, pos.z);
										if (end.Contains (p)) {
												win = true;
										}
								}
						}
				}
                if (!checkUnits())
                {
                    missionFail();
                }
		}

		void waveStart ()
		{
				gameState = true;
				musicObj.gameState = gameState;
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
				spawners [0] = createSpawner (health, damage, rate, range, enemyPos, spawnNumber + wave, 1, maxDist);
		}

		void waveEnd ()
		{
				gameState = false;
				musicObj.gameState = gameState;
				for (int i = 0; i < allies.Length; i++) {
                    if (allies[i] != null)
                    {
                        AllyUnit ally = allies[i];
                        ally.canMove = true;
                        ally.currentEnergy = ally.energy;
                    }
				}
                Application.LoadLevel("mission");
		}
        void GetInfo2(AllyUnitStats a, AllyUnit b)//calling member function isn't working
        {
            /* a.targetLocation = targetLocation;
             a.lastAttack = lastAttack;
             a.projectile = projectile;
             a.offset = offset;
             a.velocity = velocity;
             a.delay = delay;
             a.unitSound = unitSound;
             a.fireSound = fireSound;
             a.deathSound = deathSound;
             a.canMove = canMove;*/
            a.energy = b.energy;
            a.currentEnergy = b.currentEnergy;
            //   a.cpos = cpos;
            //  a.ppos = ppos;
            a.weapon = b.weapon;
            a.armor = b.armor;
            a.accessory = b.accessory;
            a.secondary = b.secondary;
            a.health = health;
            a.currentHealth = b.currentHealth;
            a.attackDmg = b.attackDmg;
            a.attackRange = b.attackRange;
            a.attackRate = b.attackRate;
            a.UnitName = b.UnitName;
        }
        void missionOver()//do mission overy stuff
        {

            //clear list of allyStats

            AllyUnitStats[] selectedUnits = new AllyUnitStats[10];
            for (int i = 0; i < allies.Length; i++)
            {
                if (allies[i] != null)
                {
                    GetInfo2(data.selectedUnits[i], allies[i]);
                    // allies[i].SendMessage("GetInfo", selectedUnits[numAlive]);

                }
                else
                {
                    data.selectedUnits[i] = null;
                }
            }
        }
        void missionFail()
        {
            for (int i = 0; i < data.selectedUnits.Length; i++)
            {
                data.selectedUnits[i] = null;
            }
            Application.LoadLevel("mission");
        }
        void OnGUI()
		{
				if (win)
						GUI.Box (new Rect (Screen.width / 2, Screen.height / 2, 200, 200), "You Win !!!");
				// Make a background box
				if (!gameState) {
						if (GUI.Button (new Rect (20, 70, 160, 20), ("Start Wave: " + wave))) {
								waveStart ();
						}
				}
				if (GUI.Button (new Rect (20, 30, 160, 20), ("Return to Mission Select"))) {
                        missionOver();
                        Application.LoadLevel ("mission");
				}
		}
}

using UnityEngine;

public class GameControl : MonoBehaviour
{
		public Object[] allies = new Object[20];//10 allied units
		public EnemySpawn[] spawners = new EnemySpawn[5];//at most 5 enemy spawners
		public GameObject enemy;
		public EnemySpawn spawner;
		bool gameState;//Holds if the game is in setup(false), or wave(true) mode
		public int currentEnemies;
		public int wave = 1;//holds the wave number
		bool win;
		//variables that determine enemy stats;
		public int health, damage, spawnNumber;
		public double rate, range;
		public Vector3 enemyPos;
		public Rect end;
		public int maxDist;
		public globalData data;
		// Use this for initialization
		void Start ()
		{
				//populate list from global
				data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
				//build ally units
				for (int i = 0; i < data.allyUnits.Length; i++) {
						allies [i] = data.allyUnits [i];
				}
				gameState = false;
				//spawn allies
				for (int i = 0; i < allies.Length; i++) {
						if (allies [i] != null) {
								//code to place ally, will need some sort of offset so not placed on top of each other
								allies [i] = Instantiate (allies [i], new Vector3 (i * 40.0f + 700, 10, 0 + 200), Quaternion.identity);
						}
				}
				for (int i = 0; i < allies.Length; i++) {
						if (allies [i] != null)
								((AllyUnit)allies [i]).canMove = true;

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

		void Update ()
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
										Vector3 pos = ((AllyUnit)allies [i]).transform.position;
										p = new Vector3 (pos.x, pos.z, pos.z);
										if (end.Contains (p)) {
												win = true;
										}
								}
						}
				}
		}

		void waveStart ()
		{
				gameState = true;
				//gets called by GUI button
				//after the wave starts
				//set all allies to stop movement.
				for (int i = 0; i < allies.Length; i++) {
						if (allies [i] != null) {
								((AllyUnit)allies [i]).canMove = false;
						}
				}
        
				wave++;
				//logic for enemy spawn types go here.
				spawners [0] = createSpawner (health, damage, rate, range, enemyPos, spawnNumber + wave, 1, maxDist);
		}

		void waveEnd ()
		{
				gameState = false;
				for (int i = 0; i < allies.Length; i++) {
						if (allies [i] != null) {
								AllyUnit ally = (AllyUnit)allies [i];
								ally.canMove = true;
								ally.currentEnergy = ally.energy;
						}

				}
		}

		void OnGUI ()
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
						Application.LoadLevel ("mission");
				}
		}
}

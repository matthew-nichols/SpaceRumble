using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
		public ArrayList enemies = new ArrayList ();
		public GameObject unitType;
		public double totalEnemies = 50;
		public Vector3 targetLocation;
		float spawnTime = 0.0f;
		public float sTime = 0.0f;
		public bool done = false;
		public GameControl control;

		void Start ()
		{

		}

		void Update ()
		{
				if (enemies.Count >= totalEnemies) {
						done = true;
						return;
				} else {
						if (spawnTime >= sTime) {
								if (control != null) {
										control.currentEnemies++;
								}
								EnemyUnit newEnemy = Instantiate (unitType, transform.position, transform.rotation) as EnemyUnit;
								enemies.Add (newEnemy);
								spawnTime = 0.0f;
						}
				}
				spawnTime += Time.deltaTime;
		}
}

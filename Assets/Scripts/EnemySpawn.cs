using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
		public GameObject unitType;
		public int totalEnemies = 50;
		float spawnTime = 0;
		public float sTime = 0;
		public bool done = false;
		public GameControl control;
		int spawned = 0;

		void Start ()
		{
				if (control == null)
						control = GameObject.Find ("GameControl").GetComponent<GameControl> ();
		}

		void Update ()
		{
				if (control.inWave) {
						if (spawned >= totalEnemies) {
								done = true;
								return;
						} else {
								if (spawnTime >= sTime) {
										if (control != null) {
												control.currentEnemies++;
										}
										EnemyUnit newEnemy = Instantiate (unitType, transform.position, transform.rotation) as EnemyUnit;
										spawned++;
										spawnTime = 0.0f;
								}
						}
						spawnTime += Time.deltaTime;
				}
		}

		public void Reset ()
		{
				done = false;
				spawned = 0;
		}
}

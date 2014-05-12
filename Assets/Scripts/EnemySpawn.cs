﻿using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{

		public ArrayList enemies = new ArrayList ();
		public GameObject unitType;
		public double totalEnemies = 50;
		public Vector3 targetLocation;
		float spawnTime = 0.0f;
		public float sTime = 0.0f;
	
		void Start ()
		{
	
		}

		void Update ()
		{
				if (enemies.Count >= totalEnemies)
						return;
				else {
						if (spawnTime >= sTime) {
								EnemyUnit newEnemy = Instantiate (unitType, transform.position, transform.rotation) as EnemyUnit;
								enemies.Add (newEnemy);
								spawnTime = 0.0f;
						}
				}
				spawnTime += Time.deltaTime;
		}
}

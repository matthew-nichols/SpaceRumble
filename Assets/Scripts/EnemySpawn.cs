using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public ArrayList enemies = new ArrayList();
	public GameObject unitType;
	public double totalEnemies = 50;
	public Vector3 targetLocation;
	private float spawnTime = 0.0f;
	public float sTime = 0.0f;
	//private int currentEnemy = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(enemies.Count >= totalEnemies)
			return;
		else{
			if(spawnTime >= sTime){
				EnemyUnit newEnemy = Instantiate(unitType, transform.position, transform.rotation) as EnemyUnit;
				enemies.Add(newEnemy);
				spawnTime = 0.0f;
			}
		}
		spawnTime += Time.deltaTime;
	}
}

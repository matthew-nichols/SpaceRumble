using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    //Variables
    public Object[] allies = new Object[10];//10 allied units
    public EnemySpawn[] spawners = new EnemySpawn[5];//at most 5 enemy spawners
    public GameObject enemy;
    public EnemySpawn spawner;
    //enum gameStates { SETUP, WAVE };
    private bool gameState;//Holds if the game is in setup(false), or wave(true) mode
    public int currentEnemies;
    public int wave =1;//holds the wave number
    private bool win;
    //variables that determine enemy stats;
    public int health, damage, spawnNumber; 
    public double rate, range;
    public Vector3 enemyPos;
    public Rect end;
    public int maxDist;

	// Use this for initialization
	void Start () {
        gameState = false;
        //spawn allies
        for (int i = 0; i < allies.Length; i++)
        {
            if (allies[i] != null)
            {
                //code to place ally, will need some sort of offset so not placed on top of each other
                allies[i] = Instantiate(allies[i], new Vector3(i * 40.0f + 700, 10, 0 + 200), Quaternion.identity);
            }
        }
        for (int i = 0; i < allies.Length; i++)
        {
            if (allies[i] != null)
                ((AllyUnit)allies[i]).canMove = true;

        } 


	}

    //creates a spawner for eneimes with these stats
    EnemySpawn createSpawner(int h, int d, double r, double rng, Vector3 pos, int n, float t, int m)
    {
        //range might be better as int 
        //create unit
        GameObject e = enemy;
        e.GetComponent<EnemyUnit>().attackDmg = d;
        e.GetComponent<EnemyUnit>().attackRange = (int)rng;
        e.GetComponent<EnemyUnit>().attackRate = r;
        e.GetComponent<EnemyUnit>().health = h;
        e.GetComponent<EnemyUnit>().currentHealth = h;//can change to reduce dificulty
        e.GetComponent<EnemyUnit>().control = this;
        e.GetComponent<EnemyUnit>().maxDist = m;
        
        EnemySpawn s = spawner;
        s.control = this.GetComponent<GameControl>();
        s.sTime = t;
        s.totalEnemies = n;
 
        spawner.unitType = e;
        
        return Instantiate(s, pos, Quaternion.identity) as EnemySpawn;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (gameState)//wait for player to finish moving units
        {
            //if all enemies are dead call waveEnd

            if (spawners[0]&&spawners[0].done == true)
            {
                Destroy(spawners[0]);
                Destroy((spawners[0].gameObject));
                spawners[0] = null;
            }
            if (currentEnemies == 0 && spawners[0] == null)
            {
                waveEnd();
            }
        }
        else//what needs to be done during the wave
        {
            //need to check if units are in end zone.
            for (int i = 0; i < allies.Length; i++)
            {
                if (allies[i] != null)
                {
                    Vector3 p;
                    p = new Vector3((((AllyUnit)allies[i]).transform.position).x, (((AllyUnit)allies[i]).transform.position).z, (((AllyUnit)allies[i]).transform.position).z);
                    if (end.Contains(p))
                    {

                        win = true;
                    }
                }
            }
        }
	
	}
    void waveStart()
    {
        gameState = true;
        //gets called by GUI button
        //after the wave starts
        //set all allies to stop movement.
        for (int i = 0; i < allies.Length; i++)
        {
            if(allies[i] != null){
                ((AllyUnit)allies[i]).canMove = false;
            }
        }
        
        wave++;
        //logic for enemy spawn types go here.
        spawners[0] = createSpawner(health, damage, rate, range, enemyPos, spawnNumber+wave, 1, maxDist);



    }
    void waveEnd()
    {
        gameState = false;
        for (int i = 0; i < allies.Length; i++)
        {
            if (allies[i] != null)
            {
                ((AllyUnit)allies[i]).canMove = true;
                ((AllyUnit)allies[i]).currentEnergy = ((AllyUnit)allies[i]).energy;
            }

        }
    }

    void OnGUI()
    {
        if(win)
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 200, 200), "You Win !!!");
        // Make a background box
        if (!gameState)
        {
               if (GUI.Button(new Rect(20, 70, 160, 20), ("Start Wave: " + wave)))
               {
                   waveStart();
               }
        }
    }
}

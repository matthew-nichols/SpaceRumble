using UnityEngine;
using System.Collections;

public class AllyUnit : baseUnit {
	
	public Vector3 targetLocation;
	public float lastAttack = 0f;
	public Rigidbody projectile;
	public Vector3 offset;
	public float velocity = 100;
	public int delay = 3;

	public AudioSource unitSound;
	public AudioClip fireSound;
	
    public bool canMove;
    public int energy;
    public int currentEnergy;
	// Use this for initialization
	void Start () {
		base.Start();
	}

	  EnemyUnit FindClosestEnemy() 
	  {
        EnemyUnit[] gos;
        gos = EnemyUnit.gameObject.FindGameObjectsWithTag("Enemy");
        EnemyUnit closest = FindObjectOfType<EnemyUnit>();
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (EnemyUnit go in gos) 
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) 
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
	
	// Update is called once per frame
	void Update () {

        if (currentHealth <= 0)
        {
            Destroy(gameObject.rigidbody);
            Destroy(gameObject);
        }
		if(isClicked){
			print(gameObject.name + " is active: " + gameObject.activeSelf);
			renderer.material = onHoverMaterial;
            moveUnit();
		}
		else{
			renderer.material = defaultMaterial;
		}


	if(!currentTarget)
	{
		currentTarget = FindObjectOfType<EnemyUnit>();	
	}
	
	transform.LookAt(currentTarget.transform);
    if (Vector3.Distance(transform.position, currentTarget.transform.position) <= attackRange && lastAttack >= attackRate)
    {
    	
        Rigidbody clone;
        clone = (Rigidbody)Instantiate(projectile, transform.position + offset, transform.rotation);
		unitSound.PlayOneShot(fireSound, 1);
        clone.velocity = transform.TransformDirection(Vector3.forward * velocity) + new Vector3(Time.deltaTime * velocity, 0, 0);
        //removeObject(clone, 3);
        lastAttack = 0;
        Destroy(clone, delay);
        Destroy(clone.gameObject, delay);
    }
    lastAttack += Time.deltaTime;

	}
	/*void OnMouseEnter(){
		renderer.material = onHoverMaterial;
	}
	void OnMouseExit(){
		renderer.material = defaultMaterial ;
	}*/
	void moveUnit(){
		if(updateRightClick){
			Debug.Log("moving unit");
			agent.SetDestination(destinationVector);
			updateRightClick = false;
		}
	}



}

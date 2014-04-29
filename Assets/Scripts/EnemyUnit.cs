using UnityEngine;
using System.Collections;

public class EnemyUnit : baseUnit {

	public Vector3 targetLocation;
    public Rigidbody projectile;
    public int delay = 3;
    public float velocity = 100;
    public Vector3 offset;
    public float lastAttack = 0f;

	//public AudioSource selectSound;
	public AudioSource unitSound;
	public AudioClip fireSound;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update () {
        if (currentHealth <= 0)
        {
            Destroy(gameObject.rigidbody);
            Destroy(gameObject);
        }
		currentTarget = FindObjectOfType<AllyUnit>();
		agent.SetDestination(currentTarget.transform.position);
		if(isClicked){
			print(gameObject.name + " is active: " + gameObject.activeSelf);
			renderer.material = onHoverMaterial;
		}
		else{
			renderer.material = defaultMaterial;
		}

        if (Vector3.Distance(transform.position, currentTarget.transform.position) < attackRange && lastAttack > attackRate)
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
	void OnMouseEnter(){
		renderer.material = onHoverMaterial;
	}
	void OnMouseExit(){
		renderer.material = defaultMaterial ;
	}

}

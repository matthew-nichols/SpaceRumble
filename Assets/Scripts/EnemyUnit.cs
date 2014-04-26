using UnityEngine;
using System.Collections;

public class EnemyUnit : baseUnit {

	public Vector3 targetLocation;
	public baseUnit currentTarget;

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
	}
	void OnMouseEnter(){
		renderer.material = onHoverMaterial;
	}
	void OnMouseExit(){
		renderer.material = defaultMaterial ;
	}

}

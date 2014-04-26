using UnityEngine;
using System.Collections;

public class AllyUnit : baseUnit {
	
	public Vector3 targetLocation;
	public baseUnit currentTarget;
    public bool canMove;
    public int energy;
    public int currentEnergy;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isClicked){
			print(gameObject.name + " is active: " + gameObject.activeSelf);
			renderer.material = onHoverMaterial;
		}
		else{
			renderer.material = defaultMaterial;
		}
		moveUnit();
	}
	void OnMouseEnter(){
		renderer.material = onHoverMaterial;
	}
	void OnMouseExit(){
		renderer.material = defaultMaterial ;
	}
	void moveUnit(){
		if(updateRightClick){
			Debug.Log("moving unit");
			agent.SetDestination(destinationVector);
			updateRightClick = false;
		}
	}
}

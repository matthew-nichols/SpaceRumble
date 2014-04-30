using UnityEngine;
using System.Collections;

public class AllyUnit : baseUnit {
	
	public Vector3 targetLocation;
	
    public bool canMove;
    public int energy;
    public int currentEnergy;
	// Use this for initialization
	void Start () {
		base.Start();
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
            if (canMove)
            {
                moveUnit();
            }
		}
		else{
			renderer.material = defaultMaterial;
		}

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

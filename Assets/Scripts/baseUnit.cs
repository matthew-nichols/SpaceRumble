﻿using UnityEngine;
using System.Collections;

public class baseUnit : MonoBehaviour {

	public Material defaultMaterial;
	public Material onHoverMaterial;
	public int health;
	public int energy;
	public bool isClicked = false;
	public bool updateRightClick = false;
	public Vector3 destinationVector;
	public NavMeshAgent agent;

	void Start () {
		//defaultMaterial = renderer.material;
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update () {
		if(isClicked){
			print(gameObject.name + " is active: " + gameObject.activeSelf);
			renderer.material = onHoverMaterial;
			//moveUnit();
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
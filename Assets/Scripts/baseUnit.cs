﻿using UnityEngine;

public class baseUnit : MonoBehaviour
{
		public Material defaultMaterial;
		public Material onHoverMaterial;
		public int health;
		public int currentHealth;
		public int attackDmg;
		public int attackRange;
		public double attackRate;
		public bool isClicked = false;
		public bool updateRightClick = false;
		public Vector3 destinationVector;
		protected NavMeshAgent agent;
		public string UnitName = "unset";
		public baseUnit currentTarget;
		public ParticleSystem deathExplosion;
		public GameControl control;

		protected virtual void Start ()
		{
				agent = GetComponent<NavMeshAgent> ();
		}
		
		protected virtual void Update ()
		{
				if (isClicked) {
						renderer.material = onHoverMaterial;
				} else {
						renderer.material = defaultMaterial;
				}
		}

		void OnMouseEnter ()
		{
				renderer.material = onHoverMaterial;
		}

		void OnMouseExit ()
		{
				renderer.material = defaultMaterial;
		}

		public virtual void ApplyDamage (int n)
		{
				currentHealth -= n;
		}
}

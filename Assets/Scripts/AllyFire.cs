﻿using UnityEngine;

public class AllyFire : baseUnit
{
		public Vector3 targetLocation;
		public Rigidbody projectile;
		public int delay = 3;
		public float velocity = 100;
		public Vector3 offset;
		public float lastAttack = 0f;
		public AudioSource unitSound;
		public AudioClip fireSound;

		void Start ()
		{
		
		}
	
		void Update ()
		{
				currentTarget = FindObjectOfType<EnemyUnit> ();
				if (Vector3.Distance (transform.position, currentTarget.transform.position) < attackRange && lastAttack > attackRate) {
						Rigidbody clone;
						clone = (Rigidbody)Instantiate (projectile, transform.position + offset, transform.rotation);
						unitSound.PlayOneShot (fireSound, 1);
						clone.velocity = transform.TransformDirection (Vector3.forward * velocity) + new Vector3 (Time.deltaTime * velocity, 0, 0);
						
						lastAttack = 0;
						Destroy (clone, delay);
						Destroy (clone.gameObject, delay);
				}
				lastAttack += Time.deltaTime;
		}
}
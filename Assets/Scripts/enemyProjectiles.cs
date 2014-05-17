using UnityEngine;

public class enemyProjectiles : MonoBehaviour
{
		public float projectileSpeed;
		public float range = 10;
		public float dist;
		public int dmg = 10;
		public int maxTime = 10;
		public ParticleSystem impactEffect;
		float time;

		void Start ()
		{

		}

		void Update ()
		{
				float amntToMove = projectileSpeed * Time.deltaTime;
				transform.Translate (Vector3.forward * amntToMove);
				time += Time.deltaTime;
				if (time > maxTime) {
						Destroy (gameObject);
				}
		}

		void OnCollisionEnter (Collision other)
		{
				if (other.gameObject.tag == "Player" || other.gameObject.tag == "Terrain") {
						ContactPoint contact = other.contacts[0];
						Vector3 pos = contact.point;
						Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal); 
						ParticleSystem temp = Instantiate(impactEffect, pos, rot) as ParticleSystem;
						Destroy (gameObject);
						Destroy (temp.gameObject, 3);
						((baseUnit)other.gameObject.GetComponent ("baseUnit")).currentHealth -= dmg;
				}
		}
}

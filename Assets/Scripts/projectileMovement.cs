using UnityEngine;

public class projectileMovement : MonoBehaviour
{
		public float projectileSpeed;
		public float range = 10;
		public float dist;
		public int dmg = 10;
		public int maxTime = 10;
		public float time;
		public ParticleSystem impactEffect;

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
				if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Terrain") {
						ContactPoint contact = other.contacts[0];
						Vector3 pos = contact.point;
						Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal); 
						ParticleSystem temp = Instantiate(impactEffect, pos, rot) as ParticleSystem;
						Destroy (gameObject);
						Destroy (temp.gameObject, 3);
						baseUnit unit = other.gameObject.GetComponent<baseUnit>();
						if (unit) unit.currentHealth -= 10;
				}
		}
}

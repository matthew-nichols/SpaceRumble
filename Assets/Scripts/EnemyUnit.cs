using UnityEngine;

public class EnemyUnit : baseUnit
{
		public Vector3 targetLocation;
		public Rigidbody projectile;
		public int delay = 3;
		public float velocity = 100;
		public Vector3 offset;
		public float lastAttack = 0f;
		public int maxDist = 100;
		public AudioSource unitSound;
		public AudioClip fireSound;

		protected override void Start ()
		{
				base.Start ();
		}

		void Update ()
		{
				base.Update ();
				if (currentHealth <= 0) {
						ParticleSystem temp = (ParticleSystem)Instantiate(deathExplosion, transform.position, transform.rotation);
						control.currentEnemies--;
						Destroy (gameObject.rigidbody);
						Destroy (gameObject);
						Destroy (temp.gameObject, 5);
				}

				currentTarget = FindObjectOfType<AllyUnit> ();
				if (Vector3.Distance (transform.position, currentTarget.transform.position) < maxDist) {
						agent.Stop ();
				} else {
						agent.SetDestination (currentTarget.transform.position);
				}

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

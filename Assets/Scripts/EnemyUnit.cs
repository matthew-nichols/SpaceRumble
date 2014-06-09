using UnityEngine;

public class EnemyUnit : baseUnit
{
		public Vector3 targetLocation;
		public Rigidbody projectile;
		public int delay = 3;
		public float velocity = 100;
		public Vector3 offset;
		public float lastAttack = 0;
		public int maxDist = 100;
		public AudioSource unitSound;
		public AudioClip fireSound;
		public AudioClip deathSound;
		public bool isTower = false;

		protected override void Start ()
		{
				base.Start ();
		}

		protected override void Update ()
		{
				base.Update ();
				if (currentHealth <= 0) {
						if (deathExplosion) {
								ParticleSystem temp = Instantiate (deathExplosion, transform.position, transform.rotation) as ParticleSystem;
								Destroy (temp.gameObject, 5);
						}
						if (deathSound) {
								PlayClipAt (deathSound, transform.position);
						}
						Destroy (gameObject.rigidbody);
						Destroy (gameObject);
						if(!isTower)
							control.currentEnemies--;
						OnDeath ();
				}

				AllyUnit[] allTargets = FindObjectsOfType<AllyUnit> ();
				float dist = float.PositiveInfinity;
				foreach (AllyUnit u in allTargets) {
						float d = Vector3.Distance (u.transform.position, transform.position);
						if (d < dist) {
								dist = d;
								currentTarget = u;
						}
				}

				if (currentTarget) {
						if (Vector3.Distance (transform.position, currentTarget.transform.position) < maxDist) {
								agent.Stop ();
						} else {
								agent.SetDestination (currentTarget.transform.position);
						}

			if (Vector3.Distance (transform.position, currentTarget.transform.position) < attackRange && lastAttack > attackRate && control.gameState) {
								Rigidbody clone = Instantiate (projectile, transform.position + offset, transform.rotation) as Rigidbody;
								unitSound.PlayOneShot (fireSound, 0.1f);
								clone.SendMessage ("updateDmg", attackDmg);
								clone.velocity = transform.TransformDirection (Vector3.forward * velocity) + new Vector3 (Time.deltaTime * velocity, 0, 0);
								clone.transform.LookAt(currentTarget.transform, Vector3.down);
								lastAttack = 0;
								Destroy (clone, delay);
								Destroy (clone.gameObject, delay);
						}
						transform.LookAt (currentTarget.transform);
				}
				lastAttack += Time.deltaTime;
		}

		AudioSource PlayClipAt (AudioClip clip, Vector3 pos)
		{
				GameObject tempGO = new GameObject ("TempAudio " + clip.name);
				tempGO.transform.position = pos;
				AudioSource aSource = tempGO.AddComponent<AudioSource> ();
				aSource.clip = clip;
				aSource.rolloffMode = unitSound.rolloffMode;
				aSource.pitch = unitSound.pitch;
				aSource.minDistance = unitSound.minDistance;
				aSource.maxDistance = unitSound.maxDistance;
				aSource.dopplerLevel = unitSound.dopplerLevel;
				aSource.volume = 1.0f;
				aSource.Play ();
				Destroy (tempGO, clip.length);
				return aSource;
		}
}

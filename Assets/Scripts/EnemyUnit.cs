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
		public AudioClip deathSound;

		protected override void Start ()
		{
				base.Start ();
		}

		protected override void Update ()
		{
				base.Update ();
				if (currentHealth <= 0) {
						Debug.Log ("Before");
						ParticleSystem temp = (ParticleSystem)Instantiate (deathExplosion, transform.position, transform.rotation);
						Debug.Log ("After");
						AudioSource tempSound = PlayClipAt (deathSound, transform.position);
						Destroy (gameObject.rigidbody);
						Destroy (gameObject);
						Destroy (temp.gameObject, 5);
						control.currentEnemies--;
				}

				currentTarget = FindObjectOfType<AllyUnit> ();
				if (currentTarget) {
						transform.LookAt (currentTarget.transform);
						if (Vector3.Distance (transform.position, currentTarget.transform.position) < maxDist) {
								agent.Stop ();
						} else {
								agent.SetDestination (currentTarget.transform.position);
						}

						if (Vector3.Distance (transform.position, currentTarget.transform.position) < attackRange && lastAttack > attackRate) {
								Rigidbody clone;
								clone = (Rigidbody)Instantiate (projectile, transform.position + offset, transform.rotation);
								unitSound.PlayOneShot (fireSound, 0.1f);
								clone.velocity = transform.TransformDirection (Vector3.forward * velocity) + new Vector3 (Time.deltaTime * velocity, 0, 0);
								lastAttack = 0;
								Destroy (clone, delay);
								Destroy (clone.gameObject, delay);
						}
				}
				lastAttack += Time.deltaTime;
		}

		AudioSource PlayClipAt (AudioClip clip, Vector3 pos)
		{
				GameObject tempGO = new GameObject ("TempAudio " + clip.name);
				tempGO.transform.position = pos;
				AudioSource aSource = tempGO.AddComponent<AudioSource> ();
				aSource.clip = deathSound;
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

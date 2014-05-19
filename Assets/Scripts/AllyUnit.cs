using UnityEngine;

public class AllyUnit : baseUnit
{
		public Vector3 targetLocation;
		public float lastAttack = 0;
		public Rigidbody projectile;
		public Vector3 offset;
		public float velocity = 100;
		public int delay = 3;
		public AudioSource unitSound;
		public AudioClip fireSound;
		public AudioClip deathSound;
		public bool canMove;
		public int energy;
		public int currentEnergy;
		public Vector3 cpos, ppos;

		protected override void Start ()
		{
				base.Start ();
		}

		protected override void Update ()
		{
				if (currentHealth <= 0) {
						ParticleSystem temp = Instantiate (deathExplosion, transform.position, transform.rotation) as ParticleSystem;
						AudioSource tempSound = PlayClipAt (deathSound, transform.position);
						Destroy (gameObject.rigidbody);
						Destroy (gameObject);
						Destroy (temp.gameObject, 5);
				}
				if (ppos != transform.position && canMove) {
						currentEnergy--;
				}

				if (ppos != transform.position && currentEnergy <= 0) {
						currentEnergy = 0;
						agent.SetDestination (rigidbody.position);
				}
				ppos = transform.position;
				if (isClicked) {
						print (gameObject.name + " is active: " + gameObject.activeSelf);
						renderer.material = onHoverMaterial;
						if (canMove && currentEnergy > 0) {
								moveUnit ();
						}
				} else {
						renderer.material = defaultMaterial;
				}

				if (!currentTarget) {
						currentTarget = FindObjectOfType<EnemyUnit> ();	
				}
				
				// possible for above to not find an enemy unit
				if (currentTarget) {
						transform.LookAt (currentTarget.transform);
						if (Vector3.Distance (transform.position, currentTarget.transform.position) <= attackRange && lastAttack >= attackRate) {

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

		void moveUnit ()
		{
				if (updateRightClick) {
						Debug.Log ("moving unit");
						agent.SetDestination (destinationVector);
						updateRightClick = false;
				}
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

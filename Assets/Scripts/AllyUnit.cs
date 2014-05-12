using UnityEngine;

public class AllyUnit : baseUnit
{
		public Vector3 targetLocation;
		public float lastAttack = 0f;
		public Rigidbody projectile;
		public Vector3 offset;
		public float velocity = 100;
		public int delay = 3;
		public AudioSource unitSound;
		public AudioClip fireSound;
		public bool canMove;
		public int energy;
		public int currentEnergy;
		public Vector3 cpos, ppos;

		void Start ()
		{
				base.Start ();
		}

		void Update ()
		{
				if (currentHealth <= 0) {
						Destroy (gameObject.rigidbody);
						Destroy (gameObject);
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
	
				transform.LookAt (currentTarget.transform);
				if (Vector3.Distance (transform.position, currentTarget.transform.position) <= attackRange && lastAttack >= attackRate) {
    	
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

		void moveUnit ()
		{
				if (updateRightClick) {
						Debug.Log ("moving unit");
						agent.SetDestination (destinationVector);
						updateRightClick = false;
				}
		}
}

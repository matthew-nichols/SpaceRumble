using UnityEngine;

public class AllyUnit : baseUnit
{
		public Vector3 targetLocation;
		public float lastAttack = 0;
		public Rigidbody projectile;
		public Vector3 offset;
		public float velocity = 100;
		public int delay = 3;
		public bool canMove;
		public float energy;
		public float currentEnergy;
		public Vector3 cpos, ppos;
		//public Weapon weapon;
		//public Armor armor;
		//public Accessory accessory;
		public Secondary secondary;
		public mainSlot mainslot;
		public int index;
		public AudioSource unitSound;
		public AudioClip fireSound;
		public AudioClip deathSound;
		public AudioClip selectSound;
		public AudioClip moveSound;
		private bool playSound;

		protected override void Start ()
		{

				playSound = true;
				if (mainslot) {
<<<<<<< HEAD
						health = mainslot.healthBoost;
                        health += secondary.healthBoost;
                        currentHealth = health;
                }				
=======
						health = mainslot.healthBoost + secondary.healthBoost;
						currentHealth = health;
				}				
>>>>>>> 4135980dd17e6d86867333a28eacf50d8408d4dd
				if (mainslot) {
						attackDmg = mainslot.damage;
				}
		}

		void SetInfo (AllyUnitStats a)
		{
				/*targetLocation = a.targetLocation;
		        lastAttack = a.lastAttack;
		        projectile = a.projectile;
		        offset= a.offset;
		        velocity = a.velocity;
		        delay = a.delay;
		        unitSound = a.unitSound;
		        fireSound = a.fireSound;
		        deathSound = a.deathSound;
		        canMove = a.canMove;*/
				energy = a.energy;
				currentEnergy = a.currentEnergy;
				// cpos = a.cpos;
				// ppos = a.ppos;
				mainslot = a.mainslot;
				//weapon = a.weapon;
				//armor = a.armor;
				//accessory = a.accessory;
				secondary = a.secondary;
				//health = a.health;
				currentHealth = a.currentHealth;
				attackDmg = a.attackDmg;
				attackRange = a.attackRange;
				attackRate = a.attackRate;
				UnitName = a.UnitName;
		}

		void GetInfo (AllyUnitStats a)
		{
				/* a.targetLocation = targetLocation;
            a.lastAttack = lastAttack;
            a.projectile = projectile;
            a.offset = offset;
            a.velocity = velocity;
            a.delay = delay;
            a.unitSound = unitSound;
            a.fireSound = fireSound;
            a.deathSound = deathSound;
            a.canMove = canMove;*/
				a.energy = energy;
				a.currentEnergy = currentEnergy;
				//   a.cpos = cpos;
				//  a.ppos = ppos;
				//a.weapon = weapon;
				//a.armor = armor;
				//a.accessory = accessory;
				a.secondary = secondary;
				a.health = health;
				a.currentHealth = currentHealth;
				a.attackDmg = attackDmg;
				a.attackRange = attackRange;
				a.attackRate = attackRate;
				a.UnitName = UnitName; 
		}

        protected override void Update()
        {
            if (currentHealth <= 0)
            {
                if (deathExplosion)
                {
                    ParticleSystem temp = Instantiate(deathExplosion, transform.position, transform.rotation) as ParticleSystem;
                    Destroy(temp.gameObject, 5);
                }
                if (deathSound)
                {
                    PlayClipAt(deathSound, transform.position);
                }

                Destroy(gameObject.rigidbody);
                Destroy(gameObject);
                OnDeath();

            }
            if (ppos != transform.position && canMove)
            {
                currentEnergy -= 20 * Time.deltaTime; // TODO
            }

            if (ppos != transform.position && currentEnergy <= 0)
            {
                currentEnergy = 0;
                agent.SetDestination(rigidbody.position);
            }
            ppos = transform.position;
            if (isClicked)
            {
                if (playSound)
                {
                    unitSound.PlayOneShot(selectSound, unitSound.volume);
                    playSound = false;
                }

                if (renderer)
                    renderer.material = onHoverMaterial;
                if (canMove && currentEnergy > 0)
                {
                    moveUnit();
                }
            }
            else
            {
                renderer.material = defaultMaterial;
                playSound = true;
            }
            if (!canMove)
            {
                EnemyUnit[] allTargets = FindObjectsOfType<EnemyUnit>();
                float dist = float.PositiveInfinity;
                foreach (EnemyUnit u in allTargets)
                {
                    float d = Vector3.Distance(u.transform.position, transform.position);
                    if (d < dist)
                    {
                        dist = d;
                        currentTarget = u;
                    }
                }

                // possible for above to not find an enemy unit
                if (currentTarget)
                {
                    transform.LookAt(currentTarget.transform);
                    if (Vector3.Distance(transform.position, currentTarget.transform.position) <= attackRange && lastAttack >= attackRate)
                    {

                        Rigidbody clone = Instantiate(projectile, transform.position + offset, transform.rotation) as Rigidbody;
                        clone.SendMessage("updateDmg", attackDmg);
                        unitSound.PlayOneShot(fireSound, 0.1f);
                        clone.velocity = transform.TransformDirection(Vector3.forward * velocity) + new Vector3(Time.deltaTime * velocity, 0, 0);

                        lastAttack = 0;
                        Destroy(clone, delay);
                        Destroy(clone.gameObject, delay);
                    }
                }
                lastAttack += Time.deltaTime;
            }
        }

		void moveUnit ()
		{
				if (updateRightClick) {
						unitSound.PlayOneShot (moveSound, unitSound.volume);
						agent.SetDestination (destinationVector);
						updateRightClick = false;
				}
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

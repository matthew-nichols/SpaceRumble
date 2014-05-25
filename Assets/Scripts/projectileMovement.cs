using UnityEngine;

public class projectileMovement : MonoBehaviour
{
		public float projectileSpeed;
		public float range = 10;
		public float dist;
		public int dmg = 10;
		public int maxTime = 10;
		public float time;
		public float impactVolume = 0.5f;
		public ParticleSystem impactEffect;
		public AudioSource projectileSound;
		public AudioClip impactSound;
		void Start ()
		{
	
		}
        void updateDmg(int d)
        {
            dmg = d;
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
						ContactPoint contact = other.contacts [0];
						Vector3 pos = contact.point;
						Quaternion rot = Quaternion.FromToRotation (Vector3.up, contact.normal); 
						ParticleSystem temp = Instantiate (impactEffect, pos, rot) as ParticleSystem;
						PlayClipAt(impactSound, pos, impactVolume);
						Destroy (gameObject);
						Destroy (temp.gameObject, 1);
						baseUnit unit = other.gameObject.GetComponent<baseUnit> ();
						if (unit)
								unit.currentHealth -= dmg;
				}
		}
		AudioSource PlayClipAt (AudioClip clip, Vector3 pos, float volume)
		{
			GameObject tempGO = new GameObject ("TempAudio " + clip.name);
			tempGO.transform.position = pos;
			AudioSource aSource = tempGO.AddComponent<AudioSource> ();
			aSource.clip = clip;
			aSource.rolloffMode = projectileSound.rolloffMode;
			aSource.pitch = projectileSound.pitch;
			aSource.minDistance = projectileSound.minDistance;
			aSource.maxDistance = projectileSound.maxDistance;
			aSource.dopplerLevel = projectileSound.dopplerLevel;
			aSource.volume = volume;
			aSource.Play ();
			Destroy (tempGO, clip.length);
			return aSource;
		}
}

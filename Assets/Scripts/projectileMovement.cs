using UnityEngine;

public class projectileMovement : MonoBehaviour
{
		public float projectileSpeed;
		public float range = 10;
		public float dist;
		public int dmg = 10;
		public int maxTime = 10;
		public float time;

		void Start ()
		{
	
		}

		void Update ()
		{
				float amntToMove = projectileSpeed * Time.deltaTime;
				transform.Translate (Vector3.forward * amntToMove);
				;
				time += Time.deltaTime;
				if (time > maxTime) {
						Destroy (gameObject);
				}
		}

		void OnCollisionEnter (Collision other)
		{
				if (other.gameObject.tag == "Enemy") {
						Destroy (gameObject);
						other.gameObject.GetComponent<baseUnit> ().currentHealth -= 10; 
				}
		}
}

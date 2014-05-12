using UnityEngine;

public class createRocket : MonoBehaviour
{
		public Rigidbody projectile;
		public float velocity = 100;
		public Vector3 offset;
		public string FireButton = "E";
		public int delay = 3;

		void Update ()
		{
				if (Input.GetButtonDown (FireButton)) {
						Instantiate (projectile, transform.position + offset, transform.rotation);
				}
		}
}

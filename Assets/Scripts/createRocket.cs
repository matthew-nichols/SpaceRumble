using UnityEngine;
using System.Collections;

public class createRocket : MonoBehaviour {
	public Rigidbody projectile;
	public float velocity = 100;
	public Vector3 offset;
	public string FireButton = "E";
	public int delay = 3;

	void Update() {
        if (Input.GetButtonDown(FireButton))
        {
            Instantiate(projectile, transform.position + offset, transform.rotation);
        }
	}
	/*
	void OnCollisionEnter(Collision collision) {
		ContactPoint contact = collision.contacts[0];
		//Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		//Vector3 pos = contact.point;
		//Instantiate(explosionPrefab, pos, rot) as Transform;
		Destroy(gameObject);
	}
	*/
}

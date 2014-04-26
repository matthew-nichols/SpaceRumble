using UnityEngine;
using System.Collections;

public class projectileMovement : MonoBehaviour {
    public float projectileSpeed;
    public float range = 10;
    public float dist;
    public int dmg = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float amntToMove = projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * amntToMove);
        //dist += Time.deltaTime * projectileSpeed;
	}
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            ((baseUnit)other.gameObject.GetComponent("baseUnit")).currentHealth -= 10; 
        }

    }
}

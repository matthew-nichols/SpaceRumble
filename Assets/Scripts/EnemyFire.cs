using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour
{

    public float fireRate;
    public int damage;
    public int range;
    public AllyUnit target;


    void Update()
    {

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

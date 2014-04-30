using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour
{

/*
    public float fireRate;
    public int damage;
    public int range;
    public AllyUnit target;
*/

    public GameObject EnemyProjectile;
    public GameObject allyUnit;
    public GameObject enemyUnit;
    public float reloadTime = 1f;
    public float turnSpeed = 5f;
    public float firePauseTime = .25f;
    public Transform myTarget;
    public Transform turret;//turretball
    public Transform[] muzzlePositions;
    public float range = 10000;

    private double nextFireTime;
    private float nextMoveTime;
    private Quaternion desiredRotation;


    void Update()
    {


    }


/*
    }
    
    void OnCollisionEnter(Collision collision) {
        ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;
        //Instantiate(explosionPrefab, pos, rot) as Transform;
        Destroy(gameObject);
    }
    */
}


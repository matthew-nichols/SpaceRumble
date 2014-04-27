using UnityEngine;
using System.Collections;

public class enemyProjectiles : MonoBehaviour
{
    public float projectileSpeed;
    public float range = 10;
    public float dist;
    public int dmg = 10;
    public int maxTime = 10;
    private float time;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float amntToMove = projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * amntToMove);
        //dist += Time.deltaTime * projectileSpeed;
        time += Time.deltaTime;
        if (time > maxTime)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            ((baseUnit)other.gameObject.GetComponent("baseUnit")).currentHealth -= dmg;
        }


    }
}

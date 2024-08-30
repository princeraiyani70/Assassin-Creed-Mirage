using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float grenadeTimer = 3f;
    public float radius = 10f;
    float countDown;
    public float giveDamage = 120f;
    bool hasExploded = false;
    public GameObject explosionEffect;

    private void Start()
    {
        countDown = grenadeTimer;
    }

    private void Update()
    {
        countDown -= Time.deltaTime;

        if (countDown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        //Debug.Log("Grenade Exploded");
        //Show Effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Get Nearby Objects
        Collider[] colliders= Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            //Add Force

            //Damage
            KnightAi knightAi =nearbyObject.GetComponent<KnightAi>();
            KnightAi2 knightAi2 = nearbyObject.GetComponent<KnightAi2>();

            if (knightAi != null)
            {
                knightAi.TakeDamage(giveDamage);
            }

            if (knightAi2 != null)
            {
                knightAi2.TakeDamage(giveDamage);
            }
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAi : MonoBehaviour
{
    [Header("Character Info")]
    public float movingSpeed;
    public float runningSpeed;
    public float CurrentmovingSpeed;
    public float turningSpeed = 300f;
    public float stopSpeed = 1f;
    public float maxHealth = 120f;
    public float currenthealth;

    [Header("Destination Var")]
    public Vector3 destination;
    public bool destinationReached;


    private void Start()
    {
        CurrentmovingSpeed = movingSpeed;
        currenthealth = maxHealth;
    }

    private void Update()
    {
        Walk();
    }

    public void Walk()
    {
        CurrentmovingSpeed = movingSpeed;

        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance > stopSpeed)
            {
                //Turning
                destinationReached = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);

                //Moving AI
                transform.Translate(Vector3.forward * CurrentmovingSpeed * Time.deltaTime);
            }
            else
            {
                destinationReached = true;
            }
        }
    }

    public void LocateDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationReached = false;
    }

    public void TakeDamage(float amount)
    {
        currenthealth-=amount;

        if (currenthealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

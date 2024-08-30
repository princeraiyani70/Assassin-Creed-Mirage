using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Transform shootingArea;
    public float giveDamage = 10f;
    public float shootingRange = 100f;
    public Animator animator;
    public bool isMoving;
    public PlayerScript playerScript;

    [Header("Rifle Ammunition And Reloading")]
    private int maximumAmmunition = 1;
    public int presentAmmunition;
    public int mag;
    public float reloadingTime;
    private bool setReloading;
    public GameObject crossHair;

    private void Start()
    {
        presentAmmunition = maximumAmmunition;
    }

    private void Update()
    {
        if (animator.GetFloat("movementValue") > 0.001f)
        {
            isMoving = true;
        }

        else if (animator.GetFloat("movementValue") < 0.999999f)
        {
            isMoving = false;
        }

        if (setReloading)
            return;

        if (presentAmmunition <= 0 && mag > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Attack") && isMoving==false)
        {
            animator.SetBool("RifleActive", true);
            animator.SetBool("Shooting", true);
            Shoot();
        }
        else if (!CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            animator.SetBool("Shooting", false);
        }

        if (Input.GetMouseButton(1))
        {
            animator.SetBool("RifleAim", true);
            //crossHair.SetActive(true);
        }

        else if (!Input.GetMouseButton(1))
        {
            animator.SetBool("RifleAim", false);
            //crossHair.SetActive(false);
        }
    }

    void Shoot()
    {
        if (mag <= 0)
        {
            return;
        }

        presentAmmunition--;

        if (presentAmmunition == 0)
        {
            mag--;
        }

        RaycastHit hitInfo;

        if (Physics.Raycast(shootingArea.position, shootingArea.forward, out hitInfo, shootingRange))
        {
            KnightAi knightAi = hitInfo.transform.GetComponent<KnightAi>();
            KnightAi2 knightAi2 = hitInfo.transform.GetComponent<KnightAi2>();

            if (knightAi != null)
            {
                knightAi.TakeDamage(giveDamage);
            }

            if (knightAi2 != null)
            {
                knightAi2.TakeDamage(giveDamage);
            }
        }
    }

    IEnumerator Reload()
    {
        setReloading = true;
        animator.SetFloat("movementValue", 0f);
        playerScript.movementSpeed = 0f;
        animator.SetBool("ReloadRifle", true);
        //reloading Anim
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("ReloadRifle", false);
        presentAmmunition = maximumAmmunition;
        setReloading = false;
        animator.SetFloat("movementValue", 0f);
        playerScript.movementSpeed = 5f;
    }
}

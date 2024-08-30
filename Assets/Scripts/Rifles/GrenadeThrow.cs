using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GrenadeThrow : MonoBehaviour
{
    public float throwForce = 10f;
    public Transform grenadeArea;
    public GameObject grenadePrefab;
    public Animator anim;

    public GameManager GM;

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Attack") && GM.numberOfgrenades>0)
        {
            //function
            StartCoroutine(GrenadeAnim());
            GM.numberOfgrenades -= 1;
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, grenadeArea.transform.position, grenadeArea.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(grenadeArea.transform.forward * throwForce, ForceMode.VelocityChange);

    }

    IEnumerator GrenadeAnim()
    {
        anim.SetBool("GradedInAir", true);
        yield return new WaitForSeconds(0.5f);
        ThrowGrenade();
        yield return new WaitForSeconds(1f);
        anim.SetBool("GradedInAir", false);
    }
}

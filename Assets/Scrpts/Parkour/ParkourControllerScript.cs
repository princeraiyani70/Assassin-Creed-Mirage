using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScript : MonoBehaviour
{
    public Enviromentcheaker enviromentCheaker;
    bool playerInAction;
    public Animator animator;


    private void Update()
    {
        if (Input.GetButton("Jump")  && !playerInAction)
        {
            var hitData = enviromentCheaker.CheckObstacle();

            if (hitData.hitFound)
            {
                //Debug.Log("Objcet Founded" + hitData.hitInfo.transform.name);
                StartCoroutine(PerformParkourAction());
            }
        }
    }

    IEnumerator PerformParkourAction()
    {
        playerInAction = true;

        animator.CrossFade("JumpUp", 0.2f);
        yield return null;

        var animationState = animator.GetNextAnimatorStateInfo(0);

        yield return new WaitForSeconds(animationState.length);

        playerInAction = false;
    }
}

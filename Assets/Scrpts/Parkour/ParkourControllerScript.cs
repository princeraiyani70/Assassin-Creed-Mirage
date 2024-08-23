using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScript : MonoBehaviour
{
    public Enviromentcheaker enviromentCheaker;
    bool playerInAction;
    public Animator animator;
    public PlayerScript playerScript;

    [Header("Parkour Action Area")]
    public List<NewParkourAction> newParkourActions;

    private void Update()
    {
        if (Input.GetButton("Jump") && !playerInAction)
        {
            var hitData = enviromentCheaker.CheckObstacle();

            if (hitData.hitFound)
            {
                //Debug.Log("Objcet Founded" + hitData.hitInfo.transform.name);

                foreach (var action in newParkourActions)
                {
                    if (action.CheckIfAvailable(hitData, transform))
                    {
                        StartCoroutine(PerformParkourAction(action));
                        break;
                    }
                }
            }
        }
    }

    IEnumerator PerformParkourAction(NewParkourAction action)
    {
        playerInAction = true;
        playerScript.SetControl(false);

        animator.CrossFade(action.AnimationName, 0.2f);
        yield return null;

        var animationState = animator.GetNextAnimatorStateInfo(0);
        if (!animationState.IsName(action.AnimationName))
        {
            Debug.Log("Animation Name Is Incorrect");
        }


        float timerCounter = 0f;

        while (timerCounter <= animationState.length)
        {
            timerCounter += Time.deltaTime;

            if (action.LookAtObstacle)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, action.RequiredRotation, playerScript.rotSpeed * Time.deltaTime);

            }

            if (action.AllowTargetMatching)
            {
                CompareTarget(action);
            }

            if (animator.IsInTransition(0) && timerCounter > 0.5f)
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(action.ParloutActionDelay);

        playerScript.SetControl(true);
        playerInAction = false;
    }

    void CompareTarget(NewParkourAction action)
    {
        animator.MatchTarget(action.ComparePosition, transform.rotation, action.CompareBodyPart, new MatchTargetWeightMask(action.ComparePositionWeight,0), action.CompareStartTime, action.CompareEndTime);
    }
}

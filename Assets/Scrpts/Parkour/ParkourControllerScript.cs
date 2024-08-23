using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScript : MonoBehaviour
{
    public Enviromentcheaker enviromentCheaker;
    bool playerInAction;
    public Animator animator;
    public PlayerScript playerScript;
    [SerializeField] NewParkourAction jumpDownParkourAction;
    float autoJumpHeightLimit = 2f;

    [Header("Parkour Action Area")]
    public List<NewParkourAction> newParkourActions;

    private void Update()
    {

        var hitData = enviromentCheaker.CheckObstacle();
        if (Input.GetButton("Jump") && !playerInAction)
        {

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

        if (playerScript.playerOnLedge && !playerInAction && !hitData.hitFound)
        {
            bool canJump = true;
            if (playerScript.LedgeInfo.height > autoJumpHeightLimit && !Input.GetButton("Jump"))
            {
                canJump = false;
            }
            if (canJump && playerScript.LedgeInfo.angle <= 90)
            {
                playerScript.playerOnLedge = false;
                StartCoroutine(PerformParkourAction(jumpDownParkourAction));

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
        animator.MatchTarget(action.ComparePosition, transform.rotation, action.CompareBodyPart, new MatchTargetWeightMask(action.ComparePositionWeight, 0), action.CompareStartTime, action.CompareEndTime);
    }
}

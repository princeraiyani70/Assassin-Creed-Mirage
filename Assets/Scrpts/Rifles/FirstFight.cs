using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFight : MonoBehaviour
{
    public float Timer = 0f;
    public int FistFightVal;
    public Animator anim;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            Timer += Time.deltaTime;
        }
        else
        {
            anim.SetBool("FistFightActive", true);
        }

        if (Timer > 5f)
        {
            anim.SetBool("FistFightActive", false);
        }

        FistFightModes();
    }

    void FistFightModes()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FistFightVal = Random.Range(1, 7);

            if (FistFightVal == 1)
            {
                //Attack
                //Animation
                StartCoroutine(SingleFist());
            }

            if (FistFightVal == 2)
            {
                StartCoroutine(DoubleFist());
            }

            if (FistFightVal == 3)
            {
                StartCoroutine(FirstFistKick());
            }

            if (FistFightVal == 4)
            {
                StartCoroutine(KickCombo());
            }

            if (FistFightVal == 5)
            {
                StartCoroutine(LeftKick());
            }
        }
    }

    IEnumerator SingleFist()
    {
        anim.SetBool("SingleFist", true);
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("SingleFist", false);
    }

    IEnumerator DoubleFist()
    {
        anim.SetBool("DoubleFist", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("DoubleFist", false);
    }

    IEnumerator FirstFistKick()
    {
        anim.SetBool("FirstFistKick", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("FirstFistKick", false);
    }

    IEnumerator KickCombo()
    {
        anim.SetBool("KickCombo", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("KickCombo", false);
    }

    IEnumerator LeftKick()
    {
        anim.SetBool("LeftKick", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("LeftKick", false);
    }
}

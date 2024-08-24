using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFight : MonoBehaviour
{
    public float Timer = 0f;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            Timer += Time.deltaTime;
        }
        else
        {
            Debug.Log("First Fight Mode ON");
        }

        if (Timer > 5f)
        {
            Debug.Log("First Fight Mode Off");
        }
    }
}

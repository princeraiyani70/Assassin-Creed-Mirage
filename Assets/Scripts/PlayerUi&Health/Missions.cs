using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Missions : MonoBehaviour
{
    public bool Mission1;
    public bool Mission2;
    public bool Mission3;

    public TextMeshProUGUI missionText;
    public GameObject missionArea;
    public GameObject showButton;

    public static Missions instance;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Show"))
        {
            StartCoroutine(ShowMissions());
        }

        if (Mission1 == false && Mission2 == false && Mission3==false)
        {
            missionText.text = "Locate Rifle And Items";
        }

        if (Mission1 == true && Mission2 == false && Mission3 == false)
        {
            missionText.text = "Locate Knight";
        }

        if (Mission1 == true && Mission2 == true && Mission3 == false)
        {
            missionText.text = "Fight Knight";
        }

        if (Mission1 == true && Mission2 == true && Mission3 == true)
        {
            missionText.text = "Missions Completed";
        }
    }

    IEnumerator ShowMissions()
    {
        showButton.SetActive(false);
        missionArea.SetActive(true);
        yield return new WaitForSeconds(2f);
        missionArea.SetActive(false);
        showButton.SetActive(true);
    }
}

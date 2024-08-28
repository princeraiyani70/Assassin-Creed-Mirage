using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Missions : MonoBehaviour
{
    public bool Mission1;
    public bool Mission2;
    public bool Mission3;

    public TextMeshProUGUI missionText;

    public static Missions instance;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
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
}

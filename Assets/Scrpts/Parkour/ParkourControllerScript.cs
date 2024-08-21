using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScript : MonoBehaviour
{
    public Enviromentcheaker enviromentCheaker;

    private void Update()
    {
        var hitData = enviromentCheaker.CheckObstacle();

        if (hitData.hitFound)
        {
            Debug.Log("Objcet Founded" + hitData.hitInfo.transform.name);
        }
    }
}

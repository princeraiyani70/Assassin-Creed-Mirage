using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviromentcheaker : MonoBehaviour
{
    public Vector3 rayOffset = new Vector3(0, 0.2f, 0);
    public float rayLength = 0.9f;
    public float heightRayLength = 6f;
    public LayerMask obtacleLayer;

    public ObstaceInfo CheckObstacle()
    {
        var hitData = new ObstaceInfo();

        var rayOrigin = transform.position + rayOffset;
        hitData.hitFound = Physics.Raycast(rayOrigin, transform.forward, out hitData.hitInfo, rayLength, obtacleLayer);

        Debug.DrawRay(rayOrigin, transform.forward * rayLength, (hitData.hitFound) ? Color.red : Color.green);

        if (hitData.hitFound)
        {
            var heightOrigin = hitData.hitInfo.point + Vector3.up * heightRayLength;
            hitData.hitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.heightInfo, heightRayLength, obtacleLayer);

            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitData.heightHitFound) ? Color.blue : Color.green);
        }

        return hitData;
    }
}

public struct ObstaceInfo
{
    public bool hitFound;
    public bool heightHitFound;
    public RaycastHit hitInfo;
    public RaycastHit heightInfo;
}

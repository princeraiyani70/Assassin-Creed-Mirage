using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviromentcheaker : MonoBehaviour
{
    public Vector3 rayOffset = new Vector3(0, 0.2f, 0);
    public float rayLength = 0.9f;
    public float heightRayLength = 6f;
    public LayerMask obtacleLayer;


    [Header("Check Ledges")]
    [SerializeField] float ledgeEatLength = 11f;
    [SerializeField] float ledgeRayHeightThreshold = 0.76f;

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

    public bool CheckLedge(Vector3 movementDirection,out LedgeInfo ledgeInfo)
    {
        ledgeInfo = new LedgeInfo();

        if (movementDirection == Vector3.zero)
            return false;

        float ledgeOriginOffset = 0.5f;
        var ledgeOrigin = transform.position + movementDirection * ledgeOriginOffset + Vector3.up;

        if (Physics.Raycast(ledgeOrigin, Vector3.down, out RaycastHit hit, ledgeRayHeightThreshold, obtacleLayer))
        {
            Debug.DrawRay(ledgeOrigin, Vector3.down * ledgeEatLength, Color.blue);

            var surfaceRaycastOrigin = transform.position + movementDirection - new Vector3(0, 0.1f, 0);
            if (Physics.Raycast(surfaceRaycastOrigin, -movementDirection, out RaycastHit surfaceHit, 2, obtacleLayer))
            {
                float Ledgeheight = transform.position.y - hit.point.y;

                if (Ledgeheight > ledgeRayHeightThreshold)
                {
                    ledgeInfo.angle = Vector3.Angle(transform.forward, surfaceHit.normal);
                    ledgeInfo.height = Ledgeheight;
                    ledgeInfo.surfaceHit = surfaceHit;
                    return true;
                }
            }
        }

        return false;
    }
}


public struct ObstaceInfo
{
    public bool hitFound;
    public bool heightHitFound;
    public RaycastHit hitInfo;
    public RaycastHit heightInfo;
}

public struct LedgeInfo
{
    public float angle;
    public float height;
    public RaycastHit surfaceHit;
}
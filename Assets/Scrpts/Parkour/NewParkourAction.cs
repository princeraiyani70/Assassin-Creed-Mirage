using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Parkour Menu/Create New Parkour Action")]
public class NewParkourAction : ScriptableObject
{
    [Header("Checking Obstacle Height")]
    [SerializeField] string animationName;
    [SerializeField] string BarrierTag;
    [SerializeField] float minimumHeight;
    [SerializeField] float maximumHeight;

    [Header("Rotating Player Towars Obstacle")]
    [SerializeField] bool lookAtObstacle;
    public Quaternion RequiredRotation { get; set; }

    [Header("Target Matching")]
    [SerializeField] bool allowTargetMatching = true;
    [SerializeField] AvatarTarget compareBodyPart;
    [SerializeField] float compareStartTime;
    [SerializeField] float compareEndTime;
    public Vector3 ComparePosition { get; set; }
    [SerializeField] Vector3 comparePositionWeight = new Vector3(0, 1, 0);


    public bool CheckIfAvailable(ObstaceInfo hitData, Transform player)
    {
        if (!string.IsNullOrEmpty(BarrierTag)&& hitData.hitInfo.transform.tag!=BarrierTag)
        {
            return false;
        }

        float checkHeight = hitData.heightInfo.point.y - player.position.y;
        if (checkHeight < minimumHeight || checkHeight > maximumHeight)
        {
            return false;
        }

        if (lookAtObstacle)
        {
            RequiredRotation = Quaternion.LookRotation(-hitData.hitInfo.normal);
        }

        if (allowTargetMatching)
        {
            ComparePosition = hitData.heightInfo.point;
        }

        return true;
    }

    public string AnimationName => animationName;
    public bool LookAtObstacle => lookAtObstacle;

    public bool AllowTargetMatching=>allowTargetMatching;
    public AvatarTarget CompareBodyPart=> compareBodyPart;

    public float CompareStartTime=>compareStartTime;
    public float CompareEndTime=> compareEndTime;
    public Vector3 ComparePositionWeight => comparePositionWeight;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Parkour Menu/Create New Parkour Action")]
public class NewParkourAction : ScriptableObject
{
    [SerializeField] string animationName;
    [SerializeField] float minimumHeight;
    [SerializeField] float maximumHeight;
    [SerializeField] bool lookAtObstacle;

    public Quaternion RequiredRotation {  get; set; }


    public bool CheckIfAvailable(ObstaceInfo hitData, Transform player)
    {
        float checkHeight = hitData.heightInfo.point.y - player.position.y;

        if (checkHeight < minimumHeight || checkHeight > maximumHeight)
            return false;

        if (lookAtObstacle)
        {
            RequiredRotation = Quaternion.LookRotation(-hitData.hitInfo.normal);
        }

        return true;
    }

    public string AnimationName => animationName;
    public bool LookAtObstacle => lookAtObstacle;
}

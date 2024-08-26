using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [Header("Ai Character")]
    CharacterNavigationScript character;
    public Waypoint currentWaypoint;

    private void Awake()
    {
        character = GetComponent<CharacterNavigationScript>();
    }

    private void Start()
    {
        character.LocateDestination(currentWaypoint.GetPosition());
    }

    private void Update()
    {
        if (character.destinationReached)
        {
            currentWaypoint = currentWaypoint.nextWaypoint;
            character.LocateDestination(currentWaypoint.GetPosition());
        }
    }
}

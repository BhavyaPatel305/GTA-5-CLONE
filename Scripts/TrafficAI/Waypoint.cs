using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Implementing Traffic AI

    //Way it works:
    // Say here is AI char and way points(1,2,3,etc are way points):
    // Player      1        2       3       4       5
    //            10       9        8         7        6 
    // what will happen is : In beginning: next way point = 1, so player will look for 1 and go there
    // Next when it reaches 1, this Way point script will be attached to 1 so from there it will get it's next way point
    // which is 2, so it will go to 2 and its previous way point will become 1
    // and so on, it will keep on going to next way point and previous way point will keep on changing
    // at the end when it reaches 10, its next way point will become 1 again, so in that way it will keep moving
    [Header("Waypoint Status")]
    // Reference to previous waypoint
    public Waypoint previousWaypoint;
    // Reference to next waypoint
    public Waypoint nextWaypoint;

    // way point width
    [Range(0f, 5f)]
    public float waypointWidth = 1f;

    // In this method, we will be getting random waypoints
    public Vector3 GetPosition(){
        // Give space between way points for the AI character to walk
        Vector3 minBound = transform.position + transform.right * waypointWidth / 2f;
        Vector3 maxBound = transform.position - transform.right * waypointWidth / 2f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }
}

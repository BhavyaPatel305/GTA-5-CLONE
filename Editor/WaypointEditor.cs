using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[InitializeOnLoad()]
public class WaypointEditor : Editor
{
    // Visualize way points
    // Using onDrawScentGizmos() as we only want to see gizmos only in Scene window and not in the game window
    // Adding gizmos attribute: [DrawGizmo(Gizmo.NonSelected | Gizmo.Selected | GizmoType.Pickable)]
    // We are just telling unity to draw a gizmo because wheater or not the game object is selected or not and also pickable or not
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmos(Waypoint waypoint, GizmoType gizmoType){
        if((gizmoType & GizmoType.Selected) != 0){
            // The highlight that way point
            Gizmos.color = Color.blue;
        }else{
            // if not selected, the we want color to be little bit faded
            Gizmos.color = Color.blue * 0.5f;
        }
        // Draw a circle around way point of size 0.1f
        Gizmos.DrawSphere(waypoint.transform.position, 0.1f);

        // Draw a white line for way point width
        Gizmos.color = Color.white;
        Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.waypointWidth/2f), (waypoint.transform.position - (waypoint.transform.right * waypoint.waypointWidth/2f)));

        // Connect waypoints
        // Drawing line from previous to next way point and from next way point to previous waypoint
        if(waypoint.previousWaypoint != null){
            // If previous way point is not null then

            // This will color line in red color
            Gizmos.color = Color.red;
            // Here we take current way point position and then 
            Vector3 offset = waypoint.transform.right * waypoint.waypointWidth/2f;
            // Here we take previous way point position
            Vector3 offsetTo = waypoint.previousWaypoint.transform.right * waypoint.previousWaypoint.waypointWidth/2f;
            // From current way point to previous way point, we created a line(of red color)
            Gizmos.DrawLine(waypoint.transform.position + offset,waypoint.previousWaypoint.transform.position + offsetTo);
        }

        // Connect waypoints
        // Drawing line from previous to next way point and from next way point to previous waypoint
        if(waypoint.nextWaypoint != null){
            // If next way point is not null then

            // This will color line in green color
            Gizmos.color = Color.green;
            // Drawing line from current waypoint to next way point

            // Here we take current way point position and then, (NOTICE: Here we are using -ve sign as we cant use .left key word)
            Vector3 offset = waypoint.transform.right * -waypoint.waypointWidth/2f;
            // Here we take previous way point position(NOTICE: Here we are using -ve sign as we cant use .left key word)
            Vector3 offsetTo = waypoint.nextWaypoint.transform.right * -waypoint.nextWaypoint.waypointWidth/2f;
            // From current way point to previous way point, we created a line(of red color)
            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.nextWaypoint.transform.position + offsetTo);
        }
    }
}

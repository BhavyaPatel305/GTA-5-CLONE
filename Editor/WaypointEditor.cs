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
    }
}

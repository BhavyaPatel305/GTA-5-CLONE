using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// This will be way point editor
public class WaypointManagerWindow : EditorWindow
{
    [MenuItem("Waypoint/Waypoints Editor Tools")]
    public static void ShowWindow(){
        GetWindow<WaypointManagerWindow>("Waypoints Editor Tools");
    }

    // This reference will be used as parent of all the way points
    public Transform waypointOrigin;

    private void OnGUI(){
        // This serialized object will for for our window so we can easily draw it's properties
        SerializedObject obj = new SerializedObject(this);

        // Property for Way point origin
        EditorGUILayout.PropertyField(obj.FindProperty("waypointOrigin"));

        // Check if way point origin is assigned or not(If not display a message)
        if(waypointOrigin == null){
            EditorGUILayout.HelpBox("Please assign a waypoint origin transform.", MessageType.Warning);
        }// If way point origin is assigned, then show some buttons
        // Those buttons will be for creating the way point
        else{
            // Show buttons in vertical direction
            EditorGUILayout.BeginVertical("box");
            // Create Buttons method called
            CreateButtons();
            EditorGUILayout.EndVertical();
        }

        // Apply the modified properties
        obj.ApplyModifiedProperties();
    }

    // Create all the buttons
    void CreateButtons(){
        // 1st button: Creating the way points
        if(GUILayout.Button("Create Waypoint")){
            CreateWaypoint();
        }
    }

    void CreateWaypoint(){
        // Create a new game object

        // What this is doing: When we click on Create Way point button, it will create a new game object
        // inside our way point origin by the name of Waypoint and that name will be followed by waypointOrigin.childCount
        // So Eg: 1st Button will be like: Waypoint 0
        // 2nd Button: Waypoint 1
        // Also assign a way point component:
        GameObject waypointObj = new GameObject("Waypoint" + waypointOrigin.childCount, typeof(Waypoint));
        // Set this waypoint object as child of way point origin
        waypointObj.transform.SetParent(waypointOrigin, false);

        // We want to make our way point system automatically link with points when we create new way points
        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();
        // Means if way point object is being created, then what to do
        if(waypointOrigin.childCount > 1){
            // It means that if way point origin has more than 1 child: waypointOrigin.childCount > 1
            // then assign the previous way point
            waypoint.previousWaypoint = waypointOrigin.GetChild(waypointOrigin.childCount-2).GetComponent<Waypoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;

            // Set new point in the same direction as the previous way point
            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            // Place it in forward direction
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }
        // Select newly created way point to edit it
        Selection.activeGameObject = waypoint.gameObject;
    }
}

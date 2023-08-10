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
}

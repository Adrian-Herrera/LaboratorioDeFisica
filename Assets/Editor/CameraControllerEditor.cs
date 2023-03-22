using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraController myScript = (CameraController)target;
        if (GUILayout.Button("Next"))
        {
            myScript.NextTarget();
        }
        if (GUILayout.Button("Previous"))
        {
            myScript.PreviousTarget();
        }
    }
}

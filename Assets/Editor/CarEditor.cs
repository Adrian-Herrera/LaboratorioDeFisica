using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Car3d))]
public class CarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Car3d myScript = (Car3d)target;
        if (GUILayout.Button("Reset All"))
        {
            myScript.ResetAll();
        }
        if (GUILayout.Button("Move"))
        {
            myScript.StartMovement(2);
        }
    }
}

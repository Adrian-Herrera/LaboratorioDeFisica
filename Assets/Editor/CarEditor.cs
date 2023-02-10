using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CinematicObject))]
public class CarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CinematicObject myScript = (CinematicObject)target;
        if (GUILayout.Button("Reset All"))
        {
            myScript.ResetAll();
        }
    }
}

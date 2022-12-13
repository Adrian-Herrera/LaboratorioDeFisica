using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pelota))]
public class PelotaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Pelota myScript = (Pelota)target;
        if (GUILayout.Button("Reset All"))
        {
            myScript.Reset();
        }
        if (GUILayout.Button("Shoot"))
        {
            myScript.Shoot();
        }
    }
}

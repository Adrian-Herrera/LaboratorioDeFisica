using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PolipastoController))]

public class PolipastoControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PolipastoController myScript = (PolipastoController)target;
        if (GUILayout.Button("Add Pulley"))
        {
            myScript.AddPulley();
        }
        if (GUILayout.Button("Remove Pulley"))
        {
            myScript.RemovePulley();
        }
    }
}

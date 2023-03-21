using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Balance))]
public class BalanceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Balance myScript = (Balance)target;
        if (GUILayout.Button("Add 1 mass"))
        {
            myScript.AddWeight(1);
        }
        if (GUILayout.Button("Add 5 mass"))
        {
            myScript.AddWeight(5);
        }
        if (GUILayout.Button("Add 10 mass"))
        {
            myScript.AddWeight(10);
        }
        if (GUILayout.Button("Remove 1 mass"))
        {
            myScript.RemoveWeight(1);
        }
        if (GUILayout.Button("Remove 5 mass"))
        {
            myScript.RemoveWeight(5);
        }
        if (GUILayout.Button("Remove 10 mass"))
        {
            myScript.RemoveWeight(10);
        }
    }
}

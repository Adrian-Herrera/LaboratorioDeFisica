using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CredentialManager))]
public class CredentialManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CredentialManager myScript = (CredentialManager)target;

        DrawDefaultInspector();
        if (GUILayout.Button("Login"))
        {
            myScript.Login();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
[CreateAssetMenu(fileName = "Reto", menuName = "ScriptableObject/RetoScriptableObject")]
public class RetosSO : ScriptableObject
{
    public RetoTemplate[] _questions;
    public RetoTemplate[] Init()
    {
        for (int i = 0; i < _questions.Length; i++)
        {
            RetoTemplate reto = _questions[i];
            reto.IsData = false;
        }
        // int rInt = Random.Range(0, _questions.Length);
        // Debug.Log("random:" + rInt);
        // _questions[rInt].IsData = true;
        return _questions;
    }
}
[Serializable]
public struct RetoTemplate
{
    public VariableEnum VariableType;
    public Variable Variable;
    public float value;
    public bool IsData;
}

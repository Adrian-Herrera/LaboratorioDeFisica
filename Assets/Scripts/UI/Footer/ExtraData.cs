using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraData : MonoBehaviour
{
    [SerializeField] private SegmentElement[] _segmentElements;
    private void Awake()
    {
        _segmentElements = GetComponentsInChildren<SegmentElement>();
    }
    public void Init(Variables[] variables)
    {
        if (_segmentElements.Length == variables.Length)
        {
            int varLength = variables.Length;
            for (int i = 0; i < varLength; i++)
            {
                _segmentElements[i].Init(variables[i]);
            }
        }
        else
        {
            Debug.Log("El numero de datos e inputFields no coincide");
            Debug.Log($"{_segmentElements.Length} == {variables.Length}");
        }
    }
}

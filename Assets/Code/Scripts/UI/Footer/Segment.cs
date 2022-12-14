using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Segment : MonoBehaviour
{
    public SegmentElement[] _segmentElements;
    public bool IsMRUV = true;
    private int _id;
    private void Awake()
    {
        _segmentElements = GetComponentsInChildren<SegmentElement>();
    }
    public void Init(Variables[] variables, int id)
    {
        if (_segmentElements.Length == variables.Length)
        {
            int varLength = variables.Length;
            for (int i = 0; i < varLength; i++)
            {
                _segmentElements[i].Init(variables[i]);
            }

            _id = id;
        }
        else
        {
            Debug.Log("El numero de datos e inputFields no coincide");
        }
    }
    public void Clean()
    {
        foreach (SegmentElement element in _segmentElements)
        {
            element.ClearData();
        }
    }
    public void ChangeToMRUV()
    {
        if (IsMRUV) return;
        Debug.Log("Change to MRUV");
        foreach (SegmentElement element in _segmentElements)
        {
            element.gameObject.SetActive(true);
            if (element.ShortName == "V") element.Init(new Variables(0, "Vo", "Velocidad Inicial"));
        }
        IsMRUV = !IsMRUV;
    }
    public void ChangeToMRU()
    {
        if (!IsMRUV) return;
        Debug.Log("Change to MRU");
        foreach (SegmentElement element in _segmentElements)
        {
            Debug.Log(element.ShortName);
            if (element.ShortName == "a" || element.ShortName == "Vf") element.gameObject.SetActive(false);
            if (element.ShortName == "Vo") element.Init(new Variables(0, "V", "Velocidad"));
        }
        IsMRUV = !IsMRUV;


    }


}

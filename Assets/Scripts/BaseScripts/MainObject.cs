using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainObject : MonoBehaviour
{
    [SerializeField] private MainObjectMovement _mainObjectMovement;
    [SerializeField] private MainObjectConfig _mainObjectConfig;
    public Image Sprite;
    private int _numberOfSegments;
    private SegmentElement[,] _data;
    private SegmentElement[] _extraData;
    public int DataLengthi, DataLengthJ;
    public int ExtraDataLengthi, ExtraDataLengthJ;

    public Variables[] names;
    public Variables[] extraNames;
    public void Init()
    {
        _numberOfSegments = 3;
        names = _mainObjectConfig.names;
        extraNames = _mainObjectConfig.extraNames;
        InstantiateData(_numberOfSegments, names);
    }
    public void ShowDatos()
    {
        Debug.Log(DataLengthi);
        Debug.Log(DataLengthJ);

        string data = "";
        for (int i = 0; i < DataLengthi; i++)
        {
            for (int j = 0; j < DataLengthJ; j++)
            {
                data += _data[i, j].Value.ToString() + " ";
            }
            data += "\n";
        }
        Debug.Log(data);
    }
    private void InstantiateData(int segments, Variables[] variables)
    {
        DataLengthi = segments;
        DataLengthJ = variables.Length;
        _data = new SegmentElement[DataLengthi, DataLengthJ];
    }
    public SegmentElement GetElement(int i, int j)
    {
        return _data[i, j];
    }
    public void SetElement(int i, int j, SegmentElement newElement)
    {
        _data[i, j] = newElement;
    }
}

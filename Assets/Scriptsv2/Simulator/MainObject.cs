using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class MainObject : MonoBehaviour
{
    private Sprite _mainObject;
    public Segmento[] _segmentos = new Segmento[3];
    public int _activeSegments = 1;
    public event Action<int> onAddSegmentData;
    public event Action<int> onRemoveSegmentData;
    public void Init()
    {

    }
    public void addDato(int segmentId, int variableId)
    {
        Dato dato = new(variableId, 0f, 1, 1);
        _segmentos[segmentId].datos.Add(dato);
        AddSegmentData(segmentId);
    }
    public void removeDato(int segmentId, int variableId)
    {
        Dato dt = _segmentos[segmentId].datos.Find(dato => dato.VariableId == variableId);
        if (dt == null) return;
        _segmentos[segmentId].datos.Remove(dt);
        RemoveSegmentData(segmentId);
    }
    public void AddSegmentData(int segmentId)
    {
        if (onAddSegmentData != null)
        {
            onAddSegmentData(segmentId);
        }
    }
    public void RemoveSegmentData(int segmentId)
    {
        if (onRemoveSegmentData != null)
        {
            onRemoveSegmentData(segmentId);
        }
    }


}
[Serializable]
public class Segmento
{
    public List<Dato> datos = new();
}

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
    public event Action<int> OnAddSegmentData;
    public event Action<int> OnRemoveSegmentData;
    public void Init()
    {

    }
    public void AddDato(int segmentId, int variableId)
    {
        Dato dato = new(variableId, 0, 1, 1);
        _segmentos[segmentId].datos.Add(dato);
        AddSegmentData(segmentId);
    }
    public void RemoveDato(int segmentId, int variableId)
    {
        Dato dt = _segmentos[segmentId].datos.Find(dato => dato.VariableId == variableId);
        if (dt == null) return;
        _segmentos[segmentId].datos.Remove(dt);
        RemoveSegmentData(segmentId);
    }
    public bool ResolveDato(int segmentId, int variableId)
    {
        Dato dato = new(variableId, 0, 1, 1);
        bool IsAnswered = Formulary.Instance.CheckRequirements(dato, _segmentos[segmentId].datos.ToArray());
        if (IsAnswered)
        {
            _segmentos[segmentId].datos.Add(dato);
            AddSegmentData(segmentId);
        }
        return IsAnswered;
    }
    public void AddSegmentData(int segmentId)
    {
        OnAddSegmentData?.Invoke(segmentId);
    }
    public void RemoveSegmentData(int segmentId)
    {
        OnRemoveSegmentData?.Invoke(segmentId);
    }


}
[Serializable]
public class Segmento
{
    public List<Dato> datos = new();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalData : MonoBehaviour, ITab
{
    [SerializeField] private FinalDataElement _dataElement;
    [SerializeField] private GameObject _container;
    [SerializeField] private Station _station;
    [SerializeField] private Tablet _tablet;
    private ControlPoints _controlPoints;
    private List<FinalDataElement> _dataList = new();
    public void Init()
    {
        Debug.Log("FinalData Init");
        if (_tablet.ActiveStation != null)
        {
            _controlPoints = _tablet.ActiveStation.GetComponent<ControlPoints>();
            ShowPointsInfo(_controlPoints.PointsInfo);
        }
    }
    public void ShowPointsInfo(Dictionary<VariableUnity, List<VariableUnity>> points)
    {
        Helpers.ClearListContent(_dataList);
        foreach (var item in points)
        {
            FinalDataElement sv = Instantiate(_dataElement, _container.transform);
            sv.Init(item.Key, item.Value);
            _dataList.Add(sv);
        }
        foreach (FinalDataElement item in _dataList)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(item.GetComponent<RectTransform>());
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_container.GetComponent<RectTransform>());
    }
}

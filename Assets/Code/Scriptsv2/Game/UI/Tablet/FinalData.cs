using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class FinalData : MonoBehaviour, ITab
{
    [SerializeField] private FinalDataElement _dataElement;
    [SerializeField] private ShowVariable _prefabShowVariable;
    [SerializeField] private RectTransform _newDataContainer;
    [SerializeField] private RectTransform _oldDataContainer;
    [SerializeField] private RectTransform _newDataVariables;
    [SerializeField] private RectTransform _oldDataVariables;
    [SerializeField] private Station _station;
    [SerializeField] private Tablet _tablet;
    private ControlPoints _controlPoints;
    private readonly List<FinalDataElement> _dataList = new();
    private readonly List<ShowVariable> _initialDataList = new();
    private List<ShowVariable> _oldDataList = new();
    private void Start()
    {
        Player.Instance.OnExitStation += () =>
        {
            Helpers.ClearListContent(_oldDataList);
            Helpers.ClearListContent(_initialDataList);
        };
    }
    public void Init()
    {
        Debug.Log("FinalData Init");
        if (_tablet.ActiveStation != null)
        {
            _controlPoints = ControlPoints.Instance;
            Helpers.ClearListContent(_dataList);
            InstanceInitialValues();
            ShowPointsInfo(_controlPoints.PointsInfo, _newDataContainer);
            ShowPointsInfo(_controlPoints.OldPointsInfo, _oldDataContainer);
        }
    }
    public void ShowPointsInfo(Dictionary<VariableUnity, List<VariableUnity>> points, RectTransform container)
    {
        foreach (var item in points)
        {
            FinalDataElement sv = Instantiate(_dataElement, container);
            sv.Init(item.Key, item.Value);
            _dataList.Add(sv);
        }
        foreach (FinalDataElement item in _dataList)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(item.GetComponent<RectTransform>());
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(container);
    }
    public void InstanceInitialValues()
    {

        Helpers.ClearListContent(_oldDataList);
        foreach (VariableUnity item in _controlPoints._oldStartValues)
        {
            ShowVariable sv = Instantiate(_prefabShowVariable, _oldDataVariables);
            sv.Init(item);
            _oldDataList.Add(sv);
        }

        Helpers.ClearListContent(_initialDataList);
        foreach (VariableUnity item in _controlPoints._newStartValues)
        {
            ShowVariable sv = Instantiate(_prefabShowVariable, _newDataVariables);
            sv.Init(item);
            _initialDataList.Add(sv);
        }

    }
}

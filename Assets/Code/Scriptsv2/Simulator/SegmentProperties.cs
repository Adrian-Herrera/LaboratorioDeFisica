using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentProperties : MonoBehaviour
{
    [SerializeField] private DropDown _dropdown;
    [SerializeField] private GameObject _dataContainer;
    [SerializeField] private DataPropertie _data;
    private int _segmentId;
    private List<Dato> _datos = new();
    private List<DataPropertie> _dataList = new();
    private bool isInit = false;
    public void Init(int segmentId)
    {
        if (!isInit)
        {
            isInit = true;
            _segmentId = segmentId;
            _dropdown.Init(_segmentId);
            SimulatorManager._selectedObject.OnAddSegmentData += CreateVariable;
            SimulatorManager._selectedObject.OnRemoveSegmentData += RemoveVariable;
        }
    }
    private void CreateVariable(int segmentId)
    {
        if (_segmentId != segmentId) return;
        Dato currentData;
        for (int i = 0; i < SimulatorManager._selectedObject._segmentos[_segmentId].datos.Count; i++)
        {
            currentData = SimulatorManager._selectedObject._segmentos[_segmentId].datos[i];
            if (!_datos.Contains(currentData))
            {
                DataPropertie dataPropertie = Instantiate(_data.gameObject, _dataContainer.transform).GetComponent<DataPropertie>();
                dataPropertie.Init(currentData);
                _dataList.Add(dataPropertie);
                _datos.Add(currentData);
                LayoutRebuilder.ForceRebuildLayoutImmediate(dataPropertie.GetComponent<RectTransform>());
            }
            // else
            // {
            //     _dataList[i].ChangeValue(currentData.Valor.ToString());
            // }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_dataContainer.GetComponent<RectTransform>());
    }
    private void RemoveVariable(int segmentId)
    {
        if (_segmentId != segmentId) return;
        for (int i = 0; i < _dataList.Count; i++)
        {
            if (!SimulatorManager._selectedObject._segmentos[_segmentId].datos.Contains(_datos[i]))
            {
                _datos.RemoveAt(i);
                Destroy(_dataList[i].gameObject);
                _dataList.RemoveAt(i);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalDataElement : MonoBehaviour
{
    [Header("Containers")]
    [SerializeField] private GameObject _keyContainer;
    [SerializeField] private GameObject _valueContainer;
    [Header("Prefab")]
    [SerializeField] private ShowVariable _variable;
    // LISTS
    [SerializeField] private List<ShowVariable> _variableInstances = new();
    public void Init(VariableUnity keyElement, List<VariableUnity> listElements)
    {
        // Cleaning list and destroy old gameobjects
        Helpers.ClearListContent(_variableInstances);
        // Instance key value
        ShowVariable keyVar = Instantiate(_variable, _keyContainer.transform);
        keyVar.Init(keyElement);
        _variableInstances.Add(keyVar);
        // Instance list value
        foreach (VariableUnity item in listElements)
        {
            ShowVariable valueVar = Instantiate(_variable, _valueContainer.transform);
            valueVar.Init(item);
            _variableInstances.Add(valueVar);
            LayoutRebuilder.ForceRebuildLayoutImmediate(valueVar.GetComponent<RectTransform>());

        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(keyVar.GetComponent<RectTransform>());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DropDown : MonoBehaviour
{
    [SerializeField] private Variable[] _variables;
    [SerializeField] TMP_Text _headerLabel;
    [SerializeField] Button _arrow;
    [SerializeField] GameObject _content;
    [SerializeField] DropDownOption _optionPrefab;
    private DropDownOption[] _options;
    public int _segmentId;
    public void Init(int id)
    {
        _segmentId = id;
        _arrow.onClick.AddListener(delegate
        {
            _content.SetActive(!_content.activeSelf);
        });
        CreateOptions();
    }
    private void CreateOptions()
    {

        _variables = GlobalInfo.Variables;
        foreach (Variable variable in _variables)
        {
            GameObject newOption = Instantiate(_optionPrefab.gameObject, _content.transform);
            newOption.GetComponent<DropDownOption>().Init(_segmentId, variable);
        }
    }

}

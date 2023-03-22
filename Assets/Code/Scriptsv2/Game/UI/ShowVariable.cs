using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowVariable : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private TMP_Text _unitText;
    [Header("Others")]
    [SerializeField] private VariableUnity _variable;
    public void Init(VariableUnity variable)
    {
        _titleText.text = variable.TipoVariable.Nombre;
        float value = Mathf.Round(variable.Value * 100) / 100;
        _valueText.text = value.ToString();
        variable.OnChangeValue += () =>
        {
            _valueText.text = (Mathf.Round(variable.Value * 100) / 100).ToString();
        };
    }
}

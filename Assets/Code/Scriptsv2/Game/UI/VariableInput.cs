using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VariableInput : MonoBehaviour
{
    public TMP_InputField _inputField;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _magnitude;
    [SerializeField] private bool _isData = false;
    [SerializeField] private Variable _variable;
    public VariableEnum _enum;
    private float _value;
    // Properties
    public bool IsData => _isData;
    public Variable Variable => _variable;
    public float Value
    {
        get
        {
            if (_isData)
            {
                return _value;
            }
            else
            {
                return float.Parse(_inputField.text);
            }
        }
    }
    public void Init(Variable variable, float value, bool isData = false)
    {
        // _enum = variable;
        // _variable = VariableHelper.VariablePairs[variable];
        // Debug.Log(_variable);
        _variable = variable;
        _label.text = variable.Nombre;
        _magnitude.text = variable.Abrev;
        _value = value;
        _isData = isData;
        if (isData)
        {
            _inputField.text = value.ToString();
            _inputField.readOnly = true;
        }
    }
    public bool CheckAnswer()
    {
        Debug.Log(_inputField.text);
        Debug.Log(_value);
        return int.Parse(_inputField.text) == _value;
    }
}

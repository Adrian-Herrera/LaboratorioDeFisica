using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class VariableInput : MonoBehaviour
{
    public TMP_InputField _inputField;
    [SerializeField] protected TMP_Text _label;
    [SerializeField] private TMP_Text _magnitude;
    [SerializeField] private bool _isData = false;
    [SerializeField] protected Variable _variable;
    protected float _value;
    public VariableUnity varUnity;
    public event Action OnChangeValue;
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
    public void Init(Variable variable, float value, bool isData = false) // Eliminar
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
    public VariableInput Init(VariableUnity variable, bool showValue = false)
    {
        varUnity = variable;
        _label.text = variable.TipoVariable.Nombre;
        if (showValue)
        {
            _inputField.text = variable.Value.ToString();
        }
        _inputField.onEndEdit.AddListener((newValue) =>
        {
            if (string.IsNullOrEmpty(newValue) || string.IsNullOrWhiteSpace(newValue))
            {
                _inputField.text = "";
                variable.Value = 0;
            }
            else
            {
                variable.Value = float.Parse(newValue);
            }
            OnChangeValue?.Invoke();
        });
        return this;
        // _magnitude.text = variable.Abrev;
    }
    public bool CheckAnswer()
    {
        Debug.Log(_inputField.text);
        Debug.Log(_value);
        return int.Parse(_inputField.text) == _value;
    }
}

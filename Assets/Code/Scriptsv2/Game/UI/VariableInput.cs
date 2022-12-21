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
    private float _value;
    private VariableTemplate _variable;
    public VariableHelper.VariableEnum _enum;
    private void Start()
    {
        // _inputField.onValueChanged.AddListener((s) => { _value = float.Parse(s); });
    }
    public void Init(VariableHelper.VariableEnum type, float value, bool isData = false)
    {
        _enum = type;
        switch (_enum)
        {
            case VariableHelper.VariableEnum.Velocidad:
                _variable = VariableHelper.Velocidad;
                break;
            case VariableHelper.VariableEnum.Distancia:
                _variable = VariableHelper.Distancia;
                break;
            case VariableHelper.VariableEnum.Tiempo:
                _variable = VariableHelper.Tiempo;
                break;
            default:
                break;
        }
        _label.text = _variable.Name;
        _magnitude.text = _variable.Abrev;
        _value = value;
        if (isData)
        {
            _inputField.text = value.ToString();
            _inputField.readOnly = true;
        }
    }
    public float GetValue()
    {
        return _value;
    }
    public bool CheckAnswer()
    {
        Debug.Log(_inputField.text);
        Debug.Log(_value);
        return int.Parse(_inputField.text) == _value;
    }
}

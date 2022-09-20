using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataPropertie : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_InputField _input;
    [SerializeField] private TMP_Dropdown _drop;
    private Dato _dato;

    public void Init(Dato dato)
    {
        _dato = dato;
        _label.text = GlobalInfo.Variables[dato.VariableId - 1].Abrev + "= ";
        _drop.ClearOptions();
        List<string> options = new();
        foreach (Unidad option in GlobalInfo.Unidades)
        {
            options.Add(option.Abrev);
        }
        _drop.AddOptions(options);
        _input.text = dato.Valor.ToString();
        _input.onEndEdit.AddListener(ChangeValue);
        _dato.dataPropertie = this;
    }
    public void ChangeValue(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
        {
            _dato.Valor = 0;
        }
        else
        {
            _dato.Valor = float.Parse(newValue);
        }
    }
    public void ChangeText(float value)
    {
        print(value);
        _input.text = value.ToString();
    }

}

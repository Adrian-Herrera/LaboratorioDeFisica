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

    public void Init(Dato dato)
    {
        _label.text = GlobalInfo.Variables[dato.VariableId - 1].Abrev;
        _drop.ClearOptions();
        List<string> options = new();
        foreach (Unidad option in GlobalInfo.Unidades)
        {
            options.Add(option.Abrev);
        }
        _drop.AddOptions(options);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPointInput : MonoBehaviour
{
    public VariableInput _inputPrefab;
    public Button _deleteButton;
    private void Awake()
    {
        // _deleteButton.onClick.AddListener()
    }
    public void Init(VariableUnity variable, bool showValue = false)
    {
        _inputPrefab.Init(variable, showValue);
    }

}

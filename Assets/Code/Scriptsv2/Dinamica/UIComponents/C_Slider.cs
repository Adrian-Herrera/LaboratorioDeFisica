using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class C_Slider : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    public Slider _slider;

    private void Start()
    {
        _slider.onValueChanged.AddListener(ValueChange);
    }
    private void ValueChange(float value)
    {
        _value.text = value.ToString();
        // Debug.Log("New value: " + value);
    }
}

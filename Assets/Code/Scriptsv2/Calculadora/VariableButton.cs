using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VariableButton : MonoBehaviour
{
    public static VariableButton ButtonSelected;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _variableId;
    [SerializeField] private Image _background;
    [SerializeField] private Button _button;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Variable _variable;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _background = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            CalculadoraController.Instance.SetVariableId(_variableId);
            SelectNewButton();
        });
    }
    public void SelectNewButton()
    {
        if (ButtonSelected != null)
        {
            ButtonSelected._background.color = _normalColor;
        }
        ButtonSelected = this;
        ButtonSelected._background.color = _selectedColor;
    }
    public void SetData(string text, int id)
    {
        _text.text = text;
        _variableId = id;
    }
}

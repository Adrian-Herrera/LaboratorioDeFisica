using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemaButton : MonoBehaviour
{
    public static TemaButton ButtonSelected;
    [SerializeField] private int _temaId;
    [SerializeField] private Image _background;
    [SerializeField] private Button _button;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _selectedColor;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _background = GetComponent<Image>();
    }
    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            CalculadoraController.Instance.SetTemaId(_temaId);
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

}

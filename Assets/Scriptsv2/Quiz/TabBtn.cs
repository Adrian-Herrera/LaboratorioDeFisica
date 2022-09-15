using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabBtn : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _border;
    [SerializeField] private Color _correctColor;
    [SerializeField] private Color _correctBorderColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _selectedBorderColor;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _normalBorderColor;
    public bool _isAnswer = false;
    private Button _button;
    public int Id;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate
        {
            QuizManager.Current.GoExercise(Id);
        });
    }
    public void SetInteractable(bool b)
    {
        _button.interactable = b;
    }
    public void SetActive(bool b)
    {
        if (!_isAnswer)
        {
            _background.color = b ? _selectedColor : _normalColor;
            _border.color = b ? _selectedBorderColor : _normalBorderColor;
        }

    }
    public void SetCorrect()
    {
        _background.color = _correctColor;
        _border.color = _correctBorderColor;
        _isAnswer = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ExerciseInformation : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _startBtn;
    private TMP_Text _btnName;
    private void Awake()
    {
        _btnName = _startBtn.GetComponentInChildren<TMP_Text>();
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
    public void ButtonConfig(string name, UnityAction action)
    {
        _btnName.text = name;
        _startBtn.onClick.RemoveAllListeners();
        _startBtn.onClick.AddListener(action);
    }
}

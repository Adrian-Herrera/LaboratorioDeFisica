using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabBtn : MonoBehaviour
{
    public bool _isAnswer = false;
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    public void GoExercise(int id)
    {
        QuizManager.Current.GoExercise(id);
    }
    public void SetInteractable(bool b)
    {
        _button.interactable = b;
    }
}

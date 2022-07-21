using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabBtn : MonoBehaviour
{
    public bool _isAnswer = false;
    private Button _button;
    private Image _image;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }
    public void GoExercise(int id) //llamado desde el inspector
    {
        QuizManager.Current.GoExercise(id);
    }
    public void SetInteractable(bool b)
    {
        _button.interactable = b;
    }
    public void SetActive(bool b)
    {
        if (!_isAnswer) _image.color = b ? Color.gray : Color.white;
    }
    public void SetCorrect()
    {
        _image.color = Color.green;
        _isAnswer = true;
    }
}

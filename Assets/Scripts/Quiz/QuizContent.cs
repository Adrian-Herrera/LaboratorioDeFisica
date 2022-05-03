using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizContent : MonoBehaviour
{
    [SerializeField] private MessagePanel _messagePanel;
    [SerializeField] private ContentPanel _contentPanel;
    public void Init()
    {
        ActivateMessage();

    }

    public void ActivateMessage()
    {
        _messagePanel.gameObject.SetActive(true);
        _contentPanel.gameObject.SetActive(false);
    }
    public void ActivateContent()
    {
        _messagePanel.gameObject.SetActive(false);
        _contentPanel.gameObject.SetActive(true);
    }
    public void SetQuestion(Question question)
    {
        if (!_contentPanel.gameObject.activeSelf) ActivateContent();
        _contentPanel.SetQuestion(question);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessagePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text Message;
    [SerializeField] private Button msgBtn;
    private void Start()
    {
        msgBtn.onClick.AddListener(QuizManager.Current.StartQuiz);
    }
}

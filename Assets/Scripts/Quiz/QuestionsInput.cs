using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionsInput : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_InputField _input;
    [SerializeField] private TMP_Text _text;
    private float _answer;
    private QuestionData _questionData;
    public void SetData(QuestionData data, bool isData = false)
    {
        _questionData = data;
        _name.text = _questionData.VarName + "=";
        _answer = _questionData.Answer;
        if (_questionData.IsAnswered)
        {
            _input.interactable = false;
            _input.GetComponent<Image>().color = Color.green;
            _input.text = _questionData.Answer.ToString();
        }

        if (isData) _text.text = _answer.ToString();
        _input.gameObject.SetActive(!isData);
        _text.gameObject.SetActive(isData);
    }
    public bool CheckAnswer()
    {
        if (_questionData.IsAnswered) return true;
        if (float.Parse(_input.text) == _answer)
        {
            _input.interactable = false;
            _input.GetComponent<Image>().color = Color.green;
            _questionData.IsAnswered = true;
            return true;
        }
        return false;
    }
}

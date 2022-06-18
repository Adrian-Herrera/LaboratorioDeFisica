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
    private Dato _questionData;
    public void SetData(Dato data, bool isData = false)
    {
        _questionData = data;
        _name.text = _questionData.Variable.Abrev + " =";
        _answer = _questionData.Valor;
        if (_questionData.IsAnswered)
        {
            _input.interactable = false;
            _input.GetComponent<Image>().color = Color.green;
        }

        if (isData) _text.text = _answer.ToString();
        _input.gameObject.SetActive(!isData);
        _text.gameObject.SetActive(isData);
    }
    public bool CheckAnswer()
    {
        Debug.Log("Check answer from: " + _questionData.Variable.Abrev);
        if (_questionData.IsAnswered) return true;
        Debug.Log(float.Parse(_input.text) + " = " + _answer);
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

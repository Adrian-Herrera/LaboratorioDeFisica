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
    [SerializeField] private TMP_Text _unidad;
    private float _answer;
    [SerializeField] private Dato _questionData;
    public void SetData(Dato data)
    {
        _questionData = data;
        _name.text = QuizManager.Current.VariableIdToText(_questionData.VariableId) + "=";
        _answer = _questionData.Valor;
        _unidad.text = QuizManager.Current.UnidadIdToText(_questionData.UnidadId);
        if (_questionData.IsAnswered)
        {
            _input.interactable = false;
            _input.GetComponent<Image>().color = Color.green;
            _input.text = _answer.ToString();
        }
        bool isQuestion = data.TipoDatoId == 2;
        if (!isQuestion) _text.text = _answer.ToString();
        _input.gameObject.SetActive(isQuestion);
        _text.gameObject.SetActive(!isQuestion);
    }
    public bool CheckAnswer()
    {
        // Debug.Log("Check answer from: " + _questionData.Variable.Abrev);
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

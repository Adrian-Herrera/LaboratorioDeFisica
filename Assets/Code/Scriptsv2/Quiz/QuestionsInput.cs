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
    [SerializeField] private TMP_Text _texto;
    private float _answer;
    [SerializeField] private Dato _questionData;
    public void SetData(Dato data)
    {
        _questionData = data;
        _name.text = data.TipoVariable.Abrev + "=";
        _answer = _questionData.Valor;
        _unidad.text = data.Magnitud.Abrev;
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
        _texto.gameObject.SetActive(!string.IsNullOrEmpty(data.Text));
        _texto.text = data.Text ?? "";
    }
    public bool CheckAnswer()
    {
        // Debug.Log("Check answer from: " + _questionData.Variable.Abrev);
        if (_questionData.IsAnswered) return true;
        if (!string.IsNullOrWhiteSpace(_input.text))
        {
            Debug.Log(float.Parse(_input.text) + " = " + _answer);
            if (float.Parse(_input.text) == _answer)
            {
                _input.interactable = false;
                _input.GetComponent<Image>().color = Color.green;
                _questionData.IsAnswered = true;
                QuizManager.Current.SendAnswer(_questionData.Id, float.Parse(_input.text));
                return true;
            }
            else
            {
                StartCoroutine(WrongAnswer());
            }
            QuizManager.Current.SendAnswer(_questionData.Id, float.Parse(_input.text));
        }
        return false;
    }
    IEnumerator WrongAnswer()
    {
        Color actualColor = _input.GetComponent<Image>().color;
        _input.GetComponent<Image>().color = Helpers.HexColor("#f2725e");
        yield return new WaitForSeconds(3);
        _input.GetComponent<Image>().color = actualColor;
    }
}

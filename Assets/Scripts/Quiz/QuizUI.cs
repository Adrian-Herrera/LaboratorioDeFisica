using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private GameObject _tabContainer;
    [SerializeField] private TabBtn[] _tabBtn;
    [SerializeField] private QuizContent _content;
    private Cuestionario _quiz;
    private Pregunta _activeQuestion;
    private int _index;
    private void Awake()
    {
        _tabBtn = GetComponentsInChildren<TabBtn>();
    }
    public void Init(Cuestionario quiz)
    {
        _quiz = quiz;
        _title.text = quiz.Titulo;
        _timer.text = quiz.TiempoLimite.ToString();
        for (int i = 0; i < _tabBtn.Length; i++)
        {
            _tabBtn[i].gameObject.SetActive(i < _quiz.Preguntas.Length);
            _tabBtn[i].SetInteractable(false);
        }
        _content.Init();
    }
    public void SetQuestion(Pregunta question, int id)
    {
        _index = id;
        _content.SetQuestion(question);
    }
    public void StartQuiz(Pregunta question)
    {
        for (int i = 0; i < _quiz.Preguntas.Length; i++)
        {
            _tabBtn[i].SetInteractable(true);
        }
        _content.SetQuestion(question);
    }
    public void TabComplete()
    {
        _tabBtn[_index].GetComponent<Image>().color = Color.green;
    }

}

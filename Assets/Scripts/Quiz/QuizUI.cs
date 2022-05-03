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
    private Quiz _quiz;
    private Question _activeQuestion;
    private int _index;
    private void Awake()
    {
        _tabBtn = GetComponentsInChildren<TabBtn>();
    }
    public void Init(Quiz quiz)
    {
        _quiz = quiz;
        _title.text = quiz.Title;
        _timer.text = quiz.Time.ToString();
        for (int i = 0; i < _tabBtn.Length; i++)
        {
            _tabBtn[i].gameObject.SetActive(i < _quiz.Questions.Length);
            _tabBtn[i].SetInteractable(false);
        }
        _content.Init();
    }
    public void SetQuestion(Question question, int id)
    {
        _index = id;
        _content.SetQuestion(question);
    }
    public void StartQuiz(Question question)
    {
        for (int i = 0; i < _quiz.Questions.Length; i++)
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

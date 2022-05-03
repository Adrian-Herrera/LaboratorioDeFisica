using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Current;
    private void Awake()
    {
        Current = this;
    }
    [SerializeField] private QuizUI _quizUI;
    [SerializeField] private QuizHelpers _helpers;
    [SerializeField] private Quiz _currentQuiz;
    private Question _currentQuestion;
    private void Start()
    {
        _currentQuiz = SimulateServer.GetQuiz();
        _quizUI.Init(_currentQuiz);
    }
    public void GoExercise(int id)
    {
        _quizUI.SetQuestion(_currentQuiz.Questions[id], id);
    }
    public void StartQuiz()
    {
        _currentQuestion = _currentQuiz.Questions[0];
        _quizUI.StartQuiz(_currentQuestion);
    }
    public void TabComplete()
    {
        _quizUI.TabComplete();
    }


}

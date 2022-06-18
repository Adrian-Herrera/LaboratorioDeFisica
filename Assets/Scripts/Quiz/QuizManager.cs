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
    [SerializeField] private Cuestionario _currentQuiz;
    private Pregunta _currentQuestion;
    private Cuestionario _cuestionario;
    private void Start()
    {
        StartCoroutine(getData());

    }
    IEnumerator getData()
    {
        StartCoroutine(ServerMethods.Current.getData(CredentialManager.Current.SendId, (callback) =>
        {
            _cuestionario = callback;
        }));
        while (_cuestionario == null)
        {
            yield return null;
        }
        _currentQuiz = _cuestionario;
        _quizUI.Init(_currentQuiz);
        yield return null;
    }
    public void GoExercise(int id)
    {
        _quizUI.SetQuestion(_currentQuiz.Preguntas[id], id);
    }
    public void StartQuiz()
    {
        _currentQuestion = _currentQuiz.Preguntas[0];
        _quizUI.StartQuiz(_currentQuestion);
    }
    public void TabComplete()
    {
        _quizUI.TabComplete();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private Graphic _graphic;
    [Header("Header")]
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TabBtn[] _tabBtns;
    private TimeSpan _remainingTime;
    private Cuestionario _quiz;
    private TabBtn _activeTabBtn;
    private int _activeQuestionIndex;

    [Header("Panels")]
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private GameObject _contentPanel;

    [Header("Message Panel")]
    [SerializeField] private TMP_Text _msgPanelText;
    [SerializeField] private Button _msgBtn;

    [Header("Content Panel")]
    [SerializeField] private TMP_Text _enunciado;
    [SerializeField] private GameObject _dataSpace, _questionsSpace;
    [SerializeField] private QuestionsInput _questionsInputPrefab;
    [SerializeField] private Button _checkAnswersBtn;
    private RectTransform[] _rtChildren;
    private readonly List<QuestionsInput> _dataList = new List<QuestionsInput>();
    private readonly List<QuestionsInput> _questionList = new List<QuestionsInput>();
    private Coroutine _coStartTime;

    private void Awake()
    {
        _msgBtn.onClick.AddListener(StartQuiz);
        _checkAnswersBtn.onClick.AddListener(CheckExercise);
        _rtChildren = _contentPanel.GetComponentsInChildren<RectTransform>();
    }
    public void Init(Cuestionario quiz)
    {
        _quiz = quiz;
        _title.text = quiz.Titulo;
        _remainingTime = TimeSpan.FromSeconds(quiz.TiempoLimite);
        _timer.text = _remainingTime.ToString(@"mm\:ss");
        for (int i = 0; i < _tabBtns.Length; i++)
        {
            if (i < _quiz.Preguntas.Length)
            {
                _tabBtns[i].gameObject.SetActive(true);
                _tabBtns[i].Id = i;
                _tabBtns[i].SetInteractable(false);
            }
            else
            {
                _tabBtns[i].gameObject.SetActive(false);
            }
        }
        ShowContent(false);
    }
    public void ShowContent(bool b)
    {
        _messagePanel.SetActive(!b);
        _contentPanel.SetActive(b);
    }
    public void StartQuiz()
    {
        Debug.Log("StartQuiz");
        for (int i = 0; i < _quiz.Preguntas.Length; i++)
        {
            _tabBtns[i].SetInteractable(true);
            _tabBtns[i]._isAnswer = false;
        }
        // _remainingTime = _quiz.TiempoLimite;
        ShowContent(true);
        SetQuestion(0);
        _coStartTime = StartCoroutine(StartTime());
    }
    public void SetQuestion(int id)
    {
        if (_activeTabBtn != null)
        {
            _activeTabBtn.SetActive(false);
        }
        _activeTabBtn = _tabBtns[id];
        _activeTabBtn.SetActive(true);
        Pregunta Pregunta = _quiz.Preguntas[id];
        Helpers.ClearListContent(_questionList);
        Helpers.ClearListContent(_dataList);
        _enunciado.text = Pregunta.Texto;

        _graphic.Init(Pregunta);
        // Debug.Log("childCount: " + _dataSpace.transform.childCount);
        foreach (Dato item in Pregunta.Datos)
        {
            if (!_activeTabBtn._isAnswer) item.IsAnswered = false;
            QuestionsInput q;
            // Debug.Log(item.Valor + " Segmento: " + item.Segmento);
            switch (item.TipoDatoId)
            {
                case 1:
                    q = Instantiate(_questionsInputPrefab, _dataSpace.transform.GetChild(item.Segmento));
                    q.SetData(item);
                    _dataList.Add(q);
                    break;
                case 2:
                    q = Instantiate(_questionsInputPrefab, _questionsSpace.transform.GetChild(item.Segmento));
                    q.SetData(item);
                    _questionList.Add(q);
                    break;
                default:
                    break;
            }
        }
        // necesario para que el tamaño se reajuste correctamente
        LayoutRebuilder.ForceRebuildLayoutImmediate(_contentPanel.GetComponent<RectTransform>());
    }
    public void CheckExercise()
    {
        bool allQuestionsAnswered = true;
        foreach (QuestionsInput question in _questionList)
        {
            bool questionAnswered = question.CheckAnswer();
            allQuestionsAnswered = allQuestionsAnswered && questionAnswered;
        }
        if (allQuestionsAnswered) TabComplete();
    }
    public void TabComplete()
    {
        _activeTabBtn.SetCorrect();
        bool allTabAnswered = true;
        for (int i = 0; i < _quiz.Preguntas.Length; i++)
        {
            allTabAnswered = allTabAnswered && _tabBtns[i]._isAnswer;
        }
        if (allTabAnswered)
        {
            StopCoroutine(_coStartTime);
            ShowContent(false);
            QuizManager.Current.Score = 100;
            _msgPanelText.text = "Felicidades, Acabaste todos los ejercicios. Puntaje: " + QuizManager.Current.Score;
            _msgBtn.gameObject.SetActive(false);
            QuizManager.Current.AddHistorial();
        }
    }
    private void TimeComplete()
    {
        ShowContent(false);
        int questionsAnswered = 0;
        for (int i = 0; i < _quiz.Preguntas.Length; i++)
        {
            if (_tabBtns[i]._isAnswer) questionsAnswered++;
        }
        QuizManager.Current.Score = questionsAnswered * 100 / _quiz.Preguntas.Length;
        _msgPanelText.text = "Se acabo el tiempo. Puntaje: " + QuizManager.Current.Score;
        QuizManager.Current.AddHistorial();
    }

    private IEnumerator StartTime()
    {
        while (_remainingTime.TotalSeconds > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            _remainingTime -= new TimeSpan(0, 0, 1);
            _timer.text = _remainingTime.ToString(@"mm\:ss");
        }
        // Se acabo el tiempo
        TimeComplete();
    }
}

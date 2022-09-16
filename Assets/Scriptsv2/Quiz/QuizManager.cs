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
    [SerializeField] private Cuestionario _currentQuiz;
    public Unidad[] _unidades;
    public Variable[] _variables;
    private Historial[] _historial;
    private List<Historial> _historialList = new List<Historial>();
    private int _numberOfAttempts;
    public int Score;
    private void Start()
    {
        StartCoroutine(GetData());
    }
    private IEnumerator GetData()
    {
        // yield return StartCoroutine(ServerMethods.Current.GetCuestionario(LevelManager.Instance.quizId, (res) =>
        // {
        //     _currentQuiz = res;
        // }));
        yield return StartCoroutine(LoginForm.Login("kilinor", "123456"));
        yield return StartCoroutine(ServerMethods.Current.GetCuestionario(2, (res) =>
        {
            _currentQuiz = res;
        }));
        yield return StartCoroutine(ServerMethods.Current.GetUnidades((res) =>
        {
            _unidades = res;
        }));
        yield return StartCoroutine(ServerMethods.Current.GetVariables((res) =>
        {
            _variables = res;
        }));
        yield return StartCoroutine(ServerMethods.Current.GetAlumnoHistorial(_currentQuiz.Id, (res) =>
        {
            _historial = res;
            _numberOfAttempts = _historial.Length;
            Debug.Log(_historial.Length);
        }));
        _quizUI.Init(_currentQuiz);
    }
    public void GoExercise(int id)
    {
        _quizUI.SetQuestion(id);
    }
    public void StartQuiz()
    {
        // _currentQuestion = _currentQuiz.Preguntas[0];
        _quizUI.StartQuiz();
    }
    public void TabComplete()
    {
        _quizUI.TabComplete();
    }
    public void AddHistorial()
    {
        _numberOfAttempts++;

        Historial historial = new Historial();
        historial.Puntaje = Score;
        historial.AlumnoId = CredentialManager.Current.JwtCredential.id;
        historial.CuestionarioId = _currentQuiz.Id;
        historial.NumeroIntento = _numberOfAttempts;

        StartCoroutine(ServerMethods.Current.CreateHistorial(historial));
        // _historialList.Add(historial);
        // foreach (Historial item in _historialList)
        // {
        //     Debug.Log(JsonUtility.ToJson(item));
        // }
    }
    public string UnidadIdToText(int id)
    {
        for (int i = 0; i < _unidades.Length; i++)
        {
            if (id == _unidades[i].Id) return _unidades[i].Abrev;
        }
        return "Unidad not find";
    }
    public string VariableIdToText(int id)
    {
        for (int i = 0; i < _variables.Length; i++)
        {
            if (id == _variables[i].Id) return _variables[i].Abrev;
        }
        return "Variable not find";
    }


}

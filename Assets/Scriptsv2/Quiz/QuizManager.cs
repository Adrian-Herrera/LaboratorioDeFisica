using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private int _numberOfAttempts;
    public int Score;
    public int CurrentTime;
    public bool isRunning = false;
    [SerializeField] private TMP_Text _infoText;
    public LogInfo _logInfo;
    private void Start()
    {
        StartCoroutine(GetData());
    }
    private void Update()
    {


    }
    private IEnumerator GetData()
    {
        // LevelManager.Instance.temaId = 1;
        // yield return StartCoroutine(LoginForm.Login("adrian", "123456"));
        // yield return StartCoroutine(ServerMethods.Current.GetCuestionario(2, (res) =>
        //     {
        //         _currentQuiz = res;
        //     }));
        if (LevelManager.Instance.TypeQuiz == "Test")
        {
            _currentQuiz = LevelManager.Instance.OnlineQuiz;
        }
        else if (LevelManager.Instance.TypeQuiz == "Quiz")
        {
            yield return StartCoroutine(ServerMethods.Current.GetCuestionario(LevelManager.Instance.quizId, (res) =>
            {
                _currentQuiz = res;
            }));
        }
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
        // _logInfo = new()
        // {
        //     Preguntas = new LogPregunta[_currentQuiz.Preguntas.Length]
        // };
        // for (int i = 0; i < _currentQuiz.Preguntas.Length; i++)
        // {
        //     Pregunta pregunta = _currentQuiz.Preguntas[i];
        //     LogPregunta nuevaP = new()
        //     {
        //         PreguntaId = pregunta.Id,
        //         Tiempo = 0,
        //         Datos = new LogDato[pregunta.Datos.Length]
        //     };
        //     for (int j = 0; j < pregunta.Datos.Length; j++)
        //     {
        //         LogDato nuevoD = new()
        //         {
        //             DatoId = pregunta.Datos[j].Id
        //         };
        //         nuevaP.Datos[j] = nuevoD;
        //     }
        //     _logInfo.Preguntas[i] = nuevaP;
        // }

        _quizUI.Init(_currentQuiz);
    }
    public void GoExercise(int id)
    {
        _quizUI.SetQuestion(id);
    }
    public void StartQuiz()
    {
        // _currentQuestion = _currentQuiz.Preguntas[0];
        // _logInfo.ActivePregunta = 1;
        isRunning = true;
        _quizUI.StartQuiz();
    }
    public void AddHistorial()
    {
        _numberOfAttempts++;

        Historial historial = new()
        {
            Puntaje = Score,
            AlumnoId = CredentialManager.Current.JwtCredential.id,
            CuestionarioId = _currentQuiz.Id,
            NumeroIntento = _numberOfAttempts
        };

        StartCoroutine(ServerMethods.Current.CreateHistorial(historial));
    }
    public void ShowDebugInfo(string info)
    {
        _infoText.text = info;
    }
    public void SendChangePregunta(int id)
    {
        string message = "{ \"Type\": \"ChangePregunta\", \"Params\": { \"ActualPreguntaId\": " + id + "}}";
        WsClient.Instance.SendCommand(message);
        Debug.Log(message);
    }
    public void SendAnswer(int datoId, float answer)
    {
        string message = "{ \"Type\": \"SendAnswer\", \"Params\": { \"datoId\": " + datoId + ", \"respuesta\": " + answer + "}}";
        WsClient.Instance.SendCommand(message);
        Debug.Log(message);
    }
}

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
    public TiempoPreguntas[] tiempos;
    private void Start()
    {
        StartCoroutine(GetData());
    }
    private void Update()
    {
        
    }
    private IEnumerator GetData()
    {
        // LevelManager.Instance.temaId = 2;
        // yield return StartCoroutine(LoginForm.Login("BlancheDelao", "123456"));
        // yield return StartCoroutine(ServerMethods.Current.GetCuestionario(1, (res) =>
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
        tiempos = new TiempoPreguntas[_currentQuiz.Preguntas.Length];
        _quizUI.Init(_currentQuiz);
    }
    public void GoExercise(int id)
    {
        Debug.Log(id);
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
            UsuarioId = CredentialManager.Current.JwtCredential.id,
            EjercicioId = _currentQuiz.Id,
            NumeroIntento = _numberOfAttempts,
            TiempoEmpleado = CurrentTime
        };

        StartCoroutine(ServerMethods.Current.CreateHistorial(historial));
    }
    public void ShowDebugInfo(string info)
    {
        _infoText.text = info;
    }
    public void SendChangePregunta(int id)
    {
        if (WsClient.Instance == null) return;
        string message = "{ \"Type\": \"ChangePregunta\", \"Params\": { \"ActualPreguntaId\": " + id + "}}";
        WsClient.Instance.SendCommand(message);
        Debug.Log(message);
    }
    public void SendAnswer(int datoId, float answer)
    {
        if (WsClient.Instance == null) return;
        string message = "{ \"Type\": \"SendAnswer\", \"Params\": { \"datoId\": " + datoId + ", \"respuesta\": " + answer + "}}";
        WsClient.Instance.SendCommand(message);
        Debug.Log(message);
    }
}

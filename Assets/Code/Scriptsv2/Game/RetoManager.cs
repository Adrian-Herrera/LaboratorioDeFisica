using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RetoManager : MonoBehaviour
{
    [SerializeField] private Station _station;
    [SerializeField] private TableroReto _tableroReto;
    private Cuestionario _activeReto;
    private Historial[] _historial;
    private int _numberOfAttempts;
    private int time;
    float timer = 0.0f;
    int Score = 0;
    private bool _isRunning = false;
    private void Update()
    {
        if (_isRunning)
        {
            timer += Time.deltaTime;
            time = (int)timer % 60;
        }

    }
    // private CinematicObject _cinematicObject;
    public void Init(Cuestionario newReto)
    {
        _activeReto = newReto;
        _tableroReto.SetNewReto(_activeReto);
        StartCoroutine(GetData());
        timer = 0;
        _isRunning = true;

    }
    private IEnumerator GetData()
    {
        yield return StartCoroutine(ServerMethods.Current.GetAlumnoHistorial(_activeReto.Id, (res) =>
        {
            _historial = res;
            _numberOfAttempts = _historial.Length;
            Debug.Log(_historial.Length);
        }));
    }
    public void CheckAnswer()
    {
        // if (_station.ActualMode != Station.ModeEnum.Reto) return;
        bool allAnswered = _activeReto.Preguntas[0].Variables.All(e =>
        {
            float value = _station.Template.FindVarById(e.TipoVariableId).Value;
            Debug.Log(Helpers.RoundFloat(value) + " == " + Helpers.RoundFloat(e.Valor));
            return Helpers.RoundFloat(value) == Helpers.RoundFloat(e.Valor);
        });
        Debug.Log($"Reto Manager => CheckAnswer: {allAnswered}");
        if (allAnswered)
        {
            Score = 100;
            AddHistorial();
        }
        else
        {
            _station.CinematicObject.ResetAll(3);

        }
        // return allAnswered;
    }

    public void AddHistorial()
    {
        _numberOfAttempts++;

        Historial historial = new()
        {
            Puntaje = Score,
            UsuarioId = CredentialManager.Current.JwtCredential.id,
            EjercicioId = _activeReto.Id,
            NumeroIntento = _numberOfAttempts,
            TiempoEmpleado = time
        };

        StartCoroutine(ServerMethods.Current.CreateHistorial(historial));
    }

}

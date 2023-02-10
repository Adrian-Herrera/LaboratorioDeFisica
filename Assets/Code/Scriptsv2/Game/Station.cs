using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StationStatus
{
    Waiting, Running
}
public class Station : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private InstructionSO _instructions;
    [SerializeField] private StationStatus _status;
    // [SerializeField] private StationUI _stationUI;
    [SerializeField] private int _temaId;
    [SerializeField] private TableroReto _tableroReto;
    [Header("Car")]
    [SerializeField] private CinematicObject _cinematicObject;
    private Reto _activeReto;
    private int _intentos;
    public ExerciseTemplate Template = new();
    private ControlPoints _controlPoint = new();
    // ATRIBUTTES
    public CinematicObject CinematicObject => _cinematicObject;
    public string Name => _name;
    public InstructionSO Instructions => _instructions;
    public StationStatus Status => _status;
    public Reto ActiveReto => _activeReto;
    public int Intentos => _intentos;
    // public StationUI StationUI => _stationUI;
    public int TemaId => _temaId;
    private void Start()
    {
        CinematicObject.OnFinishMove += CheckFinalPoint;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Algo ingreso");
            player.SetNearStation(this);
            InstructionUI.OnStartExercise += Activate;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Algo salio");
            player.SetNearStation(null);
            _status = StationStatus.Waiting;
            InstructionUI.OnStartExercise -= Activate;
        }
    }
    private void Activate()
    {
        _status = StationStatus.Running;
        switch (CinematicObject.Type)
        {
            case CinematicType.MRU:
                Template.ActivarMru();
                break;
            case CinematicType.MRUV:
                Template.ActivarMruv();
                break;
            default:
                break;
        }
    }
    public void SetTableroData(Reto reto)
    {
        _activeReto = reto;
        _tableroReto.SetNewReto(reto);
    }
    private void CheckFinalPoint()
    {
        SaveLastVariables();
        bool allAnswered = true;
        if (_activeReto != null)
        {
            for (int i = 0; i < _activeReto.RetoDatos.Length; i++)
            {
                if (_activeReto.RetoDatos[i].EsDato)
                {
                    Debug.Log(_activeReto.RetoDatos[i].Valor + " = " + Template.Variables[_activeReto.RetoDatos[i].Variable.Id].Valor);
                    if (_activeReto.RetoDatos[i].Valor != Template.Variables[_activeReto.RetoDatos[i].Variable.Id].Valor)
                    {
                        allAnswered = false;
                    }
                }
            }
            Debug.Log(allAnswered);
            if (allAnswered == false)
            {
                CinematicObject.ResetAll(3);
            }
        }
        else
        {
            CinematicObject.ResetAll(3);
        }
    }
    private void SaveLastVariables()
    {
        if (Template.Distancia.Activo)
        {
            Template.Distancia.Valor = CinematicObject.DistanceFromStart;
        }
        if (Template.Tiempo.Activo)
        {
            Template.Tiempo.Valor = CinematicObject.TimeMoving;
        }
    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public enum StatusEnum
    {
        Waiting, Running
    }
    public enum ModeEnum
    {
        Reto, Libre, Prueba
    }
    [SerializeField] private string _name;
    [SerializeField] private InstructionSO _instructions;
    [SerializeField] private StatusEnum _status;
    public ModeEnum ActualMode;
    [SerializeField] private int _temaId;
    [Header("Car")]
    [SerializeField] private CinematicObject _cinematicObject;
    // public Reto ActiveReto { get; private set; }
    public int Intentos { get; private set; }
    public ExerciseTemplate Template = new();
    private ControlPoints _controlPoint;
    private RetoManager _retoManager;
    // ATRIBUTTES
    public CinematicObject CinematicObject => _cinematicObject;
    public string Name => _name;
    public InstructionSO Instructions => _instructions;
    public StatusEnum Status => _status;
    // public ModeEnum ActualMode => _actualMode;
    public int TemaId => _temaId;
    // EVENTS
    public event Action OnStartStation;
    private void OnEnable()
    {
        if (_cinematicObject != null)
        {
            _cinematicObject.OnFinishMove += SaveDataTemplate;
        }
    }
    private void OnDisable()
    {
        if (_cinematicObject != null)
        {
            _cinematicObject.OnFinishMove -= SaveDataTemplate;
        }
    }
    private void Awake()
    {
        _retoManager = GetComponent<RetoManager>();
    }
    public void Init()
    {
        OnStartStation?.Invoke();
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
            _status = StatusEnum.Waiting;
            InstructionUI.OnStartExercise -= Activate;
        }
    }
    private void Activate()
    {
        _status = StatusEnum.Running;
        if (_cinematicObject == null) return;
        switch (CinematicObject.Type)
        {
            case CinematicType.MRU:
                Template.ActivarMru();
                break;
            case CinematicType.MRUV:
                Template.ActivarMruv();
                break;
            case CinematicType.Parabolico:
                Template.ActivarParabolico();
                break;
            default:
                break;
        }
    }
    public void SetReto(Reto reto)
    {
        _retoManager.Init(reto);
    }
    private void SaveDataTemplate()
    {
        Template.FindVarByType(BaseVariable.Distancia).Value = _cinematicObject.DistanceFromStart;
        Template.FindVarByType(BaseVariable.Tiempo).Value = _cinematicObject.TimeMoving;
        Template.FindVarByType(BaseVariable.VelocidadInicial).Value = _cinematicObject.VelX;
        if (ActualMode == ModeEnum.Reto)
        {
            _retoManager.CheckAnswer();
        }
    }
}

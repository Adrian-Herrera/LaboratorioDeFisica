using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tablet : View
{
    [Header("Prefab")]
    [SerializeField] private VariableInput _inputPrefab;
    [SerializeField] private GameObject _container;
    [Header("Station")]
    [SerializeField] private Station _activeStation;
    [Header("Buttons")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _resetButton;
    // [SerializeField] private Button _switchViewButton;
    [Header("Control Point")]
    // [SerializeField] private ControlPoints _controlPoints;
    private ControlPointsUI _controlPointsUI;
    private readonly List<VariableInput> _inputList = new();
    public Station ActiveStation => _activeStation;
    // private RectTransform _rt;
    // private bool _isHidden;
    private void Awake()
    {
        // _rt = GetComponent<RectTransform>();
        _controlPointsUI = GetComponent<ControlPointsUI>();
    }
    private void Start()
    {
        _startButton.onClick.AddListener(StartMoveCar);
        Player.Instance.OnExitStation += ExitStation;
        // _switchViewButton.onClick.AddListener(SwitchView);
    }
    public void Init(Station station)
    {
        _activeStation = station;
        if (_activeStation.CinematicObject != null)
        {
            _controlPointsUI.Init(station.GetComponent<ControlPoints>());
            _activeStation.CinematicObject.OnFinishMove += Show;
            Helpers.ClearListContent(_inputList);
            switch (station.CinematicObject.Type)
            {
                case CinematicType.MRU:
                    _inputList.Add(Instantiate(_inputPrefab, _container.transform).Init(station.Template.FindVarByType(BaseVariable.Velocidad)));
                    break;
                case CinematicType.MRUV:
                    _inputList.Add(Instantiate(_inputPrefab, _container.transform).Init(station.Template.FindVarByType(BaseVariable.VelocidadInicial)));
                    _inputList.Add(Instantiate(_inputPrefab, _container.transform).Init(station.Template.FindVarByType(BaseVariable.Aceleracion)));
                    break;
                case CinematicType.Parabolico:
                    _inputList.Add(Instantiate(_inputPrefab, _container.transform).Init(station.Template.FindVarByType(BaseVariable.Velocidad)));
                    _inputList.Add(Instantiate(_inputPrefab, _container.transform).Init(station.Template.FindVarByType(BaseVariable.VelocidadInicial)));
                    _inputList.Add(Instantiate(_inputPrefab, _container.transform).Init(station.Template.FindVarByType(BaseVariable.Gravedad)));
                    break;
                default:
                    break;
            }
        }
    }
    public void ExitStation()
    {
        if (_activeStation != null)
        {
            _activeStation.CinematicObject.OnFinishMove -= Show;
            _activeStation = null;
        }
    }
    public void StartMoveCar()
    {
        CinematicObject cinematicObject = _activeStation.CinematicObject;
        Debug.Log("Move with " + cinematicObject.Type + " type");
        cinematicObject.ResetAll();
        switch (cinematicObject.Type)
        {
            case CinematicType.MRU:
                cinematicObject.SetHorizontalVel(_activeStation.Template.FindVarByType(BaseVariable.Velocidad).Value, 0);
                cinematicObject.StartMovement();
                break;
            case CinematicType.MRUV:
                cinematicObject.SetHorizontalVel(_activeStation.Template.FindVarByType(BaseVariable.VelocidadInicial).Value, _activeStation.Template.FindVarByType(BaseVariable.Aceleracion).Value);
                cinematicObject.StartMovement();
                break;
            case CinematicType.CaidaLibre:
                break;
            case CinematicType.Parabolico:
                cinematicObject.SetHorizontalVel(_activeStation.Template.FindVarByType(BaseVariable.Velocidad).Value, 0);
                cinematicObject.SetVerticalVel(_activeStation.Template.FindVarByType(BaseVariable.VelocidadInicial).Value, _activeStation.Template.FindVarByType(BaseVariable.Gravedad).Value);
                cinematicObject.StartMovement();
                break;
            default:
                Debug.Log("No existe este tipo de cinematica");
                break;
        }
        Hide();
    }
    public void ShowFinalInfo(Cuestionario reto, int intentos)
    {
        // PlayerUI.Instance._actualView.Hide();
        // PlayerUI.Instance._actualView = PlayerUI.Instance._retoFinal;
        // PlayerUI.Instance._actualView.Show();
        PlayerUI.Instance.ShowFinalInfo(reto, intentos);
        // PlayerUI.Instance._retoFinal.Init(_actualReto, _intentos);
    }
}

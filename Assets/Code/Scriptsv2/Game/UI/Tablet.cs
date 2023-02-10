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
    [SerializeField] private Button _switchViewButton;
    private readonly List<VariableInput> _inputList = new();
    private RectTransform _rt;
    private bool _isHidden;
    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }
    private void Start()
    {
        _startButton.onClick.AddListener(StartMoveCar);
        _switchViewButton.onClick.AddListener(SwitchView);
    }
    public void Init(Station station)
    {
        // ca _car = station.Car;
        _isHidden = false;
        _activeStation = station;
        Helpers.ClearListContent(_inputList);
        switch (station.CinematicObject.Type)
        {
            case CinematicType.MRU:
                _inputList.Add(Instantiate(_inputPrefab, _container.transform).Init(station.Template.Velocidad));
                break;
            case CinematicType.MRUV:
                _inputList.Add(Instantiate(_inputPrefab, _container.transform).Init(station.Template.VelocidadInicial));
                _inputList.Add(Instantiate(_inputPrefab, _container.transform).Init(station.Template.Aceleracion));
                break;
            default:
                break;
        }
    }
    public void StartMoveCar()
    {
        CinematicObject _car = _activeStation.CinematicObject;
        Debug.Log("Move with " + _car.Type + " type");
        switch (_car.Type)
        {
            case CinematicType.MRU:
                _car.SetHorizontalVel(_activeStation.Template.Velocidad.Valor, 0);
                _car.StartMovement();
                break;
            case CinematicType.MRUV:
                _car.SetHorizontalVel(_activeStation.Template.VelocidadInicial.Valor, _activeStation.Template.Aceleracion.Valor);
                _car.StartMovement();
                break;
            case CinematicType.CaidaLibre:
                break;
            case CinematicType.Parabolico:
                break;
            default:
                Debug.Log("No existe este tipo de cinematica");
                break;
        }
    }
    public IEnumerator ResetCar()
    {
        yield return new WaitForSeconds(3);
        _activeStation.CinematicObject.ResetAll();
    }
    public void ShowFinalInfo(Reto reto, int intentos)
    {
        // PlayerUI.Instance._actualView.Hide();
        // PlayerUI.Instance._actualView = PlayerUI.Instance._retoFinal;
        // PlayerUI.Instance._actualView.Show();
        PlayerUI.Instance.ShowFinalInfo(reto, intentos);
        // PlayerUI.Instance._retoFinal.Init(_actualReto, _intentos);
    }
    private void SwitchView()
    {
        float heigth = _rt.sizeDelta.y;
        if (_isHidden)
        {
            _rt.anchoredPosition = new Vector2(_rt.anchoredPosition.x, _rt.anchoredPosition.y + heigth);
        }
        else
        {
            _rt.anchoredPosition = new Vector2(_rt.anchoredPosition.x, _rt.anchoredPosition.y - heigth);
        }
        _isHidden = !_isHidden;
    }
}

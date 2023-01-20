using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;
    [SerializeField] private Player _player;
    public Player Player => _player;
    [SerializeField] private Station _station;
    [SerializeField] private GameObject _userInfo;
    [SerializeField] private GameObject _generalInfo;
    [SerializeField] private InstructionUI _instructionUI;
    [Header("Near Station")]
    [SerializeField] private GameObject _nearStationGameObject;
    [SerializeField] private TMP_Text _nearStationText;
    [Header("Canvas")]
    public RetoSelectorMenu _retoSelector;
    public ModeSelector _modeSelector;
    [Header("Views")]
    public RetoFinalInfo _retoFinal;
    // [Header("Stations")]
    // [SerializeField] private StationUI _stationUI;
    // Views
    public View _actualView;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        if (_player != null)
        {
            _player.OnEnterStation += ShowStationName;
            _player.OnExitStation += ExitStation;
            _player.OnStartExercise += StartExercise;
        }
        InstructionUI.OnStartExercise += ShowModeSelector;
    }
    private void ShowStationName(Station station)
    {
        _station = station;
        _nearStationText.text = _station.Name;
        _nearStationGameObject.SetActive(true);
    }
    private void HideStationName()
    {
        _nearStationText.text = "";
        _nearStationGameObject.SetActive(false);
    }
    private void StartExercise()
    {
        HideStationName();
        SetInstructions(_station.Instructions);
    }
    private void ExitStation()
    {
        if (_station.Status == StationStatus.Waiting)
        {
            HideStationName();
        }
        if (_actualView != null)
        {
            _actualView.Hide();
        }
        _station = null;
    }
    private void SetInstructions(InstructionSO instructions)
    {
        ChangeView(_instructionUI);
        _instructionUI.Init(instructions);
    }
    private void ShowModeSelector()
    {
        ChangeView(_modeSelector);
    }
    public void ShowStationUI(int mode)
    {
        switch (mode)
        {
            case 1:
                ChangeView(_retoSelector);
                _retoSelector.Init(1, _player.NearStation.TemaId);
                break;
            case 2:
                ChangeView(_station.StationUI);
                break;
            default:
                break;
        }
    }
    public void StartActualStation(Reto reto)
    {
        ChangeView(_station.StationUI);
        _station.StationUI.Init(reto);
    }
    private void ChangeView(View newView)
    {
        if (_actualView != null)
        {
            _actualView.Hide();
        }
        _actualView = newView;
        _actualView.Show();
    }
}

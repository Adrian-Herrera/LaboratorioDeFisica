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
    // [SerializeField] private Station _activeStation;
    [SerializeField] private GameObject _userInfo;
    [SerializeField] private GameObject _generalInfo;
    [Header("Canvas")]
    public NearStationMessage _nearStationMessage;
    [SerializeField] private InstructionUI _instructionUI;
    public ModeSelector _modeSelector;
    public RetoSelectorMenu _retoSelector;
    public RetoFinalInfo _retoFinal;
    public Tablet _tablet;
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
        // _activeStation = station;
        ChangeView(_nearStationMessage);
        _nearStationMessage.Init(Player.NearStation);
    }
    private void StartExercise()
    {
        SetInstructions(Player.NearStation.Instructions);
    }
    private void ExitStation()
    {
        if (_actualView != null)
        {
            _actualView.Hide();
        }
        // _activeStation = null;
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
                _retoSelector.Init(1, Player.NearStation.TemaId);
                break;
            case 2:
                StartActualStation();
                break;
            default:
                break;
        }
    }
    public void SetStationReto(Reto reto)
    {
        Player.NearStation.SetTableroData(reto);
        StartActualStation();
    }
    public void StartActualStation()
    {
        Player.NearStation.Init();
        ChangeView(_tablet);
        _tablet.Init(Player.NearStation);
    }
    public void ShowFinalInfo(Reto reto, int intentos)
    {
        ChangeView(_retoFinal);
        _retoFinal.Init(reto, intentos);
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

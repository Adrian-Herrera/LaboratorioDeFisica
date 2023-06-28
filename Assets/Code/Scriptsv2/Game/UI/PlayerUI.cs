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
    public TabletDinamica _tabletDinamica;
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
    private void ShowStationName()
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
    public void ShowStationUI(Station.ModeEnum mode)
    {
        switch (mode)
        {
            case Station.ModeEnum.Reto:
                Player.NearStation.ActualMode = Station.ModeEnum.Reto;
                ChangeView(_retoSelector);
                _retoSelector.Init(1, Player.NearStation.TemaId);
                break;
            case Station.ModeEnum.Libre:
                Player.NearStation.ActualMode = Station.ModeEnum.Libre;
                StartActualStation();
                break;
            default:
                break;
        }
    }
    public void SetStationReto(Cuestionario reto)
    {
        Player.NearStation.SetReto(reto);
        StartActualStation();
    }
    public void StartActualStation()
    {
        Player.NearStation.Init();
        if (Player.NearStation.CinematicObject != null)
        {
            ChangeView(_tablet);
            _tablet.Init(Player.NearStation);
        }
        else
        {
            ChangeView(_tabletDinamica);
            _tabletDinamica.Init(Player.NearStation);
        }
    }
    public void ShowFinalInfo(Cuestionario reto, int intentos)
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

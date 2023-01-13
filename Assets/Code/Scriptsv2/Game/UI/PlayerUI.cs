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
        InstructionUI.OnStartExercise += ShowStationUI;
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
        // _instructionUI.gameObject.SetActive(false);
        // _station.StationUI.gameObject.SetActive(false);
        _station = null;
    }
    private void SetInstructions(InstructionSO instructions)
    {
        _actualView = _instructionUI;
        _actualView.Show();
        // _instructionUI.gameObject.SetActive(true);
        _instructionUI.StartExercise(instructions);
    }
    private void ShowStationUI()
    {
        // _station.StationUI.gameObject.SetActive(true);
        // _retoSelector.gameObject.SetActive(true);
        _actualView.Hide();
        _actualView = _retoSelector;
        _actualView.Show();
        _retoSelector.Init(1, _player.NearStation.TemaId);
        // StartCoroutine(ServerMethods.Current.GetRetos((res) =>
        // {
        //     _station.StationUI.Init(res);
        // }));
    }
    public void StartActualStation(Reto reto)
    {
        // _retoSelector.gameObject.SetActive(false);
        _actualView.Hide();
        _actualView = _station.StationUI;
        _actualView.Show();
        _station.StationUI.Init(reto);
    }
}

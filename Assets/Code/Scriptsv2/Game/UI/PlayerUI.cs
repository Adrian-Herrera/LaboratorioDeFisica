using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject _userInfo;
    [SerializeField] private GameObject _generalInfo;
    [SerializeField] private Instruccion _instructions;
    [SerializeField] private GameObject _nearStation;
    [SerializeField] private TMP_Text _nearStationText;
    [SerializeField] private Player _player;
    [Header("stations")]
    [SerializeField] private MRUV_StationUI _mruStation;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        _player.OnEnterStation += SetStation;
        _player.OnExitStation += StopExercise;
        _player.OnStartExercise += StartExercise;
    }
    private void SetStation(string station)
    {
        _nearStationText.text = station;
        _nearStation.SetActive(station != null);
    }
    private void StartExercise(InstructionSO instructionsSO)
    {
        if (!instructionsSO) return;
        _instructions.gameObject.SetActive(true);
        _nearStation.SetActive(false);
        _instructions.StartExercise(instructionsSO);
        _mruStation.gameObject.SetActive(true);
        _instructions.OnStartExercise += _mruStation.Init;
    }
    private void StopExercise()
    {
        _instructions.gameObject.SetActive(false);
        _mruStation.gameObject.SetActive(false);
    }
}

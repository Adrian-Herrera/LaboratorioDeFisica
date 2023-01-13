using System.Collections;
using System;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private InstructionSO _activeInstruction;
    private Station _nearStation;
    public Station NearStation => _nearStation;
    private StarterAssetsInputs _input;
    //Events
    public event Action<Station> OnEnterStation;
    // public void EnterStation(Station station) => OnEnterStation?.Invoke(station);
    public event Action OnExitStation;
    // public void ExitStation() => OnExitStation?.Invoke();
    public event Action OnStartExercise;
    // public void StartExercise(InstructionSO instruction) => OnStartExercise?.Invoke();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _input = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        if (_input.interact)
        {
            Debug.Log("Interact pressed");
            if (_nearStation != null && _nearStation.Status == StationStatus.Waiting)
            {
                OnStartExercise?.Invoke();
            }
            _input.interact = false;
        }
    }
    public void SetNearStation(Station station)
    {
        _nearStation = station;
        if (station != null)
        {
            OnEnterStation?.Invoke(_nearStation);
        }
        else
        {
            OnExitStation?.Invoke();
        }
    }
    public void ClearStation()
    {
        // EnterStation(null);
        // _activeInstruction = null;
        // ExitStation();
    }
}

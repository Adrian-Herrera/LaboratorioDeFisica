using System.Collections;
using System;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private InstructionSO _activeInstruction;
    [SerializeField] private Station _nearStation;
    public Station NearStation => _nearStation;
    private StarterAssetsInputs _input;
    //Events
    public event Action<Station> OnEnterStation;
    public event Action OnExitStation;
    public event Action OnStartExercise;
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
            if (_nearStation != null && _nearStation.Status == Station.StatusEnum.Waiting)
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
}

using System.Collections;
using System;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private InstructionSO _activeInstruction;
    private StarterAssetsInputs _input;
    //Events
    public event Action<string> OnEnterStation;
    public void EnterStation(string station) => OnEnterStation?.Invoke(station);
    public event Action OnExitStation;
    public void ExitStation() => OnExitStation?.Invoke();
    public event Action<InstructionSO> OnStartExercise;
    public void StartExercise(InstructionSO instruction) => OnStartExercise?.Invoke(instruction);
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
            StartExercise(_activeInstruction);
            _input.interact = false;
        }
    }
    public void SetActiveStation(string station, InstructionSO instruction)
    {
        EnterStation(station);
        _activeInstruction = instruction;
    }
    public void ClearStation()
    {
        EnterStation(null);
        _activeInstruction = null;
        ExitStation();
    }
}

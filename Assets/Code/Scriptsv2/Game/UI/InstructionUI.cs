using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionUI : View
{
    [Header("Components")]
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _startButton;
    [Header("Instructions")]
    [SerializeField] private InstructionSO _instructionsSO;
    [Header("Variables")]
    [SerializeField] private int _index;
    // events
    public static event Action OnStartExercise;
    // public void StartExerciseEvent() => OnStartExercise?.Invoke();
    private int Index
    {
        get { return _index; }
        set
        {
            _index = value;
            SetInstruction(_instructionsSO.Instructions[_index]);
            CheckInstructionSize();
        }
    }
    private void Start()
    {
        _nextButton.onClick.AddListener(NextInstruction);
        _previousButton.onClick.AddListener(PreviousInstruction);
        _startButton.onClick.AddListener(StartExercise);
    }
    public void Init(InstructionSO instructionsSO)
    {
        _instructionsSO = instructionsSO;
        Index = 0;
    }
    public void SetInstruction(TemplateInstruction instruction)
    {
        _titleText.text = instruction.Title;
        _descriptionText.text = instruction.Description;
    }
    private void NextInstruction()
    {
        Index++;
    }
    private void PreviousInstruction()
    {
        Index--;
    }
    private void StartExercise()
    {
        gameObject.SetActive(false);
        Debug.Log("Iniciar ejercicio");
        OnStartExercise?.Invoke();
    }
    private void CheckInstructionSize()
    {
        _previousButton.gameObject.SetActive(Index != 0);
        _nextButton.gameObject.SetActive(Index < (_instructionsSO.Instructions.Length - 1));
        _startButton.gameObject.SetActive(Index == (_instructionsSO.Instructions.Length - 1));
    }
}

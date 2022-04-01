using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private InstructionContent _content;
    [SerializeField] private Button _previousBtn, _nextBtn;
    public Instruction[] Instructions;
    [HideInInspector]
    public Instruction SelectedInstruction;
    private int _indice;
    void Start()
    {
        _indice = 0;
        setInstruction(Search(_indice));
    }
    void Update()
    {

    }

    private void setInstruction(Instruction instruction)
    {
        _title.text = instruction.title;
        ExerciseManager.current.ResetValues();
        _content.setData(instruction);
        SelectedInstruction = instruction;


        _previousBtn.gameObject.SetActive(_indice != 0);
        _nextBtn.gameObject.SetActive(_indice != Instructions.Length - 1);
        if (_nextBtn.IsActive())
        {
            _nextBtn.interactable = false;
        }
    }
    private Instruction Search(int indice)
    {
        if (Instructions.Length > 0)
        {
            return Instructions[indice];
        }
        else
        {
            return null;
        }
    }
    public void CheckAnswer()
    {
        // Debug.Log(_content.CheckAnswer());
        _nextBtn.interactable = _content.CheckAnswer();
    }
    public void ShowResult()
    {

    }
    public void Previous()
    {
        if (_indice > 0)
        {
            _indice--;
            setInstruction(Search(_indice));
        }
    }
    public void Next()
    {
        if (_indice < Instructions.Length - 1)
        {
            _indice++;
            setInstruction(Search(_indice));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _timerTxt;
    [SerializeField] private InstructionContent _content;
    [SerializeField] private Button _previousBtn, _nextBtn;
    [SerializeField] private float _limitTime;
    public Instruction[] Instructions;
    [HideInInspector]
    public Instruction SelectedInstruction;
    private int _indice;
    private float _timer;
    private bool _isPaused;
    void Start()
    {
        _indice = 0;
        _isPaused = false;
        _timer = _limitTime;
        SetInstruction(_indice);
    }
    private void Update()
    {
        if (_isPaused) return;
        _timer -= Time.deltaTime;
        _timerTxt.text = _timer.ToString();
        if (_timer <= 0)
        {
            _indice = 0;
            _timer = _limitTime;
            SetInstruction(_indice);
        }
    }

    private void SetInstruction(int indice)
    {
        if (Instructions.Length <= 0) return;

        SelectedInstruction = Instructions[indice];

        _title.text = SelectedInstruction.title;
        ExerciseManager.current.ResetValues();
        _content.SetData(SelectedInstruction);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content.GetComponent<RectTransform>());

        _previousBtn.gameObject.SetActive(_indice != 0);
        _nextBtn.gameObject.SetActive(_indice != Instructions.Length - 1);

        _timer = _limitTime;
        _isPaused = false;
        if (_nextBtn.IsActive())
        {
            _nextBtn.interactable = false;
        }
    }
    public void CheckAnswer() // call by button
    {
        if (_content.CheckAnswer())
        {
            _nextBtn.interactable = true;
            _isPaused = true;
        }
    }
    public void Previous()
    {
        if (_indice > 0)
        {
            _indice--;
            SetInstruction(_indice);
        }
    }
    public void Next()
    {
        if (_indice < Instructions.Length - 1)
        {
            _indice++;
            SetInstruction(_indice);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerTxt;
    [SerializeField] private InstructionContent _content;
    [SerializeField] private ExerciseInformation _information;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private Button _previousBtn, _nextBtn;
    [SerializeField] private float _limitTime;
    [HideInInspector] public Instruction SelectedInstruction;
    public Instruction[] Instructions;
    private int _indice;
    private float _timer;
    private bool _isPaused;
    void Start()
    {
        // Debug.Log("instruction manager start");
        _isPaused = true;
        _content.gameObject.SetActive(false);
        _buttons.SetActive(false);
        _information.SetText("Bienvenido!!! \n Aqui aprenderas a resolver ejercicios de Movimiento Rectilineo Uniformemente Variado o MRUV ");
        _information.ButtonConfig("Empezar", delegate { StartExercise(); });
        _indice = 0;
        _timer = _limitTime;
        // SetInstruction(_indice);
    }
    private void Update()
    {
        if (_isPaused) return;
        _timer -= Time.deltaTime;
        _timerTxt.text = "Tiempo: " + (Mathf.Round(_timer * 10f) / 10f).ToString();
        if (_timer <= 0)
        {
            TimeLimit();
            // _timer = _limitTime;
            // SetInstruction(_indice);
        }
    }

    private void SetInstruction(int indice)
    {
        if (Instructions.Length <= 0) return;

        SelectedInstruction = Instructions[indice];

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
            _content.Congratulation(Instructions.Length - (_indice + 1));
            LayoutRebuilder.ForceRebuildLayoutImmediate(_content.GetComponent<RectTransform>());

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
    public void StartExercise()
    {
        _content.gameObject.SetActive(true);
        _buttons.SetActive(true);
        _isPaused = false;
        _timer = _limitTime;
        SetInstruction(_indice);
        _information.gameObject.SetActive(false);
    }
    private void TimeLimit()
    {
        _content.gameObject.SetActive(false);
        _buttons.SetActive(false);
        _information.gameObject.SetActive(true);
        _isPaused = true;
        _indice = 0;
        _timer = 0;
        _timerTxt.text = "Tiempo: " + (Mathf.Round(_timer * 10f) / 10f).ToString();
        _information.SetText("Buen intento pero se acabo el tiempo \nPuedes empezar nuevamente o practicar un poco e intentarlo mas tarde.");
        _information.ButtonConfig("Volver a empezar", delegate { StartExercise(); });
    }
}

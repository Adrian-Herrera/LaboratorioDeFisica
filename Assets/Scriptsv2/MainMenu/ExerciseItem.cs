using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ExerciseItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SOMedals medals;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _medal;
    [SerializeField] private Button _exerciseBtn;
    // private ExerciseMenu data;
    private Ejercicio _ejercicio;
    public void Init(Ejercicio ejercicio)
    {
        _ejercicio = ejercicio;
        _medal.color = new Color(1, 1, 1, 1);
        if (ejercicio.PuntuacionMaxima == 100) _medal.sprite = medals.GoldMedal;
        else if (ejercicio.PuntuacionMaxima > 70) _medal.sprite = medals.SilverMedal;
        else if (ejercicio.PuntuacionMaxima > 50) _medal.sprite = medals.BronzeMedal;
        else
        {
            _medal.sprite = null;
            _medal.color = new Color(1, 1, 1, 0);
        };

        _title.text = ejercicio.Titulo;
        _exerciseBtn.onClick.AddListener(ChargeExercise);
    }
    private void ChargeExercise()
    {
        LevelManager.Instance.ChargeQuiz(_ejercicio.Id);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ExerciseSelection.Instance.EjercicioSeleccionado = _ejercicio;
    }

}

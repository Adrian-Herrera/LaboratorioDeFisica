using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExerciseSelection : MonoBehaviour
{
    public static ExerciseSelection Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    [SerializeField] private TMP_Text _title;
    [SerializeField] private GameObject _exercisesContainer;
    [SerializeField] private GameObject _exerciseItem;
    [SerializeField] private TMP_Text _infoTitle;
    [SerializeField] private TMP_Text _infoQuestions;
    [SerializeField] private TMP_Text _infoTime;
    [SerializeField] private TMP_Text _infoAttempts;
    [SerializeField] private TMP_Text _infoMaxScore;
    [SerializeField] private GameObject _attemptsContainer;
    [SerializeField] private GameObject _infoHistoryData;

    public Ejercicio[] Exercises;
    private Ejercicio _ejercicioSeleccionado;
    public Ejercicio EjercicioSeleccionado
    {
        get { return _ejercicioSeleccionado; }
        set
        {
            _ejercicioSeleccionado = value;
            InstantiateInfo();
        }
    }
    private readonly List<GameObject> AttemptsList = new();
    private readonly List<GameObject> ExerciseList = new();
    private void OnEnable()
    {
        _title.text = MainMenuCanvasSelector.Instance.SelectedChapterTitle;
        StartCoroutine(GetData());
    }
    private IEnumerator GetData()
    {
        yield return StartCoroutine(ServerMethods.Current.GetAlumnoEjercicios(LevelManager.Instance.temaId, (res) =>
       {
           Exercises = res;
       }));
        InstantiateItems();
    }
    private void InstantiateItems()
    {
        foreach (GameObject item in ExerciseList)
        {
            Destroy(item);
        }
        ExerciseList.Clear();
        foreach (GameObject item in AttemptsList)
        {
            Destroy(item);
        }
        AttemptsList.Clear();
        if (Exercises.Length == 0) return;
        foreach (Ejercicio ejercicio in Exercises)
        {
            GameObject go = Instantiate(_exerciseItem, _exercisesContainer.transform);
            go.GetComponent<ExerciseItem>().Init(ejercicio);
            ExerciseList.Add(go);
        }
        EjercicioSeleccionado = Exercises[0];
    }
    private void InstantiateInfo()
    {
        _infoTitle.text = _ejercicioSeleccionado.Titulo;
        _infoAttempts.text = "Intentos: " + _ejercicioSeleccionado.Intentos.Length;
        _infoMaxScore.text = "Puntaje maximo: " + _ejercicioSeleccionado.PuntuacionMaxima;
        _infoTime.text = "Tiempo limite: " + _ejercicioSeleccionado.TiempoLimite;
        _infoQuestions.text = "Preguntas: " + _ejercicioSeleccionado.NumeroPreguntas;
        foreach (GameObject item in AttemptsList)
        {
            Destroy(item);
        }
        AttemptsList.Clear();
        foreach (Intento intento in _ejercicioSeleccionado.Intentos)
        {
            GameObject go = Instantiate(_infoHistoryData, _attemptsContainer.transform);
            go.GetComponent<HistoryData>().Init(intento.NumeroIntento.ToString(), intento.CreadoEl, intento.Puntaje.ToString());
            AttemptsList.Add(go);
        }
    }


}
[Serializable]

public class Ejercicio
{
    public int Id;
    public string Titulo;
    public int TiempoLimite;
    public int NumeroPreguntas;
    public int PuntuacionMaxima;
    public Intento[] Intentos;

}
[Serializable]

public class Intento
{
    public int NumeroIntento;
    public string CreadoEl;
    public int Puntaje;
}

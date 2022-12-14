using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ExercisePanel : MonoBehaviour
{
    [SerializeField] private GameObject TeacherExerciseContainer;
    private List<GameObject> TeacherExercises = new List<GameObject>();
    [SerializeField] private GameObject ExerciseButtonsContainer;
    [SerializeField] private TMP_Text ExercisesLabel;
    [SerializeField] private ExerciseButton ExercisesButton;
    private Cuestionario[] _cuestionarios;
    private void OnDisable()
    {
        Debug.Log("Exercise Panel De-activated");
    }
    private void OnEnable()
    {
        Debug.Log("Exercise Panel Activated");
        StartCoroutine(GetExercise());
    }
    IEnumerator GetExercise()
    {
        if (CredentialManager.Current.JwtCredential == null)
        {
            TeacherExerciseContainer.SetActive(false);
            yield break;
        }
        TeacherExerciseContainer.SetActive(true);
        yield return StartCoroutine(ServerMethods.Current.GetCuestionarios((res) =>
        {
            _cuestionarios = res;
        }));
        foreach (GameObject item in TeacherExercises)
        {
            Destroy(item);
        }
        TeacherExercises.Clear();
        for (int i = 0; i < _cuestionarios.Length; i++)
        {
            ExerciseButton g = Instantiate(ExercisesButton, ExerciseButtonsContainer.transform);
            // g.Init(quizzes[i].Id);
            g.Init(_cuestionarios[i].Id, i + 1);
            TeacherExercises.Add(g.gameObject);
        }
        //Change label info
        ExercisesLabel.text = "Ejercicios del profesor " + 0 + "/" + _cuestionarios.Length;


    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject TeacherExercise;
    [SerializeField] private GameObject ExerciseButtonsContainer;
    [SerializeField] private TMP_Text ExercisesLabel;
    [SerializeField] private ExerciseButton ExercisesButton;

    private List<GameObject> TeacherExercises = new List<GameObject>();

    public ExercisesModel[] test1 = new ExercisesModel[2];
    public ModelContainer test2;

    public void ChangePanel(string name)
    {
        foreach (var item in items)
        {
            if (item.name != name)
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }
        }

        if (name == "Exercises")
        {
            StartCoroutine(LoadExercise());
        }
    }

    IEnumerator LoadExercise()
    {
        if (CredentialManager.Current.JwtCredential.id == 0 || CredentialManager.Current.JwtCredential.activo == false)
        {
            TeacherExercise.SetActive(false);
            yield break;
        }
        TeacherExercise.SetActive(true);
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:4000/cuestionarios/" + CredentialManager.Current.JwtCredential.id);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            string res = "{\"data\":" + www.downloadHandler.text + "}";
            Debug.Log(res);
            ModelContainer Data = JsonUtility.FromJson<ModelContainer>(res);
            foreach (GameObject item in TeacherExercises)
            {
                Destroy(item);
            }
            TeacherExercises.Clear();
            for (int i = 0; i < Data.data.Length; i++)
            {
                ExerciseButton g = Instantiate(ExercisesButton, ExerciseButtonsContainer.transform);
                g.Init(Data.data[i].Id);
                TeacherExercises.Add(g.gameObject);

            }
        }

    }
    [Serializable]
    public struct ExercisesModel
    {
        public int Id;
        public string Title;
    }
    public struct ModelContainer
    {
        public ExercisesModel[] data;
    }
}


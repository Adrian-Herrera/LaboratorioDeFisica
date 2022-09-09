using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasSelector : MonoBehaviour
{
    public static MainMenuCanvasSelector Instance;
    [SerializeField] GameObject[] canvas;
    public int SelectedChapterId;
    public string SelectedChapterTitle;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public enum SelectCanvas
    {
        MainMenu, ExerciseSelector, Login, Register
    }
    public SelectCanvas actualCanvas;
    public void GoToCanvas(SelectCanvas newCanvas)
    {
        canvas[(int)actualCanvas].SetActive(false);
        canvas[(int)newCanvas].SetActive(true);
        actualCanvas = newCanvas;
    }

}

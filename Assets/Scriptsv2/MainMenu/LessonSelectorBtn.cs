using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LessonSelectorBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private int _id;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // print("On mouse enter called on: " + this.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // print("On mouse exit called on: " + this.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MainMenuCanvasSelector.Instance.SelectedChapterId = _id;
        LevelManager.Instance.temaId = _id;
        MainMenuCanvasSelector.Instance.SelectedChapterTitle = _title.text;
        MainMenuCanvasSelector.Instance.GoToCanvas(MainMenuCanvasSelector.SelectCanvas.ExerciseSelector);
        print("Go to tema" + _id);
    }
}

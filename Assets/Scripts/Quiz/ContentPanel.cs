using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ContentPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _data;
    [SerializeField] private GameObject dataSpace, questionsSpace;
    [SerializeField] private QuestionsInput _questionsInput;
    private RectTransform[] _rtChildren;
    private List<QuestionsInput> DataList = new List<QuestionsInput>();
    private List<QuestionsInput> QuestionList = new List<QuestionsInput>();
    private void Awake()
    {
        _rtChildren = _data.GetComponentsInChildren<RectTransform>();
    }
    public void SetQuestion(Pregunta Pregunta)
    {
        foreach (QuestionsInput item in QuestionList)
        {
            Destroy(item.gameObject);
        }
        QuestionList.Clear();
        foreach (QuestionsInput item in DataList)
        {
            Destroy(item.gameObject);
        }
        DataList.Clear();

        _data.text = Pregunta.Texto;

        foreach (Dato item in Pregunta.Datos)
        {
            QuestionsInput q;
            switch (item.TipoDatoId)
            {
                case 1:
                    q = Instantiate(_questionsInput, dataSpace.transform);
                    q.SetData(item, true);
                    DataList.Add(q);
                    break;
                case 2:
                    q = Instantiate(_questionsInput, questionsSpace.transform);
                    q.SetData(item);
                    QuestionList.Add(q);
                    break;
                default:
                    break;
            }

        }

        foreach (RectTransform item in _rtChildren)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(item.GetComponent<RectTransform>());
        }
    }
    public void CheckExercise()
    {
        bool allAnswered = QuestionList.All(q => q.CheckAnswer() == true);
        if (allAnswered) QuizManager.Current.TabComplete();
    }
}

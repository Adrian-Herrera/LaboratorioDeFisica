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
    public void SetQuestion(Question question)
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

        _title.text = question.Title;
        _data.text = question.Content;

        foreach (QuestionData item in question.Data)
        {
            QuestionsInput q = Instantiate(_questionsInput, dataSpace.transform);
            q.SetData(item, true);
            DataList.Add(q);
        }

        foreach (QuestionData item in question.Questions)
        {
            QuestionsInput q = Instantiate(_questionsInput, questionsSpace.transform);
            q.SetData(item);
            QuestionList.Add(q);
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

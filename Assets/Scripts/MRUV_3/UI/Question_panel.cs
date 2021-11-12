using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Question_panel : MonoBehaviour
{
    [SerializeField]
    private GameObject _button;
    // public Header_manager manager;
    private List<GameObject> QuestionList = new List<GameObject>();
    private void OnEnable()
    {
        EventManager.onChangeType += showQuestion;
    }
    private void OnDisable()
    {
        EventManager.onChangeType -= showQuestion;

    }
    public void showQuestion()
    {
        foreach (var item in QuestionList)
        {
            Destroy(item);
        }
        QuestionList.Clear();
        foreach (var item in HeaderManager.current.ActiveType.problems)
        {
            GameObject go = Instantiate(_button, transform);
            QuestionList.Add(go);
            go.GetComponent<HeaderButton>().setData(item.title, 1);
            go.GetComponent<Button>().onClick.AddListener(item.SelectProblem);
            go.GetComponent<Button>().onClick.AddListener(onQuestionClick);
        }
    }

    private void onQuestionClick()
    {
        EventManager.current.ChangeProblem();
        
    }
}

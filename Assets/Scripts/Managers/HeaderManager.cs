using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderManager : MonoBehaviour
{
    public static HeaderManager current;
    private void Awake()
    {
        current = this;
    }
    public TypesSO ActiveType;
    public problem ActiveProblem;

    [SerializeField] private GameObject TypePanel, ProblemPanel;
    [SerializeField] private GameObject _button;
    [SerializeField] private TypesSO[] Types;
    private List<GameObject> TypeList = new List<GameObject>();
    private List<GameObject> QuestionList = new List<GameObject>();

    private void Start()
    {
        foreach (TypesSO item in Types)
        {
            GameObject go = Instantiate(_button, TypePanel.transform);
            TypeList.Add(go);
            go.GetComponent<HeaderButton>().setTitle(item.title);
            go.GetComponent<Button>().onClick.AddListener(delegate { selectType(item); });
            // go.GetComponent<Button>().onClick.AddListener(EventManager.Current.ChangeType);
        }
    }
    private void selectType(TypesSO type)
    {
        Debug.Log("selectType");
        ActiveType = type;

        foreach (var item in QuestionList)
        {
            Destroy(item);
        }
        QuestionList.Clear();
        foreach (var item in ActiveType.problems)
        {
            GameObject go = Instantiate(_button, ProblemPanel.transform);
            QuestionList.Add(go);
            go.GetComponent<HeaderButton>().setTitle(item.title);
            go.GetComponent<Button>().onClick.AddListener(delegate { selectProblem(item); });
            // go.GetComponent<Button>().onClick.AddListener(onQuestionClick);
        }
    }

    private void selectProblem(problem problem)
    {
        ActiveProblem = problem;
        EventManager.Current.ChangeProblem();
    }


}

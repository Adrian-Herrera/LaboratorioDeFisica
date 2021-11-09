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
    private void OnEnable() {
        EventManager.OnClicked += showQuestion;
    }
    private void OnDisable() {
        EventManager.OnClicked -= showQuestion;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
            go.GetComponent<Type_button>().newButton(QuestionList.FindIndex(d => d == go), item.title, 1);
            go.GetComponent<Button>().onClick.AddListener(item.SelectProblem);
        }
    }
}

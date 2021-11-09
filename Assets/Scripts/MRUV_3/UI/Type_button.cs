using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Type_button : MonoBehaviour
{
    // public Header_manager manager;
    private Sprite imageCover;
    private Button _button;
    private string title;
    private int id, type;

    public void newButton(int _id, string _title, int _type)
    {
        id = _id;
        title = _title;
        type = _type;
    }

    private void Awake()
    {
        // GetComponent<Image>().sprite = SO_TypeButton._sprite;
        _button = GetComponent<Button>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _button.GetComponentInChildren<TMP_Text>().text = title;
        _button.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void onClick()
    {

        if (type == 0)
        {

            EventManager.ChangeOptions();
        }
        else
        {

            EventManager.current.SelectType();
            HeaderManager.current.ActiveProblem.Calculate();
        }
    }





}

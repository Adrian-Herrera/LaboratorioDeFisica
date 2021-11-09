using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Type_panel : MonoBehaviour
{
    [SerializeField]
    private GameObject _button;
    public TypesSO[] Types;
    private List<GameObject> TypeList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        // for (int i = 0; i < manager.options.Length; i++)
        // {
        //     GameObject go = Instantiate(_button, transform);
        //     TypeList.Add(go);
        //     go.GetComponent<Type_button>().newButton(TypeList.FindIndex(d => d == go) ,manager.options[i].name, 0);
        // } 
        foreach (var item in Types)
        {
            GameObject go = Instantiate(_button, transform);
            TypeList.Add(go);
            go.GetComponent<Type_button>().newButton(TypeList.FindIndex(d => d == go), item.title, 0);
            go.GetComponent<Button>().onClick.AddListener(item.SelectType);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



}

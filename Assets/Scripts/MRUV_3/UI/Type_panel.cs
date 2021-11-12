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
        foreach (TypesSO item in Types)
        {
            GameObject go = Instantiate(_button, transform);
            TypeList.Add(go);
            go.GetComponent<HeaderButton>().setData(item.title, 0);
            go.GetComponent<Button>().onClick.AddListener(item.SelectType);
            go.GetComponent<Button>().onClick.AddListener(EventManager.ChangeType);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



}

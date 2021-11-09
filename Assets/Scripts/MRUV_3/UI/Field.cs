using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Field : MonoBehaviour
{
    private FieldsManager manager;
    private int FieldID, FieldSegment;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_InputField>().onEndEdit.AddListener(changeData);
    }

    public void SaveData(int id, int segment, FieldsManager _manager)
    {
        FieldID = id;
        FieldSegment = segment;
        manager = _manager;
    }

    public void ChangeColor(Color newColor)
    {
        GetComponent<Image>().color = newColor;
    }

    private void changeData(string s)
    {
        if (s != "")
        {
            manager.ChangeData(FieldSegment, FieldID, float.Parse(s));
        }
    }

    public bool CheckIfEmpty()
    {
        if (gameObject.GetComponent<TMP_InputField>().text == "")
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}

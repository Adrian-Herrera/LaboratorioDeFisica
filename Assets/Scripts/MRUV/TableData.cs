using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TableData : MonoBehaviour
{

    public Table table;

    private TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(ChangeData);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeData(string Text)
    {
        // Debug.Log("Text: " + Text);
        table.Change(inputField, Text);
    }
}

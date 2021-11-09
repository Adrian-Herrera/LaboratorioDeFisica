using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldList : MonoBehaviour
{
    public GameObject field;
    public string[] fields;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < fields.Length; i++)
        {
            GameObject go = Instantiate(field, this.GetComponent<Transform>());
            go.GetComponent<FieldName>().setData(fields[i], 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

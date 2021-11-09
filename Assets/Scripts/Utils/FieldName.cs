using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldName : MonoBehaviour
{
    private string field_name;
    private double field_value;
    private TMP_Text label;
    private TMP_InputField data;
    // Start is called before the first frame update

    void Start()
    {
        label = this.GetComponentInChildren<TMP_Text>();
        data = this.GetComponentInChildren<TMP_InputField>();
        label.text = field_name;
        data.text = field_value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setData(string fieldName, double fieldValue){
        this.field_name = fieldName;
        this.field_value = fieldValue;
    }

    public double getValue(){
        return this.field_value;
    }

    

}

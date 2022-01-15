using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item
{
    public int row, column;
    private float _itemValue;
    private TMP_InputField _inputField;
    private Field _field;
    public bool status;
    public Item() { }
    public Item(int _row, int _column)
    {
        row = _row;
        column = _column;
    }

    public float itemValue
    {
        get { return _itemValue; }
        set
        {
            // Debug.Log("Cambiaron el value: " + value);
            _inputField.text = value.ToString();
            status = true;
            _itemValue = value;
        }
    }

    // public TMP_InputField inputField
    // {
    //     get { return _inputField; }
    //     set
    //     {
    //         // Debug.Log("cambiaron el field: ");
    //         value.onEndEdit.AddListener(updateValue);
    //         value.onSelect.AddListener(segmentSelected);
    //         _inputField = value;

    //     }
    // }
    public Field field
    {
        get { return _field; }
        set
        {
            _inputField = _field.GetComponent<TMP_InputField>();
            _inputField.onEndEdit.AddListener(updateValue);
            _inputField.onSelect.AddListener(segmentSelected);
            _field = value;
        }
    }

    private void updateValue(string s)
    {
        if (s != "")
        {
            itemValue = float.Parse(s);
            status = true;
        }
        else
        {
            status = false;
        }
    }

    private void segmentSelected(string s)
    {
        EventManager.Current.SelectField(row);
        // Debug.Log("Segment: " + FieldSegment.ToString());
    }

    // public IEnumerator ShowMessage(string Message)
    // {
    //     Debug.Log("Aparece el mensaje de error");
    //     Debug.Log(Message);
    //     yield return new WaitForSeconds(3f);
    //     Debug.Log("Desaparece el mensaje de error");
    // }

}

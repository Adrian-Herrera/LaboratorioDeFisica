using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Field : MonoBehaviour
{
    public int FieldID, FieldSegment;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_InputField>().onEndEdit.AddListener(changeData);
        GetComponent<TMP_InputField>().onSelect.AddListener(segmentSelected);
    }
    public void ChangeColor(Color newColor)
    {
        GetComponent<Image>().color = newColor;
    }

    private void changeData(string s)
    {
        EventManager.Current.ChangeFieldData(FieldSegment, FieldID, s);
    }
    private void segmentSelected(string s)
    {
        EventManager.Current.SelectField(FieldSegment);
        // Debug.Log("Segment: " + FieldSegment.ToString());
    }

    public bool isEmpty()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Field : MonoBehaviour
{
    [SerializeField] private Sprite NormalFieldSprite, ErrorFieldSprite;
    private MessageBox Message;
    public int row, column;
    private float _value;
    public TMP_InputField inputField;
    public bool status, _error;
    #region getters and setters
    public float value
    {
        get { return _value; }
        set
        {
            // Debug.Log("Cambiaron el value: " + value);
            inputField.text = value.ToString();
            status = true;
            _value = value;
        }
    }
    public bool error
    {
        get { return _error; }
        set
        {
            if (value == true)
            {
                inputField.GetComponent<Image>().sprite = ErrorFieldSprite;
            }
            else
            {
                inputField.GetComponent<Image>().sprite = NormalFieldSprite;
            }
            _error = value;
        }
    }
    #endregion
    private void Awake()
    {
        if (TryGetComponent(out MessageBox box))
        {
            Message = box;
        }
        inputField = GetComponent<TMP_InputField>();
        inputField.GetComponent<Image>().sprite = NormalFieldSprite;
        inputField.onEndEdit.AddListener(updateValue);
    }
    private void updateValue(string s)
    {
        if (s != "")
        {
            _value = float.Parse(s);
            status = true;
        }
        else
        {
            _value = 0;
            status = false;
        }
    }
    public void ChangeColor(Color newColor)
    {
        GetComponent<Image>().color = newColor;
    }
    public void ShowError(string text)
    {
        error = true;
        Message.Show(text);
    }
}

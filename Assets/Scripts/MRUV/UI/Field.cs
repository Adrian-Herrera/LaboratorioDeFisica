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
    private TMP_InputField _inputField;
    public bool status, _error;

    #region getters and setters
    public float value
    {
        get { return _value; }
        set
        {
            // Debug.Log("Cambiaron el value: " + value);
            _inputField.text = value.ToString();
            status = true;
            _value = value;
        }
    }
    public TMP_InputField inputField
    {
        get
        {
            if (_inputField == null)
            {
                Debug.Log("InputField Vacio");
            }
            return _inputField;
        }
        set
        {
            // Debug.Log("cambiaron el field: ");
            value.onEndEdit.AddListener(updateValue);
            value.onSelect.AddListener(segmentSelected);
            _inputField = value;

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
    private void updateValue(string s)
    {
        if (s != "")
        {
            _value = float.Parse(s);
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
    }
    void Start()
    {
    }
    public void ChangeColor(Color newColor)
    {
        GetComponent<Image>().color = newColor;
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
    public void ShowError(string text)
    {
        error = true;
        Message.Show(text);
    }
}

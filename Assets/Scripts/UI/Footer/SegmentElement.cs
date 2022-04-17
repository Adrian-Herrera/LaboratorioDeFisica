using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SegmentElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_InputField _input;
    private Image _inputImage;
    private float _value;
    private string _shortName, _longName;
    private int _id;
    private bool _active;
    public bool error, answer = false;
    private void Awake()
    {
        _inputImage = _input.GetComponent<Image>();
        _input.onEndEdit.AddListener(NewValue);
    }
    public void Init(Variables variables)
    {
        _shortName = variables.shortName;
        _longName = variables.longName;
        _id = variables.id;

        _text.text = _shortName;
    }
    public float Value
    {
        get { return _value; }
        set
        {
            float v = Mathf.Round(value * 100f) / 100f;
            _input.text = v.ToString();
            _value = v;
            _active = true;
        }
    }
    public string ShortName
    {
        get { return _shortName; }
    }
    private void NewValue(string s)
    {
        if (s != "")
        {
            Value = float.Parse(s);
            _active = true;
        }
        else
        {
            _value = 0;
            _active = false;
        }
    }
    public void ChangeColor(Color newColor)
    {
        _inputImage.color = newColor;
    }
    public void SetInteractable(bool b)
    {
        _input.interactable = b;
    }
    public void ClearData()
    {
        error = false;
        _active = false;
        answer = false;
        _value = 0;
        _input.text = "";
    }
    public void SetDefault()
    {
        ClearData();
        ChangeColor(Color.white);
        SetInteractable(true);
    }
    public bool IsEmpty()
    {
        return _active;
    }

}

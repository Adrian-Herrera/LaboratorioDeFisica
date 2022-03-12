using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepNumber : MonoBehaviour
{
    private float LetterWidth = 18;
    private float LetterHeight = 23;
    private TMP_Text text;
    private RectTransform rt;
    private int TextLength;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        rt = GetComponent<RectTransform>();
    }
    private void Start()
    {
        // TextLength = text.text.Length;
    }
    public void init(string s)
    {
        Debug.Log("init");
        text.text = s;
        rt.sizeDelta = new Vector2(text.text.Length * LetterWidth, LetterHeight);
    }
    public float getSize()
    {
        return text.text.Length * LetterWidth;
    }
}

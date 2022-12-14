using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepText : MonoBehaviour
{
    [SerializeField] private float LetterWidth;
    [SerializeField] private float LetterHeight;
    private TMP_Text text;
    private RectTransform rt;
    private int TextLength;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        rt = GetComponent<RectTransform>();
    }
    public void init(string s)
    {
        // Debug.Log("init");
        text.text = s;
        // Debug.Log($"TextLength: {text.text.Length * LetterWidth}");
        rt.sizeDelta = new Vector2(text.text.Length * LetterWidth, LetterHeight);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RulerSeparationLine : MonoBehaviour
{
    [SerializeField] private TMP_Text _number;
    private RectTransform _rt;
    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }
    public void Init(float newValue)
    {
        _number.text = newValue.ToString();
    }
    public void SetPosition(float x)
    {
        _rt.anchoredPosition = new Vector2(x, 0);
    }
}

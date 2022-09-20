using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineDrawer : MonoBehaviour
{
    private Transform _start;
    private Transform _end;
    private float _size;
    [SerializeField] private TMP_Text _labelSize;
    private RectTransform _rt;
    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    public void Init(float size, float start, bool withData = true)
    {
        // _start = start;
        // _end = end;
        _rt.anchoredPosition = (new Vector2(size, 0) / 2) + new Vector2(start, 0);
        _rt.sizeDelta = new Vector2(size, 0.25f);
        if (withData)
        {
            _labelSize.text = size.ToString();
        }
        else
        {
            _labelSize.text = "x";
        }
        Playground.Instance.OnChangeScale += SetScale;
    }
    public void SetScale(float scale)
    {
        _rt.sizeDelta = new Vector2(_rt.sizeDelta.x, 0.25f * scale);
        _labelSize.fontSize = scale;
    }
}

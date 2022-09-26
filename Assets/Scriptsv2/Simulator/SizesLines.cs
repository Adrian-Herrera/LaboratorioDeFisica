using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SizesLines : MonoBehaviour
{
    [SerializeField] TMP_Text _label;
    [SerializeField] GameObject _point1;
    [SerializeField] GameObject _point2;
    public bool IsVertical = true;
    private RectTransform _rt;
    public void Init()
    {
        Playground.Instance.OnChangeScale += ChangeScale;
    }
    public void SetNewData(float size, string newText)
    {
        _label.text = newText;
        _rt = GetComponent<RectTransform>();
        if (IsVertical)
        {
            _rt.sizeDelta = new Vector2(_rt.sizeDelta.x, size);
        }
    }
    private void ChangeScale(float scale)
    {
        print("Line change scale");
        _rt = GetComponent<RectTransform>();
        if (IsVertical)
        {
            _rt.sizeDelta = new Vector2(0.1f * scale, _rt.sizeDelta.y);
        }
        _point1.transform.localScale = new Vector3(scale, scale, 1);
        _point2.transform.localScale = new Vector3(scale, scale, 1);
        _label.transform.localScale = new Vector3(scale, scale, 1);
    }
}

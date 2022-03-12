using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepSqrt : MonoBehaviour
{
    [SerializeField] private GameObject line, sqrt;
    private float margin = 10f;
    private float initialSize;

    private void Start()
    {

    }

    public void CalculateSize(GameObject g1)
    {
        Debug.Log(g1.GetComponent<RectTransform>().rect.width);

        g1.transform.SetParent(transform);
        g1.GetComponent<RectTransform>().SetAnchorLeft();
        g1.GetComponent<RectTransform>().anchoredPosition = new Vector2(16, 0);

        // if (g1.TryGetComponent<StepNumber>(out StepNumber num))
        // {
        //     num.setSize();
        // }

        initialSize = GetComponent<RectTransform>().rect.height;
        float width = g1.GetComponent<RectTransform>().rect.width;
        float height = g1.GetComponent<RectTransform>().rect.height;
        line.GetComponent<RectTransform>().sizeDelta = new Vector2(width, (height / initialSize) * 1.5f);
        GetComponent<RectTransform>().sizeDelta = new Vector2(16 + width, height + margin);

        // GetComponent<RectTransform>().sizeDelta = new Vector2(16 + width, GetComponent<RectTransform>().rect.height);
    }
}

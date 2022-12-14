using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepFraction : MonoBehaviour
{
    private RectTransform rt;
    [SerializeField] private GameObject Line;
    public float margin = 20;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    public void checkSize(GameObject Numerator, GameObject Denominator)
    {
        RectTransform rtNum = Numerator.GetComponent<RectTransform>();
        RectTransform rtDen = Denominator.GetComponent<RectTransform>();
        float n1 = rtNum.rect.width;
        float n2 = rtDen.rect.width;
        float size = Mathf.Max(n1, n2);
        // Debug.Log("size" + size + ": " + n1 + " " + n2);
        float temp = (rtDen.rect.height + rtNum.rect.height) + 10;
        rt.sizeDelta = new Vector2(size + margin, temp);

        float anchor = (rtDen.rect.height + 5) / temp;
        Line.GetComponent<RectTransform>().SetAnchorMin(0, anchor);
        Line.GetComponent<RectTransform>().SetAnchorMax(1, anchor);
        rt.GetComponent<RectTransform>().SetPivot(0.5f, anchor);


        Numerator.transform.SetParent(Line.transform);
        rtNum.SetAnchorBottom();
        rtNum.anchoredPosition = new Vector2(0, 5);

        Denominator.transform.SetParent(Line.transform);
        rtDen.SetAnchorTop();
        rtDen.anchoredPosition = new Vector2(0, -5);
    }

}

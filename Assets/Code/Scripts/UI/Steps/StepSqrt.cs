using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepSqrt : MonoBehaviour
{
    [SerializeField] private GameObject line;
    private float _vmargin = 14f;
    private RectTransform rt;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    public void init(GameObject[] objs)
    {
        // Debug.Log(g1.GetComponent<RectTransform>().rect.width);
        float offset = 16;
        float height = 0;
        for (int i = 0; i < objs.Length; i++)
        {
            RectTransform tempRT = objs[i].GetComponent<RectTransform>();
            objs[i].transform.SetParent(transform);
            tempRT.SetAnchorLeft();
            tempRT.anchoredPosition = new Vector2(offset, 0);
            offset += tempRT.rect.width;
            height = Mathf.Max(tempRT.rect.height, height);
        }

        line.GetComponent<RectTransform>().sizeDelta = new Vector2(offset - 16, ((height + _vmargin) / rt.rect.height) * 1.5f);
        rt.sizeDelta = new Vector2(offset, height + _vmargin);
    }
}

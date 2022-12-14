using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Line : MonoBehaviour
{
    private GameObject start, end;
    private Vector3 margin;
    private RectTransform rt;
    private TMP_Text text;
    private float scale = 1;
    private float value;
    public string LineName;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        text = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        EventManager.Current.onChangeZoom += updateSize;
    }
    private void Update()
    {
        updatePoints(margin);
    }
    public void setPoints(GameObject _start, GameObject _end, Vector3 _margin, string name)
    {
        start = _start;
        end = _end;
        margin = _margin;
        LineName = name;
    }
    private void updatePoints(Vector3 margin)
    {
        rt.anchoredPosition = (((end.transform.position - start.transform.position) / 2) + start.transform.position + margin);
        rt.anchoredPosition = new Vector2(rt.position.x * scale, rt.position.y);
        value = Vector3.Distance(start.transform.position, end.transform.position);
        text.text = value > 0 ? setName(value.ToString()) : "";

        rt.sizeDelta = new Vector2(1 * scale, value);
    }
    private string setName(string value)
    {
        return LineName + ": " + value;
    }
    private void updateSize(float cameraScale)
    {
        scale = cameraScale;

        text.GetComponentInParent<Image>().transform.localScale = new Vector3(1 * scale, 1 * scale);
    }

}

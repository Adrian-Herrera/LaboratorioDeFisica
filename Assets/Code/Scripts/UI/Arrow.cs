using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Arrow : MonoBehaviour
{
    private TMP_Text text;
    private Image image;
    private RectTransform rt;
    private float scale = 1, startPosition;
    public string ArrowName;
    public BasePointSO basePoint;
    public GameObject point;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        text = GetComponentInChildren<TMP_Text>();
        image = GetComponentInChildren<Image>();
    }
    private void Start()
    {
        EventManager.Current.onChangeZoom += changePosition;
        text.text = ArrowName;

        startPosition = rt.anchoredPosition.x;
    }
    void Update()
    {
        float value = basePoint.Datos[0, 2].value;
        if (value == 0)
        {
            text.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
        }
        else
        {
            text.gameObject.SetActive(true);
            image.gameObject.SetActive(true);
            if (value > 0)
            {
                image.transform.rotation = new Quaternion(0, 0, -180, 1);
            }
            else
            {
                image.transform.rotation = new Quaternion(0, 0, 0, 1);
            }
        }
    }
    private void LateUpdate()
    {
        rt.localScale = new Vector3(point.transform.localScale.x, point.transform.localScale.y);
        rt.anchoredPosition = new Vector3(startPosition * scale, point.transform.position.y);
    }
    private void changePosition(float CameraScale)
    {
        scale = CameraScale;
    }
}

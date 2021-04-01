using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceBars : MonoBehaviour
{

    public soCar dataCar;
    public GameObject Canvas;

    public GameObject[] Lines;
    public GameObject[] Cars;

    public int[] segments;
    public GameObject segment;

    private float lineSize;


    // Start is called before the first frame update
    void Start()
    {
        // dataCar.numberSegments = 1;

        RectTransform prt = Canvas.GetComponent<RectTransform>();

        RectTransform rt = GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (prt.sizeDelta.x * 0.95f));
        lineSize = rt.sizeDelta.x;

        foreach (var item in GetComponentsInChildren<Line>())
        {
            item.initialSize(lineSize);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}

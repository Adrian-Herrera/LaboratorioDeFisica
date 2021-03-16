using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceBars : MonoBehaviour
{

    public GameObject Canvas;

    public GameObject[] Lines;
    public GameObject[] Cars;

    public int[] segments;
    public GameObject segment;

    private float lineSize;


    // Start is called before the first frame update
    void Start()
    {
        segments = new int[] { 1, 1, 1 };

        RectTransform prt = Canvas.GetComponent<RectTransform>();

        RectTransform rt = GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (prt.sizeDelta.x * 0.9f));
        lineSize = rt.sizeDelta.x;

        foreach (var line in Lines)
        {
            line.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lineSize);

        }

        addSegment(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addSegment(int lineIndex)
    {
        var sg = Instantiate(segment, Lines[lineIndex - 1].transform);
        sg.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lineSize);
        sg.GetComponent<Line>().car = Cars[lineIndex - 1];

        Debug.Log("Numero de segmentos " + segments[lineIndex - 1]);
    }

}

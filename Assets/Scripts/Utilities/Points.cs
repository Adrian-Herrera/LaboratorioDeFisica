using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public BallSO mainBall;
    public GameObject groundPoint, HoPoint, hPoint, HTotalPoint;
    private void Start()
    {

        groundPoint.transform.position = new Vector3(0, 0);
    }
    private void Update()
    {
        HoPoint.transform.position = new Vector3(0, mainBall.ExtraFields["Ho"].value);
        hPoint.transform.position = new Vector3(0, mainBall.ExtraFields["h-max"].value);
        HTotalPoint.transform.position = new Vector3(0, mainBall.Datos[0, 3].value);
    }
}
[System.Serializable]
public struct Point
{
    public string name;
    public float value;
    public float margin;
    public GameObject start;
    public GameObject end;
    public Color color;
}

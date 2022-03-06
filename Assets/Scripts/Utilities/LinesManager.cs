using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinesManager : MonoBehaviour
{
    public GameObject Line;
    public GameObject canvas;
    public Point[] points;
    private List<GameObject> Lines = new List<GameObject>();
    private void Start()
    {
        foreach (Point item in points)
        {
            InstantiateLine(item.start, item.end, new Vector3(item.margin, 0), item.color, item.name);
        }
    }
    public void InstantiateLine(GameObject start, GameObject end, Vector3 margin, Color color, string name)
    {
        GameObject go = Instantiate(Line, canvas.transform);
        go.GetComponent<Line>().setPoints(start, end, margin, name);
        go.GetComponentInChildren<Image>().color = color;
        Lines.Add(go);
    }
}

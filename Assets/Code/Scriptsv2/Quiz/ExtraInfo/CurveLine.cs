using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveLine : MonoBehaviour
{
    public RectTransform Point1;
    public RectTransform Point2;
    public RectTransform Point3;
    public RectTransform Point4;
    public LineRenderer linerenderer;
    public float vertexCount = 12;
    public float Point2Ypositio = 2;
    public bool curveSide = true;
    public void Init()
    {
        Vector3 Point3Fixed = new(Point3.transform.position.x - 1f, Point3.transform.position.y, Point3.transform.position.z);
        Vector3 middelPoint = new((Point1.transform.position.x + Point3Fixed.x) / 2, (Point1.transform.position.y + Point3Fixed.y) / 2, (Point1.transform.position.z + Point3Fixed.z) / 2);
        float distance = Vector3.Distance(middelPoint, Point2.transform.position);
        Vector3 temp = new(Point2.transform.position.x, Point2.transform.position.y + distance, Point2.transform.position.z);
        var pointList = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
        {
            var tangent1 = Vector3.Lerp(Point1.position, temp, ratio);
            var tangent2 = Vector3.Lerp(temp, Point3Fixed, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);
            curve += new Vector3(0, 0, -1);

            pointList.Add(curve);
        }
        pointList.Add(Point4.transform.position);

        linerenderer.positionCount = pointList.Count;
        linerenderer.SetPositions(pointList.ToArray());
    }
}

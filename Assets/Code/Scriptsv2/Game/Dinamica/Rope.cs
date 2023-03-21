using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    public void SetPoints(Vector3 initialPoint, Vector3 endPoint)
    {
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPositions(new Vector3[] { initialPoint, endPoint });
    }
}

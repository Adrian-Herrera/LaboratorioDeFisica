using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Transform _pos1, _pos2;
    private Vector3 _temp1, _temp2;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    public void SetPoints(Transform initialPoint, Transform endPoint)
    {
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPositions(new Vector3[] { initialPoint.position, endPoint.position });
        _pos1 = initialPoint;
        _pos2 = endPoint;
        _temp1 = _pos1.position;
        _temp2 = _pos2.position;
    }
    private void Update()
    {
        if (_pos1.position != _temp1)
        {
            _lineRenderer.SetPosition(0, _pos1.position);
            _temp1 = _pos1.position;
        }
        if (_pos2.position != _temp2)
        {
            _lineRenderer.SetPosition(1, _pos2.position);
            _temp2 = _pos2.position;
        }
    }
}

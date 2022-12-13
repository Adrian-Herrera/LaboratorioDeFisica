using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRopes : MonoBehaviour
{
    [SerializeField] private GameObject _mainObject;
    [SerializeField] private GameObject _attachedObject;
    private LineRenderer _lineRenderer;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        _lineRenderer.positionCount = 2;
    }
    private void Update()
    {
        if (_mainObject != null)
        {
            _lineRenderer.SetPosition(0, _mainObject.transform.position);
        }
        if (_attachedObject != null)
        {
            _lineRenderer.SetPosition(1, _attachedObject.transform.position);
        }
    }
}

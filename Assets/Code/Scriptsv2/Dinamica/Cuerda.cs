using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuerda : MonoBehaviour
{
    public GameObject Origin;
    public GameObject Hook;
    private LineRenderer _lineRenderer;
    private DragObject _hookDrag;
    public GameObject AttachedObject;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _hookDrag = Hook.GetComponent<DragObject>();
    }
    private void Start()
    {
        _lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        AttachedObject = _hookDrag.AttachedObject;
        if (Origin != null)
        {
            _lineRenderer.SetPosition(0, Origin.transform.position);
        }
        if (Hook != null)
        {
            _lineRenderer.SetPosition(1, Hook.transform.position);
        }
    }
    public void SetObjects(GameObject go1, GameObject go2)
    {
        Origin = go1;
        Hook = go2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool _dragging;
    private Vector2 _offset;
    public Collider2D targetCollider;
    public Collider2D myCollider;
    public float snapDistance = 1;
    public Vector3 distance;
    public float magnitude;
    public GameObject AttachedObject;
    public Vector3 _snapPosition;
    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }
    void Update()
    {
        if (!_dragging) return;
        transform.position = GetMousePos() - _offset;
        if (targetCollider == null) return;
        Vector3 myClosestPoint = myCollider.ClosestPoint(targetCollider.transform.position);
        Vector3 targetClosestPoint = targetCollider.ClosestPoint(myClosestPoint);
        distance = targetClosestPoint - myClosestPoint;
        magnitude = distance.magnitude;
    }
    public void StartMovement()
    {
        if (CompareTag("Hook"))
        {
            transform.SetParent(null);
        }
    }
    private void OnMouseDown()
    {
        Debug.Log("Moviendo " + gameObject.name);
        _dragging = true;
        _offset = GetMousePos() - (Vector2)transform.position;

    }
    private void OnMouseUp()
    {
        _dragging = false;
        if (targetCollider == null) return;
        if (distance.magnitude == 0)
        {
            transform.position = _snapPosition;
        }
        if (distance.magnitude < snapDistance)
        {
            transform.position += distance;
            if (CompareTag("DynamicObject") && targetCollider.CompareTag("Floor"))
            {
                transform.SetParent(targetCollider.transform);
            }
            if (CompareTag("Hook") && targetCollider.CompareTag("DynamicObject"))
            {
                transform.SetParent(targetCollider.transform);
                targetCollider.GetComponent<DynamicObject>()._hasRope = true;
            }
            AttachedObject = targetCollider.transform.gameObject;
            // transform.localPosition = new Vector3(0, transform.localPosition.y, 0);

        }
        else
        {
            if (CompareTag("Hook") && targetCollider.CompareTag("DynamicObject"))
            {
                targetCollider.GetComponent<DynamicObject>()._hasRope = false;
            }
            AttachedObject = null;
            transform.SetParent(null);
            targetCollider = null;
        }
        transform.localEulerAngles = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_dragging)
        {
            targetCollider = other;
            _snapPosition = transform.position;
        }
    }
    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

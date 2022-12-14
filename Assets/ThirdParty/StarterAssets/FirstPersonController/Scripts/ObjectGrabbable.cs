using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody _objectRb;
    private Transform _objectGrabPointTransform;
    private void Awake()
    {
        _objectRb = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        _objectGrabPointTransform = objectGrabPointTransform;
        _objectRb.useGravity = false;
    }
    public void Drop()
    {
        _objectGrabPointTransform = null;
        _objectRb.useGravity = true;
    }
    private void FixedUpdate()
    {
        if (_objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, _objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            _objectRb.MovePosition(newPosition);
        }
    }
}

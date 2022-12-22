using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car3d : MonoBehaviour
{
    [SerializeField] private float _maxRealDistance = 3f;
    [Header("Components")]
    [SerializeField] private Rigidbody _rb;
    [Header("Variables")]
    [SerializeField] private float _vel;
    [SerializeField] private float _acc;
    [SerializeField] private float _timeMoving;
    [SerializeField] private float _distanceFromStart;
    [SerializeField] private float _maxVirtualDistance;
    [SerializeField] private bool _isMoving = false;
    public float Vel => _vel;
    public float TimeMoving => _timeMoving;
    public float DistanceFromStart => _distanceFromStart;
    public bool IsMoving => _isMoving;
    // Events
    public event Action OnFinishMove;
    public void FinishMove() => OnFinishMove?.Invoke();
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _maxVirtualDistance = 100f;
    }
    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _timeMoving += 10;
            _distanceFromStart = _vel * (_timeMoving / 1000);
            if (_distanceFromStart >= _maxVirtualDistance)
            {
                _distanceFromStart = _maxVirtualDistance;
                _timeMoving = _maxVirtualDistance / _vel * 1000;
                StopMovement();
                FinishMove();
            }
        }

    }
    public void StartMovement(float vel)
    {
        _vel = vel;
        float scale = _maxVirtualDistance / _maxRealDistance;
        _timeMoving = 0;
        _rb.velocity = Vector3.forward * (_vel / scale);
        _isMoving = true;
    }
    public void StopMovement()
    {
        _rb.velocity = Vector3.zero;
        _isMoving = false;
        if (transform.localPosition.z >= _maxRealDistance)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _maxRealDistance);
        }
    }
    public void ResetAll()
    {
        StopMovement();
        _timeMoving = 0;
        _distanceFromStart = 0;
        _vel = 0;
        transform.localPosition = new Vector3(0, 0.6f, 0);
        transform.localEulerAngles = Vector3.zero;
    }
}

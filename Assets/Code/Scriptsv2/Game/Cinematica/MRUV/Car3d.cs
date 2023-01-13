using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CinematicType
{
    MRU, MRUV, CaidaLibre, Parabolico
}
public class Car3d : MonoBehaviour
{
    [SerializeField] private float _maxRealDistance = 3f;
    [Header("Components")]
    [SerializeField] private Rigidbody _rb;
    [Header("Variables")]
    [SerializeField] private float _velX;
    [SerializeField] private float _velY;
    [SerializeField] private float _accX;
    [SerializeField] private float _accY;
    [SerializeField] private float _timeMoving;
    [SerializeField] private float _distanceFromStart;
    [SerializeField] private float _maxVirtualDistance;
    [SerializeField] private bool _isMoving = false;
    public CinematicType _type;
    public float VelX => _velX;
    public float VelY => _velY;
    public float TimeMoving => _timeMoving;
    public float DistanceFromStart => _distanceFromStart;
    public bool IsMoving => _isMoving;
    // Events
    public event Action OnFinishMove;
    // public void FinishMove() => OnFinishMove?.Invoke();
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _maxVirtualDistance = 100f;
        SetHorizontalVel(0, 0);
        SetVerticalVel(0, 0);
    }
    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _timeMoving += 10;
            if (_accX != 0)
            {
                float scale = _maxVirtualDistance / _maxRealDistance;
                _rb.AddForce(new Vector3(0, 0, _accX / scale), ForceMode.Acceleration);
            }
            if (_type == CinematicType.MRU)
            {
                _distanceFromStart = _velX * (_timeMoving / 1000);
            }
            else if (_type == CinematicType.MRUV)
            {
                _distanceFromStart = _velX * (_timeMoving / 1000) + _accX * Mathf.Pow(_timeMoving / 1000, 2) / 2;
            }
            if (_distanceFromStart >= _maxVirtualDistance)
            {
                _distanceFromStart = _maxVirtualDistance;
                _timeMoving = _maxVirtualDistance / _velX * 1000;
                StopMovement();
                OnFinishMove?.Invoke();
            }
        }

    }
    public void StartMovement()
    {
        float scale = _maxVirtualDistance / _maxRealDistance;
        _timeMoving = 0;
        _rb.velocity = new Vector3(0, _velY / scale, _velX / scale);
        _isMoving = true;
    }
    public void SetHorizontalVel(float vel, float acc)
    {
        _velX = vel;
        _accX = acc;
    }
    public void SetVerticalVel(float vel, float acc)
    {
        _velY = vel;
        _accY = acc;
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
        transform.localPosition = new Vector3(0, 0.6f, 0);
        transform.localEulerAngles = Vector3.zero;
    }
}

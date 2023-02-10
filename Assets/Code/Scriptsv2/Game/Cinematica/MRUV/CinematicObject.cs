using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CinematicType
{
    MRU, MRUV, CaidaLibre, Parabolico
}
public class CinematicObject : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float _maxRealDistance = 3f;
    [SerializeField] private float _maxVirtualDistance = 100f;
    private float _scale;
    [Header("Components")]
    [SerializeField] private Rigidbody _rb;
    [Header("Variables Iniciales")]
    [SerializeField] private float _velX;
    [SerializeField] private float _velY;
    [SerializeField] private float _accX;
    [SerializeField] private float _accY;
    [Header("Variables Actuales")]
    [SerializeField] private float _actualVelX;
    [SerializeField] private float _actualVelY;
    [SerializeField] private float _timeMoving;
    [SerializeField] private float _distanceFromStart;
    [SerializeField] private bool _isMoving = false;
    private Vector3 _initialPos;
    public CinematicType Type;
    public float VelX => _velX;
    public float ActualVelX => _actualVelX;
    public float VelY => _velY;
    public float TimeMoving => _timeMoving / 1000;
    public float DistanceFromStart => _distanceFromStart;
    public float MaxVirtualDistance => _maxVirtualDistance;
    public bool IsMoving => _isMoving;
    // Events
    public event Action OnFinishMove;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _scale = _maxVirtualDistance / _maxRealDistance;
        _initialPos = transform.localPosition;
        SetHorizontalVel(0, 0);
        SetVerticalVel(0, 0);
    }
    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _timeMoving += 10;
            if (_distanceFromStart >= _maxVirtualDistance)
            {
                _distanceFromStart = _maxVirtualDistance;
                StopMovement();
                CalculateTime();
                CalculateActualVelocity();
                OnFinishMove?.Invoke();
            }
            else
            {
                if (_accX != 0 || _accY != 0)
                {
                    Accelerate(_accX, _accY);
                }
                CalculateDistance();
                CalculateActualVelocity();
            }
        }

    }
    public void StartMovement()
    {
        _timeMoving = 0;
        _rb.velocity = Vector3.zero;
        _rb.velocity += transform.forward * (_velX / _scale);
        _rb.velocity += transform.up * (_velY / _scale);
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
    private void Accelerate(float xAcc, float yAcc)
    {
        _rb.AddRelativeForce(new Vector3(0, yAcc / _scale, xAcc / _scale), ForceMode.Acceleration);
    }
    private void CalculateDistance()
    {
        if (Type == CinematicType.MRU)
        {
            _distanceFromStart = Formulary2.Formula_mru_x(_velX, TimeMoving);
        }
        else if (Type == CinematicType.MRUV)
        {
            _distanceFromStart = Formulary2.Formula_4(_velX, _accX, TimeMoving);
            // _distanceFromStart = _velX * TimeMoving + _accX * Mathf.Pow(TimeMoving, 2) / 2;
        }
    }
    private void CalculateActualVelocity()
    {
        switch (Type)
        {
            case CinematicType.MRU:
                _actualVelX = _velX;
                break;
            case CinematicType.MRUV:
                _actualVelX = Formulary2.Formula_1(_velX, _accX, TimeMoving);
                // _actualVelX = _velX + _accX * TimeMoving;
                break;
            default:
                Debug.Log("No se encontro el tipo de ejercicio");
                break;
        }
    }
    private void CalculateTime()
    {
        switch (Type)
        {
            case CinematicType.MRU:
                _timeMoving = Formulary2.Formula_mru_t(_maxVirtualDistance, _velX);
                // _timeMoving = _maxVirtualDistance / VelX;
                break;
            case CinematicType.MRUV:
                _timeMoving = Formulary2.Formula_4_t(_maxVirtualDistance, _velX, _accX) * 1000;
                break;
            default:
                Debug.Log("No se encontro el tipo de ejercicio");
                break;
        }
    }
    public void StopMovement()
    {
        _isMoving = false;
        _rb.velocity = Vector3.zero;
        if (transform.localPosition.z >= _maxRealDistance)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _maxRealDistance);
        }
    }
    public void ResetAll(int seconds = 0)
    {
        StartCoroutine(ResetCar(seconds));
    }
    public IEnumerator ResetCar(int seconds)
    {
        if (seconds != 0)
        {
            yield return new WaitForSeconds(seconds);
        }
        StopMovement();
        _velX = 0;
        _velY = 0;
        _actualVelX = 0;
        _actualVelY = 0;
        _accX = 0;
        _accY = 0;
        _timeMoving = 0;
        _distanceFromStart = 0;
        transform.localPosition = _initialPos;
        transform.localEulerAngles = Vector3.zero;
    }
}

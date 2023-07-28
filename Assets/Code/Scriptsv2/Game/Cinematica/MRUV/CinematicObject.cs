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
    [SerializeField] private float _actualHeight;
    [SerializeField] private float _initialHeight;
    [SerializeField] private bool _isMoving = false;
    private Vector3 _initialPos;
    private Quaternion _initialRot;
    public CinematicType Type;
    public float VelX => _velX;
    public float VelY => _velY;
    public float AccX => _accX;
    public float AccY => _accY;
    public float ActualVelX => _actualVelX;
    public float ActualVelY => _actualVelY;
    public float TimeMoving => _timeMoving / 1000;
    public float DistanceFromStart => _distanceFromStart;
    public float MaxVirtualDistance => _maxVirtualDistance;
    public float ActualHeight => _actualHeight;
    public bool IsMoving => _isMoving;
    // Events
    public event Action OnFinishMove;
    public event Action OnStartMove;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _scale = _maxVirtualDistance / _maxRealDistance;
        _initialPos = transform.localPosition;
        _initialRot = transform.localRotation;
        SetHorizontalVel(0, 0);
        SetVerticalVel(0, 0);
    }
    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _timeMoving += 10;
            if (Type == CinematicType.MRUV || Type == CinematicType.MRU)
            {
                if (_distanceFromStart >= _maxVirtualDistance)
                {
                    _distanceFromStart = _maxVirtualDistance;
                    _timeMoving = CalculateTime(_distanceFromStart) * 1000;
                    _actualVelX = CalculateActualVelocityX(TimeMoving);
                    StopMovement();
                }
                else
                {
                    if (_accX != 0 || _accY != 0)
                    {
                        Accelerate(_accX, _accY);
                    }
                    CalculateDistance();
                    _actualVelX = CalculateActualVelocityX(TimeMoving);
                }
            }
            else if (Type == CinematicType.Parabolico)
            {
                if (transform.localPosition.y < _initialHeight)
                {
                    CalculateDistance();
                    _timeMoving = CalculateTime(_distanceFromStart) * 1000;
                    _actualVelX = CalculateActualVelocityX(TimeMoving);
                    _actualVelY = CalculateActualVelocityY(TimeMoving);
                    StopMovement();
                }
                else
                {
                    if (_accX != 0 || _accY != 0)
                    {
                        Accelerate(_accX, _accY);
                    }
                    CalculateDistance();
                    _actualVelX = CalculateActualVelocityX(TimeMoving);
                    _actualVelY = CalculateActualVelocityY(TimeMoving);
                }
            }
        }

    }
    public void StartMovement()
    {
        _timeMoving = 0;
        _initialHeight = transform.localPosition.y;
        _rb.velocity = Vector3.zero;
        _rb.velocity += transform.forward * (_velX / _scale);
        _rb.velocity += transform.up * (_velY / _scale);
        _isMoving = true;
        OnStartMove?.Invoke();
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
        _rb.AddRelativeForce(new Vector3(0, -yAcc / _scale, xAcc / _scale), ForceMode.Acceleration);
    }
    private void CalculateDistance()
    {
        switch (Type)
        {
            case CinematicType.MRU:
                _distanceFromStart = Formulary2.Formula_mru_x(_velX, TimeMoving);
                break;
            case CinematicType.MRUV:
                _distanceFromStart = Formulary2.Formula_4(_velX, _accX, TimeMoving);
                break;
            case CinematicType.Parabolico:
                _distanceFromStart = Formulary2.Formula_mru_x(_velX, TimeMoving);
                // _actualHeight = Formulary2.Formula_4(_velY, _accY, TimeMoving);
                _actualHeight = Formulary2.Altura(x: _velX, y: _velY, dist: _distanceFromStart, grav: _accY);
                break;
            default:
                break;
        }
        // if (Type == CinematicType.MRU || Type == CinematicType.Parabolico)
        // {
        //     _distanceFromStart = Formulary2.Formula_mru_x(_velX, TimeMoving);
        // }
        // else if (Type == CinematicType.MRUV)
        // {
        //     _distanceFromStart = Formulary2.Formula_4(_velX, _accX, TimeMoving);
        // }
    }
    public float CalculateActualVelocityX(float time)
    {
        float actualVel = 0;
        switch (Type)
        {
            case CinematicType.MRU:
                actualVel = _velX;
                break;
            case CinematicType.MRUV:
                actualVel = Formulary2.Formula_1(_velX, _accX, time);
                // _actualVelX = _velX + _accX * TimeMoving;
                break;
            case CinematicType.Parabolico:
                actualVel = _velX;
                break;
            default:
                Debug.Log("No se encontro el tipo de ejercicio");
                break;
        }
        return actualVel;
    }
    public float CalculateActualVelocityY(float time)
    {
        float actualVel = 0;
        switch (Type)
        {
            case CinematicType.Parabolico:
                actualVel = Formulary2.Formula_1(_velY, _accY, time);
                break;
            default:
                Debug.Log("No se encontro el tipo de ejercicio");
                break;
        }
        return actualVel;
    }
    public float CalculateTime(float distance)
    {
        float calculatedTime = 0;
        switch (Type)
        {
            case CinematicType.MRU:
                calculatedTime = Formulary2.Formula_mru_t(distance, _velX);
                // _timeMoving = _maxVirtualDistance / VelX;
                break;
            case CinematicType.MRUV:
                if (_accX == 0)
                {
                    calculatedTime = Formulary2.Formula_mru_t(distance, _velX);
                }
                else
                {
                    calculatedTime = Formulary2.Formula_4_t(distance, _velX, _accX);
                }
                break;
            case CinematicType.Parabolico:
                calculatedTime = Formulary2.Formula_mru_t(distance, _velX);
                break;
            default:
                Debug.Log("No se encontro el tipo de ejercicio");
                break;
        }
        return calculatedTime;
    }
    public void StopMovement()
    {
        _isMoving = false;
        _rb.velocity = Vector3.zero;
        if (transform.localPosition.z >= _maxRealDistance)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _maxRealDistance);
        }
        OnFinishMove?.Invoke();
    }
    public void ResetAll(int seconds = 0)
    {
        StartCoroutine(ResetCar(seconds));
    }
    private IEnumerator ResetCar(int seconds)
    {
        if (seconds != 0)
        {
            yield return new WaitForSeconds(seconds);
        }
        // StopMovement();
        _velX = 0;
        _velY = 0;
        _actualVelX = 0;
        _actualVelY = 0;
        _accX = 0;
        _accY = 0;
        _timeMoving = 0;
        _distanceFromStart = 0;
        transform.localPosition = _initialPos;
        transform.localRotation = _initialRot;
    }
}

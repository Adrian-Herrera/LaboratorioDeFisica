using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints : MonoBehaviour
{
    [SerializeField] private Station _station;
    [SerializeField] private CinematicObject _cinematicObject;
    [SerializeField] private float _nearPoint;
    // [SerializeField] private List<float> _distancePoints = new();
    [SerializeField] private List<VariableUnity> _distancePoints = new();
    private readonly Dictionary<VariableUnity, ControlPointsVariables> _pointsInfo = new();
    private int pointIndex;
    public List<VariableUnity> DistancePoints => _distancePoints;
    // EVENTS
    public event Action OnChangeList;
    private void Awake()
    {
        _station = GetComponent<Station>();
    }
    private void OnEnable()
    {
        _station.OnStartStation += Init;
        _cinematicObject.OnFinishMove += LogPoints;
        _cinematicObject.OnStartMove += ResetPoints;
    }
    private void OnDisable()
    {
        _station.OnStartStation -= Init;
        _cinematicObject.OnFinishMove -= LogPoints;
        _cinematicObject.OnStartMove -= ResetPoints;
    }
    public void Init()
    {
        _distancePoints.Clear();
        _pointsInfo.Clear();
        AddControlPointAt(_cinematicObject.MaxVirtualDistance);
        // AddControlPointAt(_cinematicObject.MaxVirtualDistance / 2);
        // AddControlPointAt(25);
        // AddControlPointAt(75);
        _nearPoint = _distancePoints[pointIndex]._value;
    }
    private void FixedUpdate()
    {
        if (_cinematicObject.IsMoving == false) return;
        if (pointIndex < _distancePoints.Count)
        {
            if (_cinematicObject.DistanceFromStart >= _nearPoint)
            {
                SavePoint(_nearPoint);
                pointIndex++;
                if (pointIndex < _distancePoints.Count)
                {
                    _nearPoint = _distancePoints[pointIndex]._value;
                }
            }
        }
        else
        {
            Debug.Log("Control point: detener auto");
            _cinematicObject.StopMovement();
        }
    }
    private void ResetPoints()
    {
        _pointsInfo.Clear();
        pointIndex = 0;
        _nearPoint = _distancePoints[pointIndex]._value;
    }
    public void SavePoint(float distance)
    {
        // Create variable point
        ControlPointsVariables controlPoint;
        controlPoint.distance = distance;
        controlPoint.time = _cinematicObject.CalculateTime(distance);
        controlPoint.velocity = _cinematicObject.CalculateActualVelocity(controlPoint.time);
        // Create unityVar
        VariableUnity varUnity = new(BaseVariable.Distancia)
        {
            _value = distance
        };
        // Save point
        _pointsInfo.Add(varUnity, controlPoint);
    }
    public ControlPointsVariables? GetPoint(float distance)
    {
        VariableUnity varUnity = _distancePoints.Find(e => e._value == distance);
        if (varUnity != null)
        {
            return _pointsInfo[varUnity];
        }
        else
        {
            Debug.Log("No existe el punto de control");
            return null;
        }
    }
    public bool AddControlPointAt(float value)
    {
        int indexValue = _distancePoints.FindIndex(e => e._value == value);
        if (indexValue < 0)
        {
            VariableUnity varUnity = new(BaseVariable.Distancia)
            {
                _value = value
            };
            _distancePoints.Add(varUnity);
            SortDistancePointsList();
            OnChangeList?.Invoke();
            // _distancePoints.Sort();
            return true;
        }
        else
        {
            Debug.Log("Ya existe un punto de control en esa posición");
            return false;
        }
    }
    public bool DeleteControlPointAt(float value)
    {
        VariableUnity oldVariable = _distancePoints.Find(e => e._value == value);
        if (oldVariable != null)
        {
            _distancePoints.Remove(oldVariable);
            OnChangeList?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
        // SortDistancePointsList();
    }
    private void SortDistancePointsList()
    {
        _distancePoints = _distancePoints.OrderBy(o => o._value).ToList();
    }
    private void LogPoints()
    {
        Debug.Log("Puntos de control");
        foreach (var controlPoint in _pointsInfo)
        {
            Debug.Log($"En la posición {controlPoint.Key} : d={controlPoint.Value.distance}, t={controlPoint.Value.time}, v={controlPoint.Value.velocity}");
        }
    }
    public struct ControlPointsVariables
    {
        public float distance;
        public float time;
        public float velocity;
    }
}

using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints : MonoBehaviour
{
    private Station _station;
    private CinematicObject _cinematicObject;
    [SerializeField] private float _nearPoint;
    // [SerializeField] private List<float> _distancePoints = new();
    [SerializeField] private List<VariableUnity> _distancePoints = new();
    private readonly Dictionary<VariableUnity, List<VariableUnity>> _pointsInfo = new();
    private int pointIndex;
    public List<VariableUnity> DistancePoints => _distancePoints;
    public Dictionary<VariableUnity, List<VariableUnity>> PointsInfo => _pointsInfo;
    // EVENTS
    public event Action OnChangeList;
    private void Awake()
    {
        _station = GetComponent<Station>();
        _cinematicObject = _station.CinematicObject;
    }
    private void OnEnable()
    {
        if (_station == null) return;
        _station.OnStartStation += Init;
        _cinematicObject.OnFinishMove += LogPoints;
        _cinematicObject.OnStartMove += ResetPoints;
    }
    private void OnDisable()
    {
        if (_station == null) return;
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
        _nearPoint = _distancePoints[pointIndex].Value;
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
                    _nearPoint = _distancePoints[pointIndex].Value;
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
        _nearPoint = _distancePoints[pointIndex].Value;
    }
    public void SavePoint(float distance)
    {
        // Create variable point
        VariableUnity distancePoint = new(BaseVariable.Distancia, distance);
        float tiempo = _cinematicObject.CalculateTime(distance);
        float velocidad = _cinematicObject.CalculateActualVelocityX(tiempo);
        List<VariableUnity> datos = new()
        {
            new(BaseVariable.Tiempo, tiempo),
            new(BaseVariable.Velocidad, velocidad)
        };
        // Save point
        _pointsInfo.Add(distancePoint, datos);
    }
    // public ControlPointsVariables? GetPoint(float distance)
    // {
    //     VariableUnity varUnity = _distancePoints.Find(e => e.Value == distance);
    //     if (varUnity != null)
    //     {
    //         return _pointsInfo[varUnity];
    //     }
    //     else
    //     {
    //         Debug.Log("No existe el punto de control");
    //         return null;
    //     }
    // }
    public bool AddControlPointAt(float value)
    {
        int indexValue = _distancePoints.FindIndex(e => e.Value == value);
        if (indexValue < 0)
        {
            VariableUnity varUnity = new(BaseVariable.Distancia)
            {
                Value = value
            };
            _distancePoints.Add(varUnity);
            SortDistancePointsList();
            OnChangeList?.Invoke();
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
        VariableUnity oldVariable = _distancePoints.Find(e => e.Value == value);
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
    }
    private void SortDistancePointsList()
    {
        _distancePoints = _distancePoints.OrderBy(o => o.Value).ToList();
    }
    private void LogPoints()
    {
        Debug.Log("Puntos de control");
        foreach (var controlPoint in _pointsInfo)
        {
            Debug.Log($"En la posición {controlPoint.Key.Value}:");
            foreach (VariableUnity item in controlPoint.Value)
            {
                Debug.Log($"    {item.TipoVariable.Nombre}: = {item.Value}");
            }
        }
    }
}

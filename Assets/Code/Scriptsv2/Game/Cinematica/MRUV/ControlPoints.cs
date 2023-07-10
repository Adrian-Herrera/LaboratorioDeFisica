using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints : MonoBehaviour
{
    public static ControlPoints Instance;
    // private Station _station;
    [SerializeField] private CinematicObject _cinematicObject;
    [SerializeField] private float _nearPoint;
    [SerializeField] private List<VariableUnity> _distancePoints = new();
    private readonly Dictionary<VariableUnity, List<VariableUnity>> _pointsInfo = new();
    private int pointIndex;
    public List<VariableUnity> DistancePoints => _distancePoints;
    public Dictionary<VariableUnity, List<VariableUnity>> PointsInfo => _pointsInfo;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        Player.Instance.OnExitStation += CleanCinematicObject;
    }
    public void SetCinematicObject(CinematicObject cObject)
    {
        _cinematicObject = cObject;
        _cinematicObject.OnFinishMove += LogPoints;
        _cinematicObject.OnStartMove += ResetPoints;
    }
    private void CleanCinematicObject()
    {
        if (_cinematicObject != null)
        {
            _cinematicObject.OnFinishMove -= LogPoints;
            _cinematicObject.OnStartMove -= ResetPoints;
            _cinematicObject = null;
        }
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
        if (_cinematicObject == null) return;
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

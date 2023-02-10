using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints
{
    [SerializeField] private CinematicObject _cinematicObject;
    private readonly Dictionary<float, ExerciseTemplate> _pointsInfo = new();
    public void SavePoint(float distance, ExerciseTemplate exercise)
    {
        if (!_pointsInfo.ContainsKey(distance))
        {
            _pointsInfo.Add(distance, exercise);
        }
    }
    public ExerciseTemplate GetPoint(float distance)
    {
        if (_pointsInfo.ContainsKey(distance))
        {
            if (_pointsInfo[distance] != null)
            {
                return _pointsInfo[distance];
            }
            else
            {
                Debug.Log("No existen datos guardados");
                return null;
            }
        }
        else
        {
            Debug.Log("No existe el punto de control");
            return null;
        }
    }
}

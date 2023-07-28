using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LiveObjectData : MonoBehaviour
{
    [SerializeField] private CinematicObject _cinematicObject;
    [SerializeField] private TableroDato _velX;
    [SerializeField] private TableroDato _accX;
    [SerializeField] private TableroDato _dist;
    [SerializeField] private TableroDato _time;
    // Parabolic movement
    [SerializeField] private TableroDato _velY;
    [SerializeField] private TableroDato _accY;
    [SerializeField] private TableroDato _altura;

    private IEnumerator CoRegisterData;
    private void Start()
    {
        CoRegisterData = RegisterData();
        _cinematicObject.OnStartMove += StartRegister;
        _cinematicObject.OnFinishMove += StopRegister;
    }
    private void StartRegister()
    {
        StartCoroutine(CoRegisterData);
    }
    private void StopRegister()
    {
        StopCoroutine(CoRegisterData);
        ShowInfo();
    }
    private void ShowInfo()
    {
        _velX.UpdateValue(_cinematicObject.ActualVelX.ToString("F2"));
        _accX.UpdateValue(_cinematicObject.AccX.ToString("F2"));
        _dist.UpdateValue(_cinematicObject.DistanceFromStart.ToString("F2"));
        _time.UpdateValue(_cinematicObject.TimeMoving.ToString("F2"));
        if (_velY.gameObject.activeSelf)
        {
            _velY.UpdateValue(_cinematicObject.ActualVelY.ToString("F2"));
        }
        if (_accY.gameObject.activeSelf)
        {
            _accY.UpdateValue(_cinematicObject.AccY.ToString("F2"));
        }
        if (_altura.gameObject.activeSelf)
        {
            _altura.UpdateValue(_cinematicObject.ActualHeight.ToString("F2"));
        }
    }
    private IEnumerator RegisterData()
    {
        while (true)
        {
            ShowInfo();
            yield return new WaitForSeconds(0.1f);
        }
    }
}

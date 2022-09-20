using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorManager : MonoBehaviour
{
    public static MainObject _selectedObject;
    [SerializeField] private MainObject[] _mainObjects;
    [SerializeField] private ObjectProperties _objectProperties;

    private void Start()
    {
        GlobalInfo.Init();
        Formulary.Instance.Init();
        _selectedObject = _mainObjects[0];
        _selectedObject.Init();
        Playground.Instance.Init();
        _objectProperties.Init();
    }


}

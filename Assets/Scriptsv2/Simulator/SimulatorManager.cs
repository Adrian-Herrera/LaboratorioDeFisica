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
        _selectedObject = _mainObjects[0];
        _objectProperties.Init();
    }


}

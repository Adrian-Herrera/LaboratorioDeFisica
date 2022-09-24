using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorManager : MonoBehaviour
{
    public static MainObject _selectedObject;
    [SerializeField] private MainObject[] _mainObjects;
    [SerializeField] private ObjectProperties _objectProperties;

    [SerializeField] private Sprite _car;
    [SerializeField] private Sprite _ball;

    private void Start()
    {
        StartCoroutine(AllInit());
    }
    IEnumerator AllInit()
    {

        yield return StartCoroutine(GlobalInfo.Instance.Init());
        Formulary.Instance.Init();
        _selectedObject = _mainObjects[0];
        _selectedObject.Init();
        Playground.Instance.Init();
        _objectProperties.Init();
    }


}

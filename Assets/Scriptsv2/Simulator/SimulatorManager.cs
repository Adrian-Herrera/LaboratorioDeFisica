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
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        if (LevelManager.Instance.temaId == 2 || LevelManager.Instance.temaId == 1)
        {
            _camera.transform.position = new Vector3(15, 0, -10);
        }
        else if (LevelManager.Instance.temaId == 3)
        {
            _camera.transform.position = new Vector3(5.5f, 5.5f, -10);
        }
        else if (LevelManager.Instance.temaId == 4)
        {
            _camera.transform.position = new Vector3(15f, 5.5f, -10);
        }
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

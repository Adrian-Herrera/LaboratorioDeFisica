using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DinamicData : MonoBehaviour
{
    // [SerializeField] private Object3d[] _objects;
    // Templates
    [SerializeField] private VariableInput _input;
    [SerializeField] private ShowVariable _show;
    // Containers
    [SerializeField] private Transform _containerInfo;
    // Lists
    private readonly List<GameObject> _listGameObject = new();

    public void Init(Object3d[] _objects)
    {
        Helpers.ClearListContent(_listGameObject);
        foreach (Object3d item in _objects)
        {
            VariableInput vi = Instantiate(_input, _containerInfo);
            vi.Init(item._masa, true);
            _listGameObject.Add(vi.gameObject);
            vi.GetComponent<RectTransform>().sizeDelta = new Vector2(vi.GetComponent<RectTransform>().sizeDelta.x, 40);

            InstantiateReadVariable(item._tension);
            InstantiateReadVariable(item._peso);
            InstantiateReadVariable(item._normal);
            InstantiateReadVariable(item._acc);
            InstantiateReadVariable(item._vel);
        }
    }
    private void InstantiateReadVariable(VariableUnity input)
    {
        ShowVariable vi = Instantiate(_show, _containerInfo);
        vi.Init(input);
        _listGameObject.Add(vi.gameObject);
        vi.GetComponent<RectTransform>().sizeDelta = new Vector2(vi.GetComponent<RectTransform>().sizeDelta.x, 40);

    }
}

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

    public void Init(DynamicObject[] _objects)
    {
        Helpers.ClearListContent(_listGameObject);
        foreach (DynamicObject item in _objects)
        {
            VariableInput vi = Instantiate(_input, _containerInfo);
            vi.Init(item._masa, true);
            _listGameObject.Add(vi.gameObject);
            vi.GetComponent<RectTransform>().sizeDelta = new Vector2(vi.GetComponent<RectTransform>().sizeDelta.x, 40);
            foreach (VariableUnity force in item.YForces)
            {
                InstantiateReadOnlyVariable(force);
            }
            InstantiateReadOnlyVariable(item._acc);
            InstantiateReadOnlyVariable(item._vel);
        }
    }
    private void InstantiateReadOnlyVariable(VariableUnity input)
    {
        ShowVariable vi = Instantiate(_show, _containerInfo);
        vi.Init(input);
        _listGameObject.Add(vi.gameObject);
        vi.GetComponent<RectTransform>().sizeDelta = new Vector2(vi.GetComponent<RectTransform>().sizeDelta.x, 40);

    }
}

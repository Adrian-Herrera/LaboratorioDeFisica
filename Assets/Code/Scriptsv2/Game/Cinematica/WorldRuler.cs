using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldRuler : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private RulerSeparationLine _separationLine;
    [SerializeField] private GameObject _separationLineContainer;
    [SerializeField] private RectTransform _objectPositionArrow;
    [Header("Variables")]
    [SerializeField] private CinematicObject _object;
    // [SerializeField] private float _rate;
    private RectTransform _rt;
    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }
    private void Start()
    {
        float temp = 0;
        while (temp <= 10)
        {
            RulerSeparationLine sepLine = Instantiate(_separationLine, _separationLineContainer.transform);
            sepLine.Init(Mathf.Lerp(0, _object.MaxVirtualDistance, temp / 10));
            sepLine.SetPosition(Mathf.Lerp(0, _rt.sizeDelta.x, temp / 10));
            temp += 1;
        }
    }
    private void FixedUpdate()
    {
        if (_object.IsMoving)
        {
            _objectPositionArrow.anchoredPosition = new Vector2(Mathf.Lerp(0, _rt.sizeDelta.x, _object.DistanceFromStart / _object.MaxVirtualDistance), -10);
        }
    }
}

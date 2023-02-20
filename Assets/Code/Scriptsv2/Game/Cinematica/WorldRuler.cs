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
        for (float i = 0; i <= 10; i++)
        {
            RulerSeparationLine sepLine = Instantiate(_separationLine, _separationLineContainer.transform);
            sepLine.Init(Mathf.Lerp(0, _object.MaxVirtualDistance, i / 10));
            sepLine.SetPosition(Mathf.Lerp(0, _rt.sizeDelta.x, i / 10));
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

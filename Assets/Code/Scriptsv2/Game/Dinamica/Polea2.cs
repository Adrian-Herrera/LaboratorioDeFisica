using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polea2 : MonoBehaviour
{
    [SerializeField] private Transform _leftPoint, _rightPoint, _middlePoint;
    public Transform LeftPoint => _leftPoint;
    public Transform RightPoint => _rightPoint;
    public Transform MiddlePoint => _middlePoint;


}

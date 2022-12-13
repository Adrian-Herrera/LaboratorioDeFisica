using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polea3d : MonoBehaviour
{
    [SerializeField] private Object3d _object1;
    [SerializeField] private Object3d _object2;
    [SerializeField] private float _acc;
    [SerializeField] private float _tension;
    private float masa1, masa2;
    public bool floor1, floor2;
    // Start is called before the first frame update
    void Start()
    {
        masa1 = _object1.Masa;
        masa2 = _object2.Masa;
        floor1 = _object1._onGround;
        floor2 = _object2._onGround;
        CalculateTension();
    }

    // Update is called once per frame
    void Update()
    {
        if (masa1 != _object1.Masa || masa2 != _object2.Masa)
        {
            CalculateTension();
        }
        if (floor1 != _object1._onGround || floor2 != _object2._onGround)
        {
            _object2.StopMovement = _object1._onGround;
            _object1.StopMovement = _object2._onGround;
            floor1 = _object1._onGround;
            floor2 = _object2._onGround;
        }
    }
    private void CalculateTension()
    {
        masa1 = _object1.Masa;
        masa2 = _object2.Masa;
        _acc = (_object2.Masa * 10 - _object1.Masa * 10) / (_object1.Masa + _object2.Masa);
        _tension = _object1.Masa * Mathf.Abs(_acc) + _object1.Masa * 10;
        if (_acc < 0)
        {
            _tension = _object1.Masa * 10 - (_object1.Masa * Mathf.Abs(_acc));
        }
        _object1.SetTension(_tension);
        _object2.SetTension(_tension);
    }
}

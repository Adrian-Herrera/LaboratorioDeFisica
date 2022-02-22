using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[CreateAssetMenu(fileName = "CarSO", menuName = "ScriptableObject/Points/CarSO")]

public class CarSO : BasePointSO
{
    [SerializeField] protected string[] ExtraFieldsNames;
    protected override void OnEnable()
    {
        base.OnEnable();
        ExtraFieldsNames = new string[] { "Tiempo Total", "Distancia Total" };
    }
    public override string[] getNames()
    {
        return ExtraFieldsNames;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BallSO", menuName = "ScriptableObject/Points/BallSO")]

public class BallSO : BasePointSO
{
    [SerializeField] protected string[] ExtraFieldsNames;
    protected override void OnEnable()
    {
        base.OnEnable();
        ExtraFieldsNames = new string[] { "h-max", "Ho", "Tiempo Subida", "Tiempo Bajada" };
    }
    public override string[] getNames()
    {
        return ExtraFieldsNames;
    }
    public override void setDefaultValues()
    {
        for (int i = 0; i < Datos.GetLength(0); i++)
        {
            Datos[i, 2].value = 9.8f;
        }
    }

    public bool isRising()
    {
        return Datos[0, 2].value < 0;
    }
}

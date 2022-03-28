using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ParabolicBallSO", menuName = "ScriptableObject/Points/ParabolicBallSO")]
public class ParabolicBallSO : BasePointSO
{
    [SerializeField] protected string[] ExtraFieldsNames;
    protected override void OnEnable()
    {
        base.OnEnable();
        Datos = new Field[3, 12]; // { Voy, Vy, g, y, t, Vox, x, V , "Angulo", "y-max", "tv", "x-max"}
        ExtraFieldsNames = new string[] { "Trayectoria" };
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

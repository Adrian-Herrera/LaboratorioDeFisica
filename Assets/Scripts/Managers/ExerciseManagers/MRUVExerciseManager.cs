using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRUVExerciseManager : ExerciseManager
{
    private MRUVFormulary Formulary;
    private float Vo, Vf, a, x, t, TiempoEncuentro;
    protected override void Awake()
    {
        current = this;
        Formulary = GetComponent<MRUVFormulary>();
    }
    protected override void Start()
    {
        Debug.Log("MRUVExerciseManager active");
    }
    protected override void GetFieldData(int segment)
    {
        Vo = BasePointSO[0].Datos[segment, 0].value;
        Vf = BasePointSO[0].Datos[segment, 1].value;
        a = BasePointSO[0].Datos[segment, 2].value;
        x = BasePointSO[0].Datos[segment, 3].value;
        t = BasePointSO[0].Datos[segment, 4].value;
    }
    public override void SetUnit(int unit) // refactorizar
    {
        base.SetUnit(unit);
        int segment = SelectedSegment.SegmentID;
        if (SelectedUnit == 0)
        {
            if (BasePointSO[0].Datos[segment, 0].value != 0) BasePointSO[0].Datos[segment, 0].value = MtsToKmh(BasePointSO[0].Datos[segment, 0].value, false);
            if (BasePointSO[0].Datos[segment, 1].value != 0) BasePointSO[0].Datos[segment, 1].value = MtsToKmh(BasePointSO[0].Datos[segment, 1].value, false);
            if (BasePointSO[0].Datos[segment, 2].value != 0) BasePointSO[0].Datos[segment, 2].value = Mts2ToKmh2(BasePointSO[0].Datos[segment, 2].value, false);
            if (BasePointSO[0].Datos[segment, 3].value != 0) BasePointSO[0].Datos[segment, 3].value = MetersToKilometers(BasePointSO[0].Datos[segment, 3].value, false);
            if (BasePointSO[0].Datos[segment, 4].value != 0) BasePointSO[0].Datos[segment, 4].value = SecondsToHours(BasePointSO[0].Datos[segment, 4].value, false);
        }
        else if (SelectedUnit == 1)
        {
            if (BasePointSO[0].Datos[segment, 0].value != 0) BasePointSO[0].Datos[segment, 0].value = MtsToKmh(BasePointSO[0].Datos[segment, 0].value);
            if (BasePointSO[0].Datos[segment, 1].value != 0) BasePointSO[0].Datos[segment, 1].value = MtsToKmh(BasePointSO[0].Datos[segment, 1].value);
            if (BasePointSO[0].Datos[segment, 2].value != 0) BasePointSO[0].Datos[segment, 2].value = Mts2ToKmh2(BasePointSO[0].Datos[segment, 2].value);
            if (BasePointSO[0].Datos[segment, 3].value != 0) BasePointSO[0].Datos[segment, 3].value = MetersToKilometers(BasePointSO[0].Datos[segment, 3].value);
            if (BasePointSO[0].Datos[segment, 4].value != 0) BasePointSO[0].Datos[segment, 4].value = SecondsToHours(BasePointSO[0].Datos[segment, 4].value);
        }
    }
    public override void SearchFormula(int Variable)
    {
        GetFieldData(SelectedSegment.SegmentID);
        int op = HeaderManager.current.ActiveProblem.Incognita;
        switch (Variable)
        {
            case 3:
                SelectedSegment.childFields[op].value = Formulary.Formula_1(Vo, Vf, a, t, op);
                break;
            case 2:
                SelectedSegment.childFields[op].value = Formulary.Formula_2(Vo, Vf, x, t, op);
                break;
            case 4:
                SelectedSegment.childFields[op].value = Formulary.Formula_3(Vo, Vf, a, x, op);
                break;
            case 1:
                StartCoroutine(Formulary.Formula_4(Vo, a, x, t, op, SelectedSegment.childFields[op]));
                break;
            case 0:
                StartCoroutine(Formulary.Formula_5(Vf, a, x, t, op, SelectedSegment.childFields[op]));
                break;
            default:
                Debug.Log("No se uso ninguna formula");
                break;
        }
    }
    public override void CheckEveryTime(BasePointSO car)
    {
        Formulary.checkTime(car);
        Formulary.checkDistance(car);
    }
    public override void PreFormula()
    {

    }

}

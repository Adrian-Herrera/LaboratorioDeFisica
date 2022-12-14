using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaLibreExerciseManager : ExerciseManager
{
    public CaidaLibreFormulary Formulary;
    private float Vo, Vf, a, x, t, ho, hmax;
    protected override void Awake()
    {
        base.Awake();
        Formulary = GetComponent<CaidaLibreFormulary>();
    }
    protected override void Start()
    {
        Debug.Log("CaidaLibreExerciseManager active");
        Formulary._sa = "g";
    }
    protected override void GetFieldData(int segment)
    {
        Vo = BasePointSO[0].Datos[segment, 0].value;
        Vf = BasePointSO[0].Datos[segment, 1].value;
        a = BasePointSO[0].Datos[segment, 2].value;
        x = BasePointSO[0].Datos[segment, 3].value;
        t = BasePointSO[0].Datos[segment, 4].value;

        ho = BasePointSO[0].ExtraFields["Ho"].value;
        hmax = BasePointSO[0].ExtraFields["h-max"].value;
    }
    public override void SearchFormula(int Variable)
    {
        GetFieldData(SelectedSegment.SegmentID);
        int op = HeaderManager.current.ActiveProblem.Incognita;
        PreFormula();
        switch (Variable)
        {
            case 3:
                SelectedSegment.childFields[op].value = BasePointSO[0].ExtraFields["Ho"].status ? Formulary.Formula_1(Vo, Vf, a, t, op) + BasePointSO[0].ExtraFields["Ho"].value : Formulary.Formula_1(Vo, Vf, a, t, op);
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
    public override void CheckEveryTime(BasePointSO basePointSO)
    {
        Formulary.AlturaInicial(basePointSO);
        Formulary.AlturaMaxima(basePointSO);
    }

    public override void PreFormula()
    {
        GetFieldData(SelectedSegment.SegmentID);
        if (!BasePointSO[0].Datos[0, 3].status && (BasePointSO[0].ExtraFields["Ho"].status && BasePointSO[0].ExtraFields["h-max"].status))
        {
            BasePointSO[0].Datos[0, 3].value = hmax - ho;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaLibreExerciseManager : ExerciseManager
{
    public CaidaLibreFormulary Formulary;
    private float Vo, Vf, a, x, t, TiempoEncuentro;
    protected override void Awake()
    {
        base.Awake();
        Formulary = GetComponent<CaidaLibreFormulary>();
    }
    protected override void Start()
    {
        Debug.Log("CaidaLibreExerciseManager active");
    }
    protected override void getFieldData(int segment)
    {
        Vo = Cars[0].Datos[segment, 0].value;
        Vf = Cars[0].Datos[segment, 1].value;
        a = Cars[0].Datos[segment, 2].value;
        x = Cars[0].Datos[segment, 3].value;
        t = Cars[0].Datos[segment, 4].value;
    }
    public override void searchFormula(int Variable)
    {
        getFieldData(SelectedSegment.SegmentID);
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
        // throw new System.NotImplementedException();
    }
}

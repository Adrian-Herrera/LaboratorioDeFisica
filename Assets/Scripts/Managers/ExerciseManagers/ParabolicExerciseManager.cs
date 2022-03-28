using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicExerciseManager : ExerciseManager
{
    public ParabolicFormulary Formulary;
    private float Vo, Vf, a, x, t, Vox, d, V, Angulo, yMax, tv, xMax, Trayectoria;
    protected override void Awake()
    {
        base.Awake();
        Formulary = GetComponent<ParabolicFormulary>();
    }
    protected override void Start()
    {
        Debug.Log("ParabolicExerciseManager active");
        Formulary._sa = "g";
    }
    protected override void getFieldData(int segment)
    {
        Vo = BasePointSO[0].Datos[segment, 0].value;
        Vf = BasePointSO[0].Datos[segment, 1].value;
        a = BasePointSO[0].Datos[segment, 2].value;
        x = BasePointSO[0].Datos[segment, 3].value;
        t = BasePointSO[0].Datos[segment, 4].value;

        Vox = BasePointSO[0].Datos[segment, 5].value;
        d = BasePointSO[0].Datos[segment, 6].value;
        V = BasePointSO[0].Datos[segment, 7].value;

        Angulo = BasePointSO[0].Datos[segment, 8].value;
        yMax = BasePointSO[0].Datos[segment, 9].value;
        tv = BasePointSO[0].Datos[segment, 10].value;
        xMax = BasePointSO[0].Datos[segment, 11].value;
        Trayectoria = BasePointSO[0].ExtraFields["Trayectoria"].value;
    }
    public override void searchFormula(int Variable)
    {
        getFieldData(SelectedSegment.SegmentID);
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
            case 8:
                SelectedSegment.childFields[op].value = Formulary.Angulo(BasePointSO[0]);
                break;
            case 9:
                SelectedSegment.childFields[op].value = Formulary.AlturaMaxima(BasePointSO[0]);
                break;
            case 10:
                SelectedSegment.childFields[op].value = Formulary.TiempoVuelo(BasePointSO[0]);
                break;
            case 11:
                SelectedSegment.childFields[op].value = Formulary.AlcanceMaximo(BasePointSO[0]);
                break;
            default:
                Debug.Log("No se uso ninguna formula");
                break;
        }
    }
    public override void CheckEveryTime(BasePointSO basePointSO)
    {

    }

    public override void PreFormula()
    {
        getFieldData(SelectedSegment.SegmentID);
        // if (!BasePointSO[0].Datos[0, 3].status && (BasePointSO[0].ExtraFields["Ho"].status && BasePointSO[0].ExtraFields["h-max"].status))
        // {
        //     BasePointSO[0].Datos[0, 3].value = hmax - ho;
        // }
    }
}

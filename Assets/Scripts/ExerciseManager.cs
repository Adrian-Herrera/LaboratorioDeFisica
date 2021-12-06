using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseManager : MonoBehaviour
{
    Formulary Formulary = new Formulary();
    public static ExerciseManager current;

    public CarSO[] Cars;
    private float Vo, Vf, a, x, t;
    public enum Variables { Vo, Vf, a, x, t };
    public ProblemsSO ActiveProblem;

    public delegate void problems(string s);
    public Dictionary<string, problems> equations = new Dictionary<string, problems>();
    private void Awake()
    {
        current = this;

    }
    private void Start()
    {
        equations.Add("Vf", CalculateVelF);
        equations.Add("Vo", CalculateVelo);
        equations.Add("a", CalculateAcc);
        equations.Add("x", CalculateDist);
        equations.Add("t", CalculateTime);
    }
    private void getFieldData(int segment)
    {
        Vo = Cars[0].Datos[segment, (int)Variables.Vo];
        Vf = Cars[0].Datos[segment, (int)Variables.Vf];
        a = Cars[0].Datos[segment, (int)Variables.a];
        x = Cars[0].Datos[segment, (int)Variables.x];
        t = Cars[0].Datos[segment, (int)Variables.t];
    }
    public void CalculateVelF(string emptyVariable)
    {
        getFieldData(Cars[0].selectedSegment);
        float res = Formulary.Vel_f(Vo, a, x, t, emptyVariable);
        Cars[0].Datos[Cars[0].selectedSegment, (int)Variables.Vf] = res;
    }
    public void CalculateVelo(string emptyVariable)
    {
        getFieldData(Cars[0].selectedSegment);
        float res = Formulary.Vel_o(Vf, a, x, t, emptyVariable);
        Cars[0].Datos[Cars[0].selectedSegment, (int)Variables.Vo] = res;
    }
    public void CalculateAcc(string emptyVariable)
    {
        getFieldData(Cars[0].selectedSegment);
        float res = Formulary.Acc(Vo, Vf, x, t, emptyVariable);
        Cars[0].Datos[Cars[0].selectedSegment, (int)Variables.a] = res;
    }
    public void CalculateDist(string emptyVariable)
    {
        getFieldData(Cars[0].selectedSegment);
        float res = Formulary.Dist(Vo, Vf, a, t, emptyVariable);
        Cars[0].Datos[Cars[0].selectedSegment, (int)Variables.x] = res;
    }
    public void CalculateTime(string emptyVariable)
    {
        getFieldData(Cars[0].selectedSegment);
        float res = Formulary.Time(Vo, Vf, a, x, emptyVariable);
        Cars[0].Datos[Cars[0].selectedSegment, (int)Variables.t] = res;
    }


}

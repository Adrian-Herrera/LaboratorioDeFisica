using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseManager : MonoBehaviour
{
    public static ExerciseManager current;

    public CarSO[] Cars;
    public enum Variables { Vo, Vf, a, x, t };
    public ProblemsSO ActiveProblem;

    public delegate void problems();
    public List<problems> equation = new List<problems>();
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        equation.Add(CalculateVelF);
        equation.Add(test2);

    }
    public void CalculateVelF()
    {
        float res = Formulary.current.VelF_1(Cars[0].Datos[0, (int)Variables.Vo], Cars[0].Datos[0, (int)Variables.a], Cars[0].Datos[0, (int)Variables.t]);
        Cars[0].Datos[0, (int)Variables.Vf] = res;

    }
    public void test2()
    {
        Debug.Log("Manager 2");
    }
}

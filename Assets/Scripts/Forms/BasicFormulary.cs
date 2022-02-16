using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFormulary
{
    public enum option
    {
        Vo, Vf, a, x, t
    }
    private float Formula_1(float Vo, float Vf, float a, float t, option op)
    {
        Debug.Log("Se uso Formula 1 con " + op);
        switch (op)
        {
            case option.Vf:
                return (Vo + (a * t));
            case option.Vo:
                return (Vf - a * t);
            case option.a:
                return (Vf - Vo) / t;
            case option.t:
                return (Vf - Vo) / a;
            default:
                return 0;
        }
    }
    private float Formula_2(float Vo, float Vf, float x, float t, option op)
    {
        Debug.Log("Se uso Formula 2 con " + op);
        switch (op)
        {
            case option.Vf:
                return (2 * x / t) - Vo;
            case option.Vo:
                return (2 * x / t) - Vf;
            case option.x:
                return (Vo + Vf) * t / 2;
            case option.t:
                return (2 * x) / (Vo + Vf);
            default:
                return 0;
        }
    }
    private float Formula_3(float Vo, float Vf, float a, float x, option op)
    {
        Debug.Log("Se uso Formula 3 con " + op);
        switch (op)
        {
            case option.Vf:
                return Mathf.Sqrt(Mathf.Pow(Vo, 2) + (2 * a * x));
            case option.Vo:
                return Mathf.Sqrt(Mathf.Pow(Vf, 2) - (2 * a * x));
            case option.x:
                return (Mathf.Pow(Vf, 2) - Mathf.Pow(Vo, 2)) / (2 * a);
            case option.a:
                return (Mathf.Pow(Vf, 2) - Mathf.Pow(Vo, 2)) / (2 * x);
            default:
                return 0;
        }
    }
    private float Formula_4(float Vo, float a, float x, float t, option op)
    {
        Debug.Log("Se uso Formula 4 con " + op);
        switch (op)
        {
            case option.Vo:
                return (x - (a * Mathf.Pow(t, 2) / 2)) / t;
            case option.x:
                return Vo * t + (a * Mathf.Pow(t, 2) / 2);
            case option.a:
                return (x - Vo * t) * 2 / Mathf.Pow(t, 2);
            case option.t:
            // return QuadraticFormula((a / 2), Vo, -x);
            default:
                return 0;
        }
    }
    private float Formula_5(float Vf, float a, float x, float t, option op)
    {
        Debug.Log("Se uso Formula 5 con " + op);
        switch (op)
        {
            case option.Vf:
                return (x + (a * Mathf.Pow(t, 2) / 2)) / t;
            case option.x:
                return Vf * t - (a * Mathf.Pow(t, 2) / 2);
            case option.a:
                return -((x - Vf * t) * 2 / Mathf.Pow(t, 2));
            case option.t:
            // return QuadraticFormula(-(a / 2), Vf, -x);
            default:
                return 0;
        }
    }
    private (float? x1, float? x2) QuadraticFormula(float a, float b, float c)
    {
        float x1, x2;
        float BeforeSquare = Mathf.Pow(b, 2) - (4 * a * c);
        Debug.Log("BeforeSquare: " + BeforeSquare);
        if (BeforeSquare == 0)
        {
            x1 = (-b + Mathf.Sqrt(BeforeSquare)) / (2 * a);
            x2 = x1;
            return (x1, x2);
        }
        else if (BeforeSquare > 0)
        {
            x1 = (-b + Mathf.Sqrt(BeforeSquare)) / (2 * a);
            x2 = (-b - Mathf.Sqrt(BeforeSquare)) / (2 * a);
            return (x1, x2);
        }
        else
        {
            return (null, null);
        }
    }

    // private void searchFormula(int ProblemOption)
    // {

    // }

    // public string searchFormula(System.Reflection.MethodInfo method)
    // {
    //     string retVal = string.Empty;

    //     if (method != null && method.GetParameters().Length > index)
    //         retVal = method.GetParameters()[index].Name;


    //     return retVal;
    // }
}

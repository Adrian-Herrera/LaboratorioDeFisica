using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRUVFormulary : MonoBehaviour
{
    public enum option
    {
        Vo, Vf, a, x, t
    }
    public float Formula_1(float Vo, float Vf, float a, float t, int op)
    {
        Debug.Log("Se uso Formula 1 con " + op);
        switch ((option)op)
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
    public float Formula_2(float Vo, float Vf, float x, float t, int op)
    {
        Debug.Log("Se uso Formula 2 con " + op);
        switch ((option)op)
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
    public float Formula_3(float Vo, float Vf, float a, float x, int op)
    {
        Debug.Log("Se uso Formula 3 con " + op);
        switch ((option)op)
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
    public IEnumerator Formula_4(float Vo, float a, float x, float t, int op, Field f)
    {
        Debug.Log("Se uso Formula 4 con " + op);
        switch ((option)op)
        {
            case option.Vo:
                f.value = (x - (a * Mathf.Pow(t, 2) / 2)) / t;
                break;
            case option.x:
                f.value = Vo * t + (a * Mathf.Pow(t, 2) / 2);
                break;
            case option.a:
                f.value = (x - Vo * t) * 2 / Mathf.Pow(t, 2);
                break;
            case option.t:
                yield return QuadraticFormula(f, (a / 2), Vo, -x);
                break;
            default:
                break;
        }
    }
    public IEnumerator Formula_5(float Vf, float a, float x, float t, int op, Field f)
    {
        Debug.Log("Se uso Formula 5 con " + op);
        switch ((option)op)
        {
            case option.Vf:
                f.value = (x + (a * Mathf.Pow(t, 2) / 2)) / t;
                yield return null;
                break;
            case option.x:
                f.value = Vf * t - (a * Mathf.Pow(t, 2) / 2);
                break;
            case option.a:
                f.value = -((x - Vf * t) * 2 / Mathf.Pow(t, 2));
                break;
            case option.t:
                yield return QuadraticFormula(f, -(a / 2), Vf, -x);
                break;
            default:
                break;
        }
    }
    public IEnumerator QuadraticFormula(Field x, float a, float b, float c)
    {
        float x1, x2;
        float BeforeSquare = Mathf.Pow(b, 2) - (4 * a * c);
        Debug.Log("BeforeSquare: " + BeforeSquare);
        if (BeforeSquare == 0)
        {
            x1 = (-b + Mathf.Sqrt(BeforeSquare)) / (2 * a);
            x.value = x1;
        }
        else if (BeforeSquare > 0)
        {
            x1 = (-b + Mathf.Sqrt(BeforeSquare)) / (2 * a);
            x2 = (-b - Mathf.Sqrt(BeforeSquare)) / (2 * a);
            yield return QuadraticSolver.Current.createPanel(x, "Tiempo", x1, x2);
        }
        else
        {
            Debug.Log("No tiene solución");
            yield return null;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulary2 : MonoBehaviour
{
    // Formulas MRU
    public static float Formula_mru_x(float v, float t)
    {
        return v * t;
    }
    public static float Formula_mru_v(float x, float t)
    {
        return x / t;
    }
    public static float Formula_mru_t(float x, float v)
    {
        return x / v;
    }
    // Formulas MRUV
    public static float Formula_1(float vo, float a, float t)
    {
        return vo + (a * t);
    }
    public static float Formula_2(float vo, float vf, float t)
    {
        return (vo + vf) * t / 2;
    }
    public static float Formula_4(float vo, float a, float t)
    {
        return (vo * t) + (a * Mathf.Pow(t, 2) / 2);
    }
    public static float Formula_4_t(float x, float vo, float a)
    {
        float? qf = QuadraticFormula(a / 2, vo, -x);
        if (qf.HasValue)
        {
            return qf.Value;
        }
        return 0;
    }
    public static float Altura(float x, float y, float dist, float grav)
    {
        float c = Mathf.Sqrt((x * x) + (y * y));
        float angulo = Mathf.Asin(y / c);
        Debug.Log($"c: {c} angulo: {angulo * Mathf.Rad2Deg}");
        // Trayectoria
        Debug.Log($"tan: {Mathf.Tan(angulo)} dist: {dist}");
        float altura = (Mathf.Tan(angulo) * dist) - (grav / (2 * (c * c) * Mathf.Pow(Mathf.Cos(angulo), 2)) * (dist * dist));
        return altura;
    }
    private static float? QuadraticFormula(float a, float b, float c)
    {
        float insideSquare = Mathf.Pow(b, 2) - (4 * a * c);
        if (insideSquare > 0)
        {
            float square = Mathf.Sqrt(insideSquare);
            float result1 = (-b + square) / (2 * a);
            float result2 = (-b - square) / (2 * a);
            Debug.Log($"Los resultados son: R1 = {result1} , R2 = {result2}");
            if (result1 > 0 && result2 > 0)
            {
                Debug.LogWarning("Formula Cuadratica: Ambos resultados son validos");
            }
            if (result1 > 0)
            {
                return result1;
            }
            else if (result2 > 0)
            {
                return result2;
            }
        }
        Debug.Log($"La raiz saldra negativa: {insideSquare}");
        return null;
    }


}

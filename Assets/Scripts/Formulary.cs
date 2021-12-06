using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulary
{
    private float Formula_1(float Vo, float Vf, float a, float t, string option)
    {
        switch (option)
        {
            case "Vf":
                return (Vo + (a * t));
            case "Vo":
                return (Vf - a * t);
            case "a":
                return (Vf - Vo) / t;
            case "t":
                return (Vf - Vo) / a;
            default:
                return 0;
        }

    }

    private float Formula_2(float Vo, float Vf, float x, float t, string option)
    {
        switch (option)
        {
            case "Vf":
                return (2 * x / t) - Vo;
            case "Vo":
                return (2 * x / t) - Vf;
            case "x":
                return (Vo + Vf) * t / 2;
            case "t":
                return (2 * x) / (Vo + Vf);
            default:
                return 0;
        }
    }

    private float Formula_3(float Vo, float Vf, float a, float x, string option)
    {
        switch (option)
        {
            case "Vf":
                return Mathf.Sqrt(Mathf.Pow(Vo, 2) + (2 * a * x));
            case "Vo":
                return Mathf.Sqrt(Mathf.Pow(Vf, 2) - (2 * a * x));
            case "x":
                return (Mathf.Pow(Vf, 2) - Mathf.Pow(Vo, 2)) / (2 * a);
            case "a":
                return (Mathf.Pow(Vf, 2) - Mathf.Pow(Vo, 2)) / (2 * x);
            default:
                return 0;
        }
    }

    private float Formula_4(float Vo, float a, float x, float t, string option)
    {
        switch (option)
        {
            case "Vo":
                return (x - (a * Mathf.Pow(t, 2) / 2)) / t;
            case "x":
                return Vo * t + (a * Mathf.Pow(t, 2) / 2);
            case "a":
                return (x - Vo * t) * 2 / Mathf.Pow(t, 2);
            case "t":
                return QuadraticFormula((a / 2), Vo, -x);
            default:
                return 0;
        }
    }

    private float Formula_5(float Vf, float a, float x, float t, string option)
    {
        switch (option)
        {
            case "Vf":
                return (x + (a * Mathf.Pow(t, 2) / 2)) / t;
            case "x":
                return Vf * t - (a * Mathf.Pow(t, 2) / 2);
            case "a":
                return -((x - Vf * t) * 2 / Mathf.Pow(t, 2));
            case "t":
                return QuadraticFormula(-(a / 2), Vf, -x);
            default:
                return 0;
        }
    }

    private float QuadraticFormula(float a, float b, float c)
    {
        float BeforeSquare = Mathf.Pow(b, 2) - (4 * a * c);
        float answer = (-b + Mathf.Sqrt(BeforeSquare)) / (2 * a);
        return answer;
    }

    public float Vel_f(float Vo, float a, float x, float t, string option)
    {
        switch (option)
        {
            case "x":
                return Formula_1(Vo, 0, a, t, "Vf");
            case "a":
                return Formula_2(Vo, 0, x, t, "Vf");
            case "t":
                return Formula_3(Vo, 0, a, x, "Vf");
            case "Vo":
                return Formula_5(0, a, x, t, "Vf");
            default:
                return 0;
        }
    }

    public float Vel_o(float Vf, float a, float x, float t, string option)
    {
        switch (option)
        {
            case "x":
                return Formula_1(0, Vf, a, t, "Vo");
            case "a":
                return Formula_2(0, Vf, x, t, "Vo");
            case "t":
                return Formula_3(0, Vf, a, x, "Vo");
            case "Vf":
                return Formula_4(0, a, x, t, "Vo");
            default:
                return 0;
        }
    }

    public float Acc(float Vo, float Vf, float x, float t, string option)
    {
        switch (option)
        {
            case "x":
                return Formula_1(Vo, Vf, 0, t, "a");
            case "t":
                return Formula_3(Vo, Vf, 0, x, "a");
            case "Vf":
                return Formula_4(Vo, 0, x, t, "a");
            case "Vo":
                return Formula_5(Vf, 0, x, t, "a");
            default:
                return 0;
        }
    }

    public float Dist(float Vo, float Vf, float a, float t, string option)
    {
        switch (option)
        {
            case "a":
                return Formula_2(Vo, Vf, 0, t, "x");
            case "t":
                return Formula_3(Vo, Vf, a, 0, "x");
            case "Vf":
                return Formula_4(Vo, a, 0, t, "x");
            case "Vo":
                return Formula_5(Vf, a, 0, t, "x");
            default:
                return 0;
        }
    }

    public float Time(float Vo, float Vf, float a, float x, string option)
    {
        switch (option)
        {
            case "x":
                return Formula_1(Vo, Vf, a, 0, "t");
            case "t":
                return Formula_2(Vo, Vf, x, 0, "t");
            case "Vf":
                return Formula_4(Vo, a, x, 0, "t");
            case "Vo":
                return Formula_5(Vf, a, x, 0, "t");
            default:
                return 0;
        }
    }



}

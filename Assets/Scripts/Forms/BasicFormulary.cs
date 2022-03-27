using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFormulary : MonoBehaviour
{
    public string _sVo, _sVf, _sa, _st, _sx;
    private Steps _stp;
    private void Start()
    {
        _stp = Steps.Current;
        _sVo = "Vo";
        _sVf = "Vf";
        _sa = "a";
        _st = "t";
        _sx = "x";
    }
    public enum option
    {
        Vo, Vf, a, x, t
    }
    public float Formula_1(float Vo, float Vf, float a, float t, int op)
    {
        Debug.Log("Se uso Formula 1 con " + op);
        float answer;
        switch ((option)op)
        {
            case option.Vf: // Main Formula
                answer = (Vo + (a * t));
                _stp.NewText("Se usa la siguiente formula:");
                _stp.NewLine($"Vf=Vo+({_sa}·t)");
                _stp.NewLine($"Vf={Vo}+({a}·{t})");
                _stp.NewLine("Vf=" + answer);
                return answer;
            case option.Vo:
                answer = (Vf - a * t);
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine($"Vf=Vo+({_sa}·t)");
                _stp.NewText("Se obtiene:");
                _stp.NewLine($"Vo=Vf-({_sa}·t)");
                _stp.NewLine($"Vo={Vf}-({a}·{t})");
                _stp.NewLine("Vo=" + answer);
                return answer;
            case option.a:
                answer = (Vf - Vo) / t;
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine($"Vf=Vo+({_sa}·t)");
                _stp.NewText("Se obtiene:");
                _stp.NewLine($"{_sa}=", _stp.Frac("(Vf-Vo)", "t"));
                _stp.NewLine($"{_sa}=", _stp.Frac($"({Vf}-{Vo})", t));
                _stp.NewLine($"{_sa}=" + answer);
                return answer;
            case option.t:
                answer = (Vf - Vo) / a;
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine($"Vf=Vo+({_sa}·t)");
                _stp.NewText("Se obtiene:");
                _stp.NewLine("t=", _stp.Frac("(Vf-Vo)", $"{_sa}"));
                _stp.NewLine("t=", _stp.Frac($"({Vf}-{Vo})", a));
                _stp.NewLine("t=" + answer);
                return answer;
            default:
                return 0;
        }
    }
    public float Formula_2(float Vo, float Vf, float x, float t, int op)
    {
        Debug.Log("Se uso Formula 2 con " + op);
        float answer;
        switch ((option)op)
        {
            case option.Vf:
                answer = (2 * x / t) - Vo;
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
                _stp.NewText("Se obtiene:");
                _stp.NewLine("Vf=", _stp.Frac("2·x", "t"), "-Vo");
                _stp.NewLine("Vf=", _stp.Frac($"2·{x}", t), $"-{Vo}");
                _stp.NewLine("Vf=" + answer);
                return answer;
            case option.Vo:
                answer = (2 * x / t) - Vf;
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
                _stp.NewText("Se obtiene:");
                _stp.NewLine("Vo=", _stp.Frac("2·x", "t"), "-Vf");
                _stp.NewLine("Vo=", _stp.Frac($"2·{x}", t), $"-{Vf}");
                _stp.NewLine("Vo=" + answer);
                return answer;
            case option.x: // Main Formula
                answer = (Vo + Vf) * t / 2;
                _stp.NewText("Se usa la siguiente formula:");
                _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
                _stp.NewLine("x=", _stp.Frac($"({Vo}+{Vf})·{t}", 2));
                _stp.NewLine("x=" + answer);
                return answer;
            case option.t:
                answer = (2 * x) / (Vo + Vf);
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
                _stp.NewText("Se obtiene:");
                _stp.NewLine("t=", _stp.Frac("2·x", "Vo+Vf"));
                _stp.NewLine("t=", _stp.Frac($"2·{x}", $"{Vo}+{Vf}"));
                _stp.NewLine("t=" + answer);
                return answer;
            default:
                return 0;
        }
    }
    public float Formula_3(float Vo, float Vf, float a, float x, int op)
    {
        Debug.Log("Se uso Formula 3 con " + op);
        float answer;
        switch ((option)op)
        {
            case option.Vf: //main formula
                answer = Mathf.Sqrt(Mathf.Pow(Vo, 2) + (2 * a * x));
                _stp.NewText("Se usa la siguiente formula:");
                _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
                _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS($"{Vo}", 2), $"+(2·{a}·{x})")));
                _stp.NewLine("Vf=" + answer);
                return answer;
            case option.Vo:
                answer = Mathf.Sqrt(Mathf.Pow(Vf, 2) - (2 * a * x));
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
                _stp.NewText("Se obtiene:");
                _stp.NewLine("Vo=", _stp.Sqrt(_stp.SupS("Vf", 2), $"-(2·{_sa}·x)"));
                _stp.NewLine("Vo=", _stp.Sqrt(_stp.SupS($"{Vf}", 2), $"-(2·{a}·{x})"));
                _stp.NewLine("Vo=" + answer);
                return answer;
            case option.x:
                answer = (Mathf.Pow(Vf, 2) - Mathf.Pow(Vo, 2)) / (2 * a);
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
                _stp.NewText("Se obtiene:");
                _stp.NewLine("x=", _stp.Frac(_stp.Group(_stp.SupS("Vf", 2), "-", _stp.SupS("Vo", 2)), $"(2·{_sa})"));
                _stp.NewLine("x=", _stp.Frac(_stp.Group(_stp.SupS($"{Vf}", 2), "-", _stp.SupS($"{Vo}", 2)), $"(2·{a})"));
                _stp.NewLine("x=" + answer);
                return answer;
            case option.a:
                answer = (Mathf.Pow(Vf, 2) - Mathf.Pow(Vo, 2)) / (2 * x);
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
                _stp.NewText("Se obtiene:");
                _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group(_stp.SupS("Vf", 2), "-", _stp.SupS("Vo", 2)), "(2·x)"));
                _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group(_stp.SupS($"{Vf}", 2), "-", _stp.SupS($"{Vo}", 2)), $"(2·{x})"));
                _stp.NewLine($"{_sa}=" + answer);
                return answer;
            default:
                return 0;
        }
    }
    public IEnumerator Formula_4(float Vo, float a, float x, float t, int op, Field f)
    {
        Debug.Log("Se uso Formula 4 con " + op);
        float answer;
        switch ((option)op)
        {
            case option.Vo:
                answer = (x - (a * Mathf.Pow(t, 2) / 2)) / t;
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
                _stp.NewText("Se obtiene:");
                _stp.NewLine("Vo=", _stp.Frac(_stp.Group("x-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2)), "t"));
                _stp.NewLine("Vo=", _stp.Frac(_stp.Group($"{x}-", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2)), t));
                _stp.NewLine("Vo=", answer);
                f.value = answer;
                break;
            case option.x: //main formula
                answer = Vo * t + (a * Mathf.Pow(t, 2) / 2);
                _stp.NewText("Se usa la siguiente formula:");
                _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
                _stp.NewLine("x=", $"{Vo}·{t}+", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2));
                _stp.NewLine("x=", answer);
                f.value = answer;
                break;
            case option.a:
                answer = (x - Vo * t) * 2 / Mathf.Pow(t, 2);
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
                _stp.NewText("Se obtiene:");
                _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group("(x-Vo·t)2"), _stp.SupS("t", 2)));
                _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group($"({x}-{Vo}·{t})2"), _stp.SupS(t, 2)));
                _stp.NewLine($"{_sa}=", answer);
                f.value = answer;
                break;
            case option.t:
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
                if (Vo == 0)
                {
                    answer = Mathf.Sqrt(2 * x / a);
                    _stp.NewText("Como Vo=0 se obtiene:");
                    _stp.NewLine("x=", _stp.Cancel("Vo·t"), "+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
                    _stp.NewLine("t=", _stp.Sqrt(_stp.Frac("2·x", _sa)));
                    _stp.NewLine("t=", _stp.Sqrt(_stp.Frac($"2·{x}", a)));
                    _stp.NewLine("t=", answer);
                    f.value = answer;
                }
                else
                {
                    _stp.NewLine(_stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2), "Vo·t-x=0 //·2");
                    _stp.NewLine($"{_sa}·", _stp.SupS("t", 2), "+2·Vo·t-2·x=0");
                    _stp.NewLine($"{a}·", _stp.SupS(t, 2), $"+2·{Vo}·{t}-2·{x}=0");
                    yield return QuadraticFormula(f, a, 2 * Vo, -2 * x);
                }
                break;
            default:
                break;
        }
    }
    public IEnumerator Formula_5(float Vf, float a, float x, float t, int op, Field f)
    {
        Debug.Log("Se uso Formula 5 con " + op);
        float answer;
        switch ((option)op)
        {
            case option.Vf:
                answer = (x + (a * Mathf.Pow(t, 2) / 2)) / t;
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
                _stp.NewText("Se obtiene:");
                _stp.NewLine("Vf=", _stp.Frac(_stp.Group("x+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2)), "t"));
                _stp.NewLine("Vf=", _stp.Frac(_stp.Group($"{x}-", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2)), t));
                _stp.NewLine("Vf=", answer);
                f.value = answer;
                yield break;
            case option.x: //main formula
                answer = Vf * t - (a * Mathf.Pow(t, 2) / 2);
                _stp.NewText("Se usa la siguiente formula:");
                _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
                _stp.NewLine("x=", $"{Vf}·{t}-", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2));
                _stp.NewLine("x=", answer);
                f.value = answer;
                break;
            case option.a:
                answer = -((x - Vf * t) * 2 / Mathf.Pow(t, 2));
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
                _stp.NewText("Se obtiene:");
                _stp.NewLine($"{_sa}=", "-", _stp.Frac(_stp.Group("(x-Vf·t)2"), _stp.SupS("t", 2)));
                _stp.NewLine($"{_sa}=", "-", _stp.Frac(_stp.Group($"({x}-{Vf}·{t})2"), _stp.SupS(t, 2)));
                _stp.NewLine($"{_sa}=", answer);
                f.value = answer;
                break;
            case option.t:
                _stp.NewText("Despejando de la formula principal:");
                _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
                if (Vf == 0)
                {
                    answer = Mathf.Sqrt(2 * x / -a);
                    _stp.NewText("Como Vf=0 se obtiene:");
                    _stp.NewLine("x=", _stp.Cancel("Vf·t"), "-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
                    _stp.NewLine("t=", _stp.Sqrt(_stp.Frac("2·x", $"-{_sa}")));
                    _stp.NewLine("t=", _stp.Sqrt(_stp.Frac($"2·{x}", $"-{a}")));
                    _stp.NewLine("t=", answer);
                    f.value = answer;
                }
                else
                {
                    _stp.NewLine("-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2), "+Vf·t-x=0 //·-2");
                    _stp.NewLine($"{_sa}·", _stp.SupS("t", 2), "-2·Vf·t+2·x=0");
                    _stp.NewLine($"{a}·", _stp.SupS("t", 2), $"-2·{Vf}·t+2·{x}=0");
                    yield return QuadraticFormula(f, a, -2 * Vf, 2 * x);
                }
                break;
            default:
                break;
        }
    }
    public IEnumerator QuadraticFormula(Field x, float a, float b, float c)
    {
        // Debug.Log($"a: {a}, b: {b} y c: {c}");
        _stp.NewText($"Se resolvera usando la formula cuadrática con los valores a: {a}, b: {b} y c: {c}");
        float x1, x2;
        float BeforeSquare = Mathf.Pow(b, 2) - (4 * a * c);
        _stp.NewLine(_stp.Frac(_stp.Group("-b+-", _stp.Sqrt(_stp.SupS("b", 2), "-4·a·c")), "2·a"));
        _stp.NewLine(_stp.Frac(_stp.Group($"-{b}+-", _stp.Sqrt(_stp.SupS(b, 2), $"-4·{a}·{c}")), $"2·{a}"));
        Debug.Log("BeforeSquare: " + BeforeSquare);
        if (BeforeSquare == 0)
        {
            _stp.NewLine(_stp.Frac(_stp.Group($"-{b}+-", _stp.Cancel(_stp.Sqrt(0))), $"2·{a}"));
            x1 = (-b) / (2 * a);
            _stp.NewLine("t=", x1);
            x.value = x1;
        }
        else if (BeforeSquare > 0)
        {
            _stp.NewLine(_stp.Frac(_stp.Group($"-{b}+-", _stp.Sqrt(BeforeSquare)), $"2{a}"));
            x1 = (-b + Mathf.Sqrt(BeforeSquare)) / (2 * a);
            x2 = (-b - Mathf.Sqrt(BeforeSquare)) / (2 * a);
            _stp.NewLine("t1=", x1);
            _stp.NewLine("t2=", x2);
            if (x1 >= 0 && x2 >= 0)
            {
                yield return QuadraticSolver.Current.createPanel(x, "Tiempo", x1, x2);
            }
            else
            {
                if (x1 < 0 && x2 < 0)
                {
                    _stp.NewLine($"Ningun valor es valido. No puede existir tiempo negativo");
                }
                else
                {
                    if (x1 < 0)
                    {
                        _stp.NewText($"t1={x1} No es valor valido porque no existe tiempo negativo");
                        _stp.NewLine("Por lo tanto t=", x2);
                    }
                    if (x2 < 0)
                    {
                        _stp.NewText($"t2={x2} No es valor valido porque no existe tiempo negativo");
                        _stp.NewLine("Por lo tanto t=", x1);
                    }
                }
            }
        }
        else
        {
            Debug.Log("No tiene solución");
            _stp.NewLine(_stp.Frac(_stp.Group($"-{b}+-", _stp.Sqrt(BeforeSquare)), $"2{a}"));
            _stp.NewLine("No tiene solución");
            yield return null;
        }
    }

}

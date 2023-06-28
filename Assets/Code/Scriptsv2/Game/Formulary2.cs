using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Formulary2 : MonoBehaviour
{
    public static Steps _stp;
    private void Start()
    {
        _stp = Steps.Current;
    }
    // Formulas MRU
    public static float Formula_mru_x(float v, float t)
    {
        return v * t;
    }
    public static float Formula_mru_x(List<VariableInput> variables)
    {
        float v = variables.First((e) => { return e.varUnity.TipoVariable == BaseVariable.Velocidad; }).Value;
        float t = variables.First((e) => { return e.varUnity.TipoVariable == BaseVariable.Tiempo; }).Value;
        return v * t;
    }
    public static float Formula_mru_v(float x, float t)
    {
        return x / t;
    }
    public static float Formula_mru_v(List<VariableInput> variables)
    {
        float x = variables.First((e) => { return e.varUnity.TipoVariable == BaseVariable.Distancia; }).Value;
        float t = variables.First((e) => { return e.varUnity.TipoVariable == BaseVariable.Tiempo; }).Value;
        return x / t;
    }
    public static float Formula_mru_t(float x, float v)
    {
        return x / v;
    }
    public static float Formula_mru_t(List<VariableInput> variables)
    {
        float x = variables.First((e) => { return e.varUnity.TipoVariable == BaseVariable.Distancia; }).Value;
        float v = variables.First((e) => { return e.varUnity.TipoVariable == BaseVariable.Velocidad; }).Value;
        return x / v;
    }
    // Formulas MRUV
    public static float Formula_1(float vo, float a, float t)
    {
        return vo + (a * t);
    }
    public static float Formula_1(List<VariableInput> variables)
    {
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float a = variables.First((e) => { return e.varUnity.TipoVariable == BaseVariable.Aceleracion || e.varUnity.TipoVariable == BaseVariable.Gravedad; }).Value;
        float t = GetValue(variables, BaseVariable.Tiempo);
        float answer = vo + (a * t);
        _stp.NewText("Se usa la siguiente formula:");
        if (variables.First((e) => { return e.varUnity.TipoVariable == BaseVariable.Aceleracion; }) != null)
        {
            _stp.NewLine($"Vf=Vo+(a·t)");
        }
        else
        {
            _stp.NewLine($"Vf=Vo+(g·t)");
        }
        _stp.NewLine($"Vf={vo}+({a}·{t})");
        _stp.NewLine("Vf=" + answer);
        return answer;
    }
    public static float Formula_1_vo(List<VariableInput> variables)
    {
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float a = variables.First((e) => { return e.varUnity.TipoVariable == BaseVariable.Aceleracion || e.varUnity.TipoVariable == BaseVariable.Gravedad; }).Value;
        float t = GetValue(variables, BaseVariable.Tiempo);
        float answer = vf - a * t;
        string _sa = "";
        if (variables.First((e) => { return e.varUnity.TipoVariable == BaseVariable.Aceleracion; }) != null)
        {
            _sa = "a";
        }
        else
        {
            _sa = "g";
        }
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine($"Vf=Vo+({_sa}·t)");
        _stp.NewText("Se obtiene:");
        _stp.NewLine($"Vo=Vf-({_sa}·t)");
        _stp.NewLine($"Vo={vf}-({a}·{t})");
        _stp.NewLine("Vo=" + answer);
        return answer;
    }
    public static float Formula_1_a(List<VariableInput> variables)
    {
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float t = GetValue(variables, BaseVariable.Tiempo);
        float answer = (vf - vo) / t;
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine($"Vf=Vo+({_sa}·t)");
        _stp.NewText("Se obtiene:");
        _stp.NewLine($"{_sa}=", _stp.Frac("(Vf-Vo)", "t"));
        _stp.NewLine($"{_sa}=", _stp.Frac($"({vf}-{vo})", t));
        _stp.NewLine($"{_sa}=" + answer);
        return answer;
    }
    public static float Formula_1_t(List<VariableInput> variables)
    {
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float a = GetValue(variables, BaseVariable.Aceleracion);
        float answer = (vf - vo) / a;
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine($"Vf=Vo+({_sa}·t)");
        _stp.NewText("Se obtiene:");
        _stp.NewLine("t=", _stp.Frac("(Vf-Vo)", $"{_sa}"));
        _stp.NewLine("t=", _stp.Frac($"({vf}-{vo})", a));
        _stp.NewLine("t=" + answer);
        return answer;
    }
    public static float Formula_2(float vo, float vf, float t)
    {
        return (vo + vf) * t / 2;
    }
    public static float Formula_2(List<VariableInput> variables)
    {
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float t = GetValue(variables, BaseVariable.Tiempo);
        float answer = (vo + vf) * t / 2;
        _stp.NewText("Se usa la siguiente formula:");
        _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
        _stp.NewLine("x=", _stp.Frac($"({vo}+{vf})·{t}", 2));
        _stp.NewLine("x=" + answer);
        return answer;
    }
    public static float Formula_2_vf(List<VariableInput> variables)
    {
        float x = GetValue(variables, BaseVariable.Distancia);
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float t = GetValue(variables, BaseVariable.Tiempo);
        float answer = (2 * x / t) - vo;
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
        _stp.NewText("Se obtiene:");
        _stp.NewLine("Vf=", _stp.Frac("2·x", "t"), "-Vo");
        _stp.NewLine("Vf=", _stp.Frac($"2·{x}", t), $"-{vo}");
        _stp.NewLine("Vf=" + answer);
        return answer;
    }
    public static float Formula_2_vo(List<VariableInput> variables)
    {
        float x = GetValue(variables, BaseVariable.Distancia);
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float t = GetValue(variables, BaseVariable.Tiempo);
        float answer = (2 * x / t) - vf;
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
        _stp.NewText("Se obtiene:");
        _stp.NewLine("Vo=", _stp.Frac("2·x", "t"), "-Vf");
        _stp.NewLine("Vo=", _stp.Frac($"2·{x}", t), $"-{vf}");
        _stp.NewLine("Vo=" + answer);
        return answer;
    }
    public static float Formula_2_t(List<VariableInput> variables)
    {
        float x = GetValue(variables, BaseVariable.Distancia);
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float answer = 2 * x / (vo + vf);
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
        _stp.NewText("Se obtiene:");
        _stp.NewLine("t=", _stp.Frac("2·x", "Vo+Vf"));
        _stp.NewLine("t=", _stp.Frac($"2·{x}", $"{vo}+{vf}"));
        _stp.NewLine("t=" + answer);
        return answer;
    }
    public static float Formula_3(List<VariableInput> variables)
    {
        float x = GetValue(variables, BaseVariable.Distancia);
        float a = GetValue(variables, BaseVariable.Aceleracion);
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float answer = Mathf.Sqrt(Mathf.Pow(vo, 2) + (2 * a * x));
        string _sa = "a";
        _stp.NewText("Se usa la siguiente formula:");
        _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
        _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS($"{vo}", 2), $"+(2·{a}·{x})")));
        _stp.NewLine("Vf=" + answer);
        return answer;
    }
    public static float Formula_3_vo(List<VariableInput> variables)
    {
        float x = GetValue(variables, BaseVariable.Distancia);
        float a = GetValue(variables, BaseVariable.Aceleracion);
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float answer = Mathf.Sqrt(Mathf.Pow(vf, 2) - (2 * a * x));
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
        _stp.NewText("Se obtiene:");
        _stp.NewLine("Vo=", _stp.Sqrt(_stp.SupS("Vf", 2), $"-(2·{_sa}·x)"));
        _stp.NewLine("Vo=", _stp.Sqrt(_stp.SupS($"{vf}", 2), $"-(2·{a}·{x})"));
        _stp.NewLine("Vo=" + answer);
        return answer;
    }
    public static float Formula_3_x(List<VariableInput> variables)
    {
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float a = GetValue(variables, BaseVariable.Aceleracion);
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float answer = (Mathf.Pow(vf, 2) - Mathf.Pow(vo, 2)) / (2 * a);
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
        _stp.NewText("Se obtiene:");
        _stp.NewLine("x=", _stp.Frac(_stp.Group(_stp.SupS("Vf", 2), "-", _stp.SupS("Vo", 2)), $"(2·{_sa})"));
        _stp.NewLine("x=", _stp.Frac(_stp.Group(_stp.SupS($"{vf}", 2), "-", _stp.SupS($"{vo}", 2)), $"(2·{a})"));
        _stp.NewLine("x=" + answer);
        return answer;
    }
    public static float Formula_3_a(List<VariableInput> variables)
    {
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float x = GetValue(variables, BaseVariable.Distancia);
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float answer = (Mathf.Pow(vf, 2) - Mathf.Pow(vo, 2)) / (2 * x);
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
        _stp.NewText("Se obtiene:");
        _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group(_stp.SupS("Vf", 2), "-", _stp.SupS("Vo", 2)), "(2·x)"));
        _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group(_stp.SupS($"{vf}", 2), "-", _stp.SupS($"{vo}", 2)), $"(2·{x})"));
        _stp.NewLine($"{_sa}=" + answer);
        return answer;
    }
    public static float Formula_4(float vo, float a, float t)
    {
        return (vo * t) + (a * Mathf.Pow(t, 2) / 2);
    }
    public static float Formula_4(List<VariableInput> variables)
    {
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float t = GetValue(variables, BaseVariable.Tiempo);
        float a = GetValue(variables, BaseVariable.Aceleracion);
        float answer = (vo * t) + (a * Mathf.Pow(t, 2) / 2);
        string _sa = "a";
        _stp.NewText("Se usa la siguiente formula:");
        _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
        _stp.NewLine("x=", $"{vo}·{t}+", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2));
        _stp.NewLine("x=", answer);
        return answer;
    }
    public static float Formula_4_vo(List<VariableInput> variables)
    {
        float x = GetValue(variables, BaseVariable.Distancia);
        float t = GetValue(variables, BaseVariable.Tiempo);
        float a = GetValue(variables, BaseVariable.Aceleracion);
        float answer = (x - (a * Mathf.Pow(t, 2) / 2)) / t;
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
        _stp.NewText("Se obtiene:");
        _stp.NewLine("Vo=", _stp.Frac(_stp.Group("x-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2)), "t"));
        _stp.NewLine("Vo=", _stp.Frac(_stp.Group($"{x}-", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2)), t));
        _stp.NewLine("Vo=", answer);
        return answer;
    }
    public static float Formula_4_a(List<VariableInput> variables)
    {
        float x = GetValue(variables, BaseVariable.Distancia);
        float t = GetValue(variables, BaseVariable.Tiempo);
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float answer = (x - vo * t) * 2 / Mathf.Pow(t, 2);
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
        _stp.NewText("Se obtiene:");
        _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group("(x-Vo·t)2"), _stp.SupS("t", 2)));
        _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group($"({x}-{vo}·{t})2"), _stp.SupS(t, 2)));
        _stp.NewLine($"{_sa}=", answer);
        return answer;
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
    public static float Formula_4_t(List<VariableInput> variables)
    {
        float x = GetValue(variables, BaseVariable.Distancia);
        float a = GetValue(variables, BaseVariable.Aceleracion);
        float vo = GetValue(variables, BaseVariable.VelocidadInicial);
        float answer;
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
        if (vo == 0)
        {
            answer = Mathf.Sqrt(2 * x / a);
            _stp.NewText("Como Vo=0 se obtiene:");
            _stp.NewLine("x=", _stp.Cancel("Vo·t"), "+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
            _stp.NewLine("t=", _stp.Sqrt(_stp.Frac("2·x", _sa)));
            _stp.NewLine("t=", _stp.Sqrt(_stp.Frac($"2·{x}", a)));
            _stp.NewLine("t=", answer);
        }
        else
        {
            _stp.NewLine(_stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2), "Vo·t-x=0");
            float? qf = QuadraticFormula(a / 2, vo, -x);
            if (qf.HasValue)
            {
                return qf.Value;
            }
            return 0;

        }
        return answer;
    }
    public static float Formula_5(List<VariableInput> variables)
    {
        float t = GetValue(variables, BaseVariable.Tiempo);
        float a = GetValue(variables, BaseVariable.Aceleracion);
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float answer = vf * t - (a * Mathf.Pow(t, 2) / 2);
        string _sa = "a";
        _stp.NewText("Se usa la siguiente formula:");
        _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
        _stp.NewLine("x=", $"{vf}·{t}-", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2));
        _stp.NewLine("x=", answer);
        return answer;
    }
    public static float Formula_5_vf(List<VariableInput> variables)
    {
        float t = GetValue(variables, BaseVariable.Tiempo);
        float a = GetValue(variables, BaseVariable.Aceleracion);
        float x = GetValue(variables, BaseVariable.Distancia);
        float answer = (x + (a * Mathf.Pow(t, 2) / 2)) / t;
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
        _stp.NewText("Se obtiene:");
        _stp.NewLine("Vf=", _stp.Frac(_stp.Group("x+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2)), "t"));
        _stp.NewLine("Vf=", _stp.Frac(_stp.Group($"{x}-", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2)), t));
        _stp.NewLine("Vf=", answer);
        return answer;
    }
    public static float Formula_5_a(List<VariableInput> variables)
    {
        float t = GetValue(variables, BaseVariable.Tiempo);
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float x = GetValue(variables, BaseVariable.Distancia);
        float answer = -((x - vf * t) * 2 / Mathf.Pow(t, 2));
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
        _stp.NewText("Se obtiene:");
        _stp.NewLine($"{_sa}=", "-", _stp.Frac(_stp.Group("(x-Vf·t)2"), _stp.SupS("t", 2)));
        _stp.NewLine($"{_sa}=", "-", _stp.Frac(_stp.Group($"({x}-{vf}·{t})2"), _stp.SupS(t, 2)));
        _stp.NewLine($"{_sa}=", answer);
        return answer;
    }
    public static float Formula_5_t(List<VariableInput> variables)
    {
        float a = GetValue(variables, BaseVariable.Aceleracion);
        float vf = GetValue(variables, BaseVariable.VelocidadFinal);
        float x = GetValue(variables, BaseVariable.Distancia);
        float answer;
        string _sa = "a";
        _stp.NewText("Despejando de la formula principal:");
        _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
        if (vf == 0)
        {
            answer = Mathf.Sqrt(2 * x / -a);
            _stp.NewText("Como Vf=0 se obtiene:");
            _stp.NewLine("x=", _stp.Cancel("Vf·t"), "-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
            _stp.NewLine("t=", _stp.Sqrt(_stp.Frac("2·x", $"-{_sa}")));
            _stp.NewLine("t=", _stp.Sqrt(_stp.Frac($"2·{x}", $"-{a}")));
            _stp.NewLine("t=", answer);
        }
        else
        {
            _stp.NewLine("-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2), "+Vf·t-x=0");
            float? qf = QuadraticFormula(-a / 2, vf, -x);
            if (qf.HasValue)
            {
                return qf.Value;
            }
            return 0;
        }
        return answer;
    }
    public static float Altura(float x, float y, float dist, float grav)
    {
        float c = Mathf.Sqrt((x * x) + (y * y));
        float angulo = Mathf.Asin(y / c);
        // Debug.Log($"c: {c} angulo: {angulo * Mathf.Rad2Deg}");
        // Trayectoria
        // Debug.Log($"tan: {Mathf.Tan(angulo)} dist: {dist}");
        float altura = (Mathf.Tan(angulo) * dist) - (grav / (2 * (c * c) * Mathf.Pow(Mathf.Cos(angulo), 2)) * (dist * dist));
        return altura;
    }
    private static float? QuadraticFormula(float a, float b, float c)
    {
        _stp.NewText($"Se resolvera usando la formula cuadrática con los valores a: {a}, b: {b} y c: {c}");
        float x1;
        float insideSquare = Mathf.Pow(b, 2) - (4 * a * c);
        _stp.NewLine(_stp.Frac(_stp.Group("-b+-", _stp.Sqrt(_stp.SupS("b", 2), "-4·a·c")), "2·a"));
        _stp.NewLine(_stp.Frac(_stp.Group($"-{b}+-", _stp.Sqrt(_stp.SupS(b, 2), $"-4·{a}·{c}")), $"2·{a}"));
        if (insideSquare == 0)
        {
            _stp.NewLine(_stp.Frac(_stp.Group($"-{b}+-", _stp.Cancel(_stp.Sqrt(0))), $"2·{a}"));
            x1 = (-b) / (2 * a);
            _stp.NewLine("t=", x1);
        }
        if (insideSquare > 0)
        {
            _stp.NewLine(_stp.Frac(_stp.Group($"-{b}+-", _stp.Sqrt(insideSquare)), $"2{a}"));
            float square = Mathf.Sqrt(insideSquare);
            float result1 = (-b + square) / (2 * a);
            float result2 = (-b - square) / (2 * a);
            Debug.Log($"Los resultados son: R1 = {result1} , R2 = {result2}");
            _stp.NewLine("t1=", result1);
            _stp.NewLine("t2=", result2);
            if (result1 > 0 && result2 > 0)
            {
                Debug.LogWarning("Formula Cuadratica: Ambos resultados son validos");
            }
            else
            {
                if (result1 < 0 && result2 < 0)
                {
                    _stp.NewLine($"Ningun valor es valido. No puede existir tiempo negativo");
                }
                else
                {
                    if (result1 < 0)
                    {
                        _stp.NewText($"t1={result1} No es valor valido porque no existe tiempo negativo");
                        _stp.NewLine("Por lo tanto t=", result2);

                    }
                    if (result2 < 0)
                    {
                        _stp.NewText($"t2={result2} No es valor valido porque no existe tiempo negativo");
                        _stp.NewLine("Por lo tanto t=", result1);

                    }
                }
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
        _stp.NewLine(_stp.Frac(_stp.Group($"-{b}+-", _stp.Sqrt(insideSquare)), $"2{a}"));
        _stp.NewLine("No tiene solución");
        _stp.NewLine($"La raiz saldra negativa: {insideSquare}");
        Debug.Log($"La raiz saldra negativa: {insideSquare}");
        return null;
    }
    private static float GetValue(List<VariableInput> variables, TipoVariable varToFind)
    {
        return variables.First((e) => { return e.varUnity.TipoVariable == varToFind; }).Value;
    }

}

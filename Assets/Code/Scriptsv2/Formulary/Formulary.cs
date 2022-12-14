using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulary : MonoBehaviour
{
    public static Formulary Instance;
    public string _sVo, _sVf, _sa, _st, _sx;
    public Steps _stp;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private List<Formula> formulaList = new();
    public void Init()
    {
        _stp = Steps.Current;
        _sVo = "Vo";
        _sVf = "Vf";
        _sa = "a";
        _st = "t";
        _sx = "x";
        formulaList.Add(new Formula_1());
        formulaList.Add(new Formula_2());
        formulaList.Add(new Formula_3());
        formulaList.Add(new Formula_4());
        formulaList.Add(new Formula_5());
    }
    public bool CheckRequirements(Dato dataToAnswer, Dato[] actualData)
    {
        List<Dato> allData = new();
        allData.Add(dataToAnswer);
        allData.AddRange(actualData);

        for (int i = 0; i < formulaList.Count; i++)
        {
            bool dataExist = false;
            for (int j = 0; j < formulaList[i].Requirements.Count; j++)
            {
                dataExist = allData.Exists(data => data.VariableId == formulaList[i].Requirements[j].Id);
                print($"El requisito {formulaList[i].Requirements[j].Nombre} = {dataExist}");
                if (!dataExist) break;
            }
            if (dataExist)
            {
                print("Se cumplen todos los requisitos");
                if (formulaList[i].IsEnumerator)
                {
                    StartCoroutine(formulaList[i].Calculate2(GlobalInfo.Variables[dataToAnswer.VariableId - 1], actualData, (res) =>
                    {
                        dataToAnswer.Valor = res;
                        // dataToAnswer.ChangeValor(res);
                    }));
                }
                else
                {
                    dataToAnswer.Valor = formulaList[i].Calculate(GlobalInfo.Variables[dataToAnswer.VariableId - 1], actualData);
                }
                return true;
            }
        }
        return false;
    }
}
public abstract class Formula
{
    protected Variable _Vo = GlobalInfo.Variables[4 - 1];
    protected Variable _Vf = GlobalInfo.Variables[5 - 1];
    protected Variable _a = GlobalInfo.Variables[6 - 1];
    protected Variable _x = GlobalInfo.Variables[2 - 1];
    protected Variable _t = GlobalInfo.Variables[3 - 1];
    public Steps _stp = Steps.Current;
    public string _sVo = "Vo", _sVf = "Vf", _sa = "a", _st = "t", _sx = "x";
    public bool IsEnumerator { get; set; }
    public abstract List<Variable> Requirements { get; set; }
    public abstract float Calculate(Variable variableToAnswer, Dato[] datos);
    public abstract IEnumerator Calculate2(Variable variableToAnswer, Dato[] datos, Action<float> res);
    protected IEnumerator QuadraticFormula(float a, float b, float c, Action<float> res)
    {
        Debug.Log($"a: {a}, b: {b} y c: {c}");
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
            res(x1);
        }
        else if (BeforeSquare > 0)
        {
            _stp.NewLine(_stp.Frac(_stp.Group($"-{b}+-", _stp.Sqrt(BeforeSquare)), $"2{a}"));
            x1 = (-b + Mathf.Sqrt(BeforeSquare)) / (2 * a);
            x2 = (-b - Mathf.Sqrt(BeforeSquare)) / (2 * a);
            Debug.Log($"{x1}, {x2}");
            _stp.NewLine("t1=", x1);
            _stp.NewLine("t2=", x2);
            if (x1 >= 0 && x2 >= 0)
            {
                yield return QuadraticSolver.Current.createPanel("Tiempo", x1, x2, (answer) =>
                {
                    res(answer);
                });
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
                        res(x2);
                    }
                    if (x2 < 0)
                    {
                        _stp.NewText($"t2={x2} No es valor valido porque no existe tiempo negativo");
                        _stp.NewLine("Por lo tanto t=", x1);
                        res(x1);
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
public class Formula_1 : Formula
{
    private List<Variable> _requirements;
    private float Vo, Vf, a, t;
    public Formula_1()
    {
        Debug.Log("Formula 1 instance");
        Requirements = new List<Variable>(new Variable[] { _Vo, _Vf, _a, _t });
        IsEnumerator = false;
    }
    public override List<Variable> Requirements { get => _requirements; set => _requirements = value; }
    public override float Calculate(Variable variableToAnswer, Dato[] datos)
    {
        Debug.Log("Se uso Formula 1 con " + variableToAnswer.Nombre);
        foreach (Dato dato in datos)
        {
            if (dato.VariableId == _Vo.Id)
            {
                Vo = dato.Valor;
            }
            else if (dato.VariableId == _Vf.Id)
            {
                Vf = dato.Valor;
            }
            else if (dato.VariableId == _a.Id)
            {
                a = dato.Valor;
            }
            else if (dato.VariableId == _t.Id)
            {
                t = dato.Valor;
            }
        }
        float answer = 0;
        if (variableToAnswer.Id == _Vf.Id)
        {
            answer = Vo + (a * t);
            _stp.NewText("Se usa la siguiente formula:");
            _stp.NewLine($"Vf=Vo+({_sa}·t)");
            _stp.NewLine($"Vf={Vo}+({a}·{t})");
            _stp.NewLine("Vf=" + answer);
        }
        else if (variableToAnswer.Id == _Vo.Id)
        {
            answer = Vf - a * t;
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine($"Vf=Vo+({_sa}·t)");
            _stp.NewText("Se obtiene:");
            _stp.NewLine($"Vo=Vf-({_sa}·t)");
            _stp.NewLine($"Vo={Vf}-({a}·{t})");
            _stp.NewLine("Vo=" + answer);
        }
        else if (variableToAnswer.Id == _a.Id)
        {
            answer = (Vf - Vo) / t;
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine($"Vf=Vo+({_sa}·t)");
            _stp.NewText("Se obtiene:");
            _stp.NewLine($"{_sa}=", _stp.Frac("(Vf-Vo)", "t"));
            _stp.NewLine($"{_sa}=", _stp.Frac($"({Vf}-{Vo})", t));
            _stp.NewLine($"{_sa}=" + answer);
        }
        else if (variableToAnswer.Id == _t.Id)
        {
            answer = (Vf - Vo) / a;
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine($"Vf=Vo+({_sa}·t)");
            _stp.NewText("Se obtiene:");
            _stp.NewLine("t=", _stp.Frac("(Vf-Vo)", $"{_sa}"));
            _stp.NewLine("t=", _stp.Frac($"({Vf}-{Vo})", a));
            _stp.NewLine("t=" + answer);
        }
        return answer;
    }
    public override IEnumerator Calculate2(Variable variableToAnswer, Dato[] datos, Action<float> res)
    {
        throw new NotImplementedException();
    }
}
public class Formula_2 : Formula
{
    private List<Variable> _requirements;
    private float Vo, Vf, x, t;
    public Formula_2()
    {
        Debug.Log("Formula 2 instance");
        Requirements = new List<Variable>(new Variable[] { _Vo, _Vf, _x, _t });
        IsEnumerator = false;
    }
    public override List<Variable> Requirements { get => _requirements; set => _requirements = value; }
    public override float Calculate(Variable variableToAnswer, Dato[] datos)
    {
        Debug.Log("Se uso Formula 2 con " + variableToAnswer.Nombre);
        foreach (Dato dato in datos)
        {
            if (dato.VariableId == _Vo.Id)
            {
                Vo = dato.Valor;
            }
            else if (dato.VariableId == _Vf.Id)
            {
                Vf = dato.Valor;
            }
            else if (dato.VariableId == _x.Id)
            {
                x = dato.Valor;
            }
            else if (dato.VariableId == _t.Id)
            {
                t = dato.Valor;
            }
        }
        float answer = 0;
        if (variableToAnswer.Id == _Vf.Id)
        {
            answer = (2 * x / t) - Vo;
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
            _stp.NewText("Se obtiene:");
            _stp.NewLine("Vf=", _stp.Frac("2·x", "t"), "-Vo");
            _stp.NewLine("Vf=", _stp.Frac($"2·{x}", t), $"-{Vo}");
            _stp.NewLine("Vf=" + answer);
        }
        else if (variableToAnswer.Id == _Vo.Id)
        {
            answer = (2 * x / t) - Vf;
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
            _stp.NewText("Se obtiene:");
            _stp.NewLine("Vo=", _stp.Frac("2·x", "t"), "-Vf");
            _stp.NewLine("Vo=", _stp.Frac($"2·{x}", t), $"-{Vf}");
            _stp.NewLine("Vo=" + answer);
        }
        else if (variableToAnswer.Id == _x.Id)
        {
            answer = (Vo + Vf) * t / 2;
            _stp.NewText("Se usa la siguiente formula:");
            _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
            _stp.NewLine("x=", _stp.Frac($"({Vo}+{Vf})·{t}", 2));
            _stp.NewLine("x=" + answer);
        }
        else if (variableToAnswer.Id == _t.Id)
        {
            answer = 2 * x / (Vo + Vf);
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine("x=", _stp.Frac("(Vo+Vf)·t", 2));
            _stp.NewText("Se obtiene:");
            _stp.NewLine("t=", _stp.Frac("2·x", "Vo+Vf"));
            _stp.NewLine("t=", _stp.Frac($"2·{x}", $"{Vo}+{Vf}"));
            _stp.NewLine("t=" + answer);
        }
        return answer;
    }
    public override IEnumerator Calculate2(Variable variableToAnswer, Dato[] datos, Action<float> res)
    {
        throw new NotImplementedException();
    }
}
public class Formula_3 : Formula
{
    private List<Variable> _requirements;
    private float Vo, Vf, x, a;
    public Formula_3()
    {
        Debug.Log("Formula 3 instance");
        Requirements = new List<Variable>(new Variable[] { _Vo, _Vf, _x, _a });
        IsEnumerator = false;
    }
    public override List<Variable> Requirements { get => _requirements; set => _requirements = value; }
    public override float Calculate(Variable variableToAnswer, Dato[] datos)
    {
        Debug.Log("Se uso Formula 3 con " + variableToAnswer.Nombre);
        foreach (Dato dato in datos)
        {
            if (dato.VariableId == _Vo.Id)
            {
                Vo = dato.Valor;
            }
            else if (dato.VariableId == _Vf.Id)
            {
                Vf = dato.Valor;
            }
            else if (dato.VariableId == _x.Id)
            {
                x = dato.Valor;
            }
            else if (dato.VariableId == _a.Id)
            {
                a = dato.Valor;
            }
        }
        float answer = 0;
        if (variableToAnswer.Id == _Vf.Id)
        {
            answer = Mathf.Sqrt(Mathf.Pow(Vo, 2) + (2 * a * x));
            _stp.NewText("Se usa la siguiente formula:");
            _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
            _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS($"{Vo}", 2), $"+(2·{a}·{x})")));
            _stp.NewLine("Vf=" + answer);
        }
        else if (variableToAnswer.Id == _Vo.Id)
        {
            answer = Mathf.Sqrt(Mathf.Pow(Vf, 2) - (2 * a * x));
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
            _stp.NewText("Se obtiene:");
            _stp.NewLine("Vo=", _stp.Sqrt(_stp.SupS("Vf", 2), $"-(2·{_sa}·x)"));
            _stp.NewLine("Vo=", _stp.Sqrt(_stp.SupS($"{Vf}", 2), $"-(2·{a}·{x})"));
            _stp.NewLine("Vo=" + answer);
        }
        else if (variableToAnswer.Id == _x.Id)
        {
            answer = (Mathf.Pow(Vf, 2) - Mathf.Pow(Vo, 2)) / (2 * a);
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
            _stp.NewText("Se obtiene:");
            _stp.NewLine("x=", _stp.Frac(_stp.Group(_stp.SupS("Vf", 2), "-", _stp.SupS("Vo", 2)), $"(2·{_sa})"));
            _stp.NewLine("x=", _stp.Frac(_stp.Group(_stp.SupS($"{Vf}", 2), "-", _stp.SupS($"{Vo}", 2)), $"(2·{a})"));
            _stp.NewLine("x=" + answer);
        }
        else if (variableToAnswer.Id == _a.Id)
        {
            answer = (Mathf.Pow(Vf, 2) - Mathf.Pow(Vo, 2)) / (2 * x);
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine("Vf=", _stp.Sqrt(_stp.Group(_stp.SupS("Vo", 2), $"+(2·{_sa}·x)")));
            _stp.NewText("Se obtiene:");
            _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group(_stp.SupS("Vf", 2), "-", _stp.SupS("Vo", 2)), "(2·x)"));
            _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group(_stp.SupS($"{Vf}", 2), "-", _stp.SupS($"{Vo}", 2)), $"(2·{x})"));
            _stp.NewLine($"{_sa}=" + answer);
        }
        return answer;
    }
    public override IEnumerator Calculate2(Variable variableToAnswer, Dato[] datos, Action<float> res)
    {
        throw new NotImplementedException();
    }
}
public class Formula_4 : Formula
{
    private List<Variable> _requirements;
    private float Vo, t, x, a;
    public Formula_4()
    {
        Debug.Log("Formula 4 instance");
        Requirements = new List<Variable>(new Variable[] { _Vo, _t, _x, _a });
        IsEnumerator = true;
    }
    public override List<Variable> Requirements { get => _requirements; set => _requirements = value; }
    public override float Calculate(Variable variableToAnswer, Dato[] datos)
    {
        return 0;
    }
    public override IEnumerator Calculate2(Variable variableToAnswer, Dato[] datos, Action<float> res)
    {
        Debug.Log("Se uso Formula 4 con " + variableToAnswer.Nombre);
        foreach (Dato dato in datos)
        {
            if (dato.VariableId == _Vo.Id)
            {
                Vo = dato.Valor;
            }
            else if (dato.VariableId == _t.Id)
            {
                t = dato.Valor;
            }
            else if (dato.VariableId == _x.Id)
            {
                x = dato.Valor;
            }
            else if (dato.VariableId == _a.Id)
            {
                a = dato.Valor;
            }
        }
        float answer = 0;
        if (variableToAnswer.Id == _Vo.Id)
        {
            answer = (x - (a * Mathf.Pow(t, 2) / 2)) / t;
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
            _stp.NewText("Se obtiene:");
            _stp.NewLine("Vo=", _stp.Frac(_stp.Group("x-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2)), "t"));
            _stp.NewLine("Vo=", _stp.Frac(_stp.Group($"{x}-", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2)), t));
            _stp.NewLine("Vo=", answer);
        }
        else if (variableToAnswer.Id == _x.Id)
        {
            answer = Vo * t + (a * Mathf.Pow(t, 2) / 2);
            _stp.NewText("Se usa la siguiente formula:");
            _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
            _stp.NewLine("x=", $"{Vo}·{t}+", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2));
            _stp.NewLine("x=", answer);
        }
        else if (variableToAnswer.Id == _a.Id)
        {
            answer = (x - Vo * t) * 2 / Mathf.Pow(t, 2);
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine("x=", "Vo·t+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
            _stp.NewText("Se obtiene:");
            _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group("(x-Vo·t)2"), _stp.SupS("t", 2)));
            _stp.NewLine($"{_sa}=", _stp.Frac(_stp.Group($"({x}-{Vo}·{t})2"), _stp.SupS(t, 2)));
            _stp.NewLine($"{_sa}=", answer);
        }
        else if (variableToAnswer.Id == _t.Id)
        {
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
            }
            else
            {
                _stp.NewLine(_stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2), "Vo·t-x=0 //·2");
                _stp.NewLine($"{_sa}·", _stp.SupS("t", 2), "+2·Vo·t-2·x=0");
                _stp.NewLine($"{a}·", _stp.SupS(t, 2), $"+2·{Vo}·{t}-2·{x}=0");
                yield return QuadraticFormula(a, 2 * Vo, -2 * x, (res) =>
                {
                    answer = res;
                });
            }
        }
        res(answer);
    }
}
public class Formula_5 : Formula
{
    private List<Variable> _requirements;
    private float Vf, t, x, a;
    public Formula_5()
    {
        Debug.Log("Formula 5 instance");
        Requirements = new List<Variable>(new Variable[] { _Vf, _t, _x, _a });
        IsEnumerator = true;
    }
    public override List<Variable> Requirements { get => _requirements; set => _requirements = value; }
    public override float Calculate(Variable variableToAnswer, Dato[] datos)
    {
        return 0;
    }
    public override IEnumerator Calculate2(Variable variableToAnswer, Dato[] datos, Action<float> res)
    {
        Debug.Log("Se uso Formula 5 con " + variableToAnswer.Nombre);
        foreach (Dato dato in datos)
        {
            if (dato.VariableId == _Vf.Id)
            {
                Vf = dato.Valor;
            }
            else if (dato.VariableId == _t.Id)
            {
                t = dato.Valor;
            }
            else if (dato.VariableId == _x.Id)
            {
                x = dato.Valor;
            }
            else if (dato.VariableId == _a.Id)
            {
                a = dato.Valor;
            }
        }
        float answer = 0;
        if (variableToAnswer.Id == _Vf.Id)
        {
            answer = (x + (a * Mathf.Pow(t, 2) / 2)) / t;
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
            _stp.NewText("Se obtiene:");
            _stp.NewLine("Vf=", _stp.Frac(_stp.Group("x+", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2)), "t"));
            _stp.NewLine("Vf=", _stp.Frac(_stp.Group($"{x}-", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2)), t));
            _stp.NewLine("Vf=", answer);
        }
        else if (variableToAnswer.Id == _x.Id)
        {
            answer = Vf * t - (a * Mathf.Pow(t, 2) / 2);
            _stp.NewText("Se usa la siguiente formula:");
            _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
            _stp.NewLine("x=", $"{Vf}·{t}-", _stp.Frac(1, 2), $"{a}·", _stp.SupS(t, 2));
            _stp.NewLine("x=", answer);
        }
        else if (variableToAnswer.Id == _a.Id)
        {
            answer = -((x - Vf * t) * 2 / Mathf.Pow(t, 2));
            _stp.NewText("Despejando de la formula principal:");
            _stp.NewLine("x=", "Vf·t-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2));
            _stp.NewText("Se obtiene:");
            _stp.NewLine($"{_sa}=", "-", _stp.Frac(_stp.Group("(x-Vf·t)2"), _stp.SupS("t", 2)));
            _stp.NewLine($"{_sa}=", "-", _stp.Frac(_stp.Group($"({x}-{Vf}·{t})2"), _stp.SupS(t, 2)));
            _stp.NewLine($"{_sa}=", answer);
        }
        else if (variableToAnswer.Id == _t.Id)
        {
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
            }
            else
            {
                _stp.NewLine("-", _stp.Frac(1, 2), $"{_sa}·", _stp.SupS("t", 2), "+Vf·t-x=0 //·-2");
                _stp.NewLine($"{_sa}·", _stp.SupS("t", 2), "-2·Vf·t+2·x=0");
                _stp.NewLine($"{a}·", _stp.SupS("t", 2), $"-2·{Vf}·t+2·{x}=0");
                yield return QuadraticFormula(a, -2 * Vf, 2 * x, (res) =>
                {
                    answer = res;
                });
            }
        }
        res(answer);
    }
}

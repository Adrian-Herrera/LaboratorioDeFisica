using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum VariableEnum
{
    Velocidad, Distancia, Tiempo, VelocidadInicial, VelocidadFinal, Aceleracion
}
public static class VariableHelper
{

    public static VariableTemplate Velocidad = new("Velocidad", "v");
    public static VariableTemplate Distancia = new("Distancia", "x");
    public static VariableTemplate Tiempo = new("Tiempo", "t");
    public static VariableTemplate VelocidadInicial = new("Velocidad Inicial", "Vo");
    public static VariableTemplate VelocidadFinal = new("Velocidad Final", "Vf");
    public static VariableTemplate Aceleracion = new("Aceleración", "a");
    public static Dictionary<VariableEnum, VariableTemplate> VariablePairs = new(){
        {VariableEnum.Velocidad, Velocidad},
        {VariableEnum.Distancia, Distancia},
        {VariableEnum.Tiempo, Tiempo},
        {VariableEnum.VelocidadInicial, VelocidadInicial},
        {VariableEnum.VelocidadFinal, VelocidadFinal},
        {VariableEnum.Aceleracion, Aceleracion},
    };
}
[Serializable]
public class VariableTemplate
{
    public string Name;
    private string _abrev;
    public string Abrev
    {
        get { return "(" + _abrev + ")"; }
        set { _abrev = value; }
    }
    public VariableTemplate(string name, string abrev)
    {
        Abrev = abrev;
        Name = name;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class VariableUnity
{
    public float Value;
    public TipoVariable TipoVariable;
    public VariableUnity(TipoVariable tipo)
    {
        TipoVariable = tipo;
    }
    public VariableUnity(TipoVariable tipo, float value)
    {
        TipoVariable = tipo;
        Value = value;
    }
}
[Serializable]
public class TipoVariable
{
    [SerializeField] private int _id;
    [SerializeField] private string _nombre;
    // public bool Activo = false;
    public int Id
    {
        get { return _id; }
        private set { _id = value; }
    }
    public string Nombre
    {
        get { return _nombre; }
        private set { _nombre = value; }
    }
    public TipoVariable(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}
public class BaseVariable
{
    public static TipoVariable Velocidad = new(1, "Velocidad");
    public static TipoVariable Distancia = new(2, "Distancia");
    public static TipoVariable Tiempo = new(3, "Tiempo");
    public static TipoVariable VelocidadInicial = new(4, "Velocidad Inicial");
    public static TipoVariable VelocidadFinal = new(5, "Velocidad Final");
    public static TipoVariable Aceleracion = new(6, "Aceleracion");
}

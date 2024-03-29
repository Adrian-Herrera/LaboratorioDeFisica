using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class VariableUnity : ICloneable
{
    [SerializeField] private float _value;
    public float Value
    {
        get { return _value; }
        set
        {
            _value = value;
            OnChangeValue?.Invoke();
        }
    }
    public TipoVariable TipoVariable;
    public event Action OnChangeValue;
    public VariableUnity(TipoVariable tipo)
    {
        TipoVariable = tipo;
    }
    public VariableUnity(TipoVariable tipo, float value)
    {
        TipoVariable = tipo;
        Value = value;
    }
    public object Clone()
    {
        return MemberwiseClone(); //create a shallow-copy of the object
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
    public static TipoVariable Altura = new(7, "Altura");
    public static TipoVariable Gravedad = new(8, "Gravedad");
    // Dinamica
    public static TipoVariable Masa = new(9, "Masa");
    public static TipoVariable Tension = new(10, "Tension");
    public static TipoVariable Peso = new(11, "Peso");
    public static TipoVariable Normal = new(12, "Normal");
}

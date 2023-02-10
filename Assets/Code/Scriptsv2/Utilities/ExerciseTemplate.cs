using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ExerciseTemplate
{
    public Dictionary<int, VariableLocal> Variables = new();
    // POR SEGMENTO
    public VariableLocal Velocidad = new(1, "Velocidad");
    public VariableLocal Distancia = new(2, "Distancia");
    public VariableLocal Tiempo = new(3, "Tiempo");
    public VariableLocal VelocidadInicial = new(4, "Velocidad Inicial");
    public VariableLocal VelocidadFinal = new(5, "Velocidad Final");
    public VariableLocal Aceleracion = new(6, "Aceleracion");
    public VariableLocal Altura;
    // TOTALES
    public VariableLocal TiempoTotal;
    public VariableLocal DistanciaTotal;
    public VariableLocal Gravedad;
    public VariableLocal AlturaMaxima;
    public VariableLocal AlturaInicial;
    public VariableLocal AlturaTotal;
    public VariableLocal DistanciaMaxima;
    public VariableLocal TiempoDeVuelo;
    public VariableLocal TiempoDeSubida;
    public VariableLocal TiempoDeBajada;
    public void ActivarMru()
    {
        Variables.Clear();
        ActivateVariable(Velocidad);
        ActivateVariable(Distancia);
        ActivateVariable(Tiempo);
    }
    public void ActivarMruv()
    {
        Variables.Clear();
        ActivateVariable(Distancia);
        ActivateVariable(Tiempo);
        ActivateVariable(VelocidadInicial);
        ActivateVariable(VelocidadFinal);
        ActivateVariable(Aceleracion);
    }
    private void ActivateVariable(VariableLocal variable)
    {
        Variables.Add(variable.Id, variable);
        variable.Activo = true;
    }
}
[Serializable]
public class VariableLocal
{
    [SerializeField] private int _id;
    [SerializeField] private string _nombre;
    [SerializeField] private float _valor;
    public bool Activo = false;
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
    public float Valor
    {
        get { return _valor; }
        set
        {
            if (Activo == false)
            {
                Debug.LogWarning("La variable " + Nombre + " no esta activada");
                return;
            }
            _valor = value;
        }
    }
    public VariableLocal(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}

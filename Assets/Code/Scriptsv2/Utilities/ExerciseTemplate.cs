using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ExerciseTemplate
{
    // public Dictionary<int, TipoVariable> Variables = new();
    public List<VariableUnity> Variables = new();
    public VariableUnity FindVarById(int id)
    {
        return Variables.Find(e => id == e.TipoVariable.Id);
    }
    public VariableUnity FindVarByType(TipoVariable tipo)
    {
        return Variables.Find(e => tipo == e.TipoVariable);
    }
    // POR SEGMENTO
    // public TipoVariable Velocidad = new(1, "Velocidad");
    // public TipoVariable Distancia = new(2, "Distancia");
    // public TipoVariable Tiempo = new(3, "Tiempo");
    // public TipoVariable VelocidadInicial = new(4, "Velocidad Inicial");
    // public TipoVariable VelocidadFinal = new(5, "Velocidad Final");
    // public TipoVariable Aceleracion = new(6, "Aceleracion");
    // public TipoVariable Altura;
    // // TOTALES
    // public TipoVariable TiempoTotal;
    // public TipoVariable DistanciaTotal;
    // public TipoVariable Gravedad;
    // public TipoVariable AlturaMaxima;
    // public TipoVariable AlturaInicial;
    // public TipoVariable AlturaTotal;
    // public TipoVariable DistanciaMaxima;
    // public TipoVariable TiempoDeVuelo;
    // public TipoVariable TiempoDeSubida;
    // public TipoVariable TiempoDeBajada;
    public void ActivarMru()
    {
        Variables.Clear();
        ActivateVariable(BaseVariable.Velocidad);
        ActivateVariable(BaseVariable.Distancia);
        ActivateVariable(BaseVariable.Tiempo);
    }
    public void ActivarMruv()
    {
        Variables.Clear();
        ActivateVariable(BaseVariable.Distancia);
        ActivateVariable(BaseVariable.Tiempo);
        ActivateVariable(BaseVariable.VelocidadInicial);
        ActivateVariable(BaseVariable.VelocidadFinal);
        ActivateVariable(BaseVariable.Aceleracion);
    }
    private void ActivateVariable(TipoVariable variable)
    {
        VariableUnity varUnity = new(variable);
        Variables.Add(varUnity);
        // variable.Activo = true;
    }
}

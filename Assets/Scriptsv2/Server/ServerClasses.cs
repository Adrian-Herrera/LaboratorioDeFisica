using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class ServerClasses
{
}
[Serializable]
public class Cuestionario
{
    public int Id;
    public string Titulo;
    public int TiempoLimite;
    public Pregunta[] Preguntas;
}
[Serializable]
public class Pregunta
{
    public string Texto;
    public Dato[] Datos;
}
[Serializable]
public class Dato
{
    public float _valor;
    public float Valor
    {
        get { return _valor; }
        set
        {
            Debug.Log("Valor change to " + value);
            _valor = value;
            // Debug.Log(dataPropertie);
            if (dataPropertie != null) dataPropertie.ChangeText(value);

        }
    }
    public int Segmento;
    public int VariableId;
    public int TipoDatoId;
    public int UnidadId;
    public bool IsAnswered = false;
    public DataPropertie dataPropertie;
    public Dato(int variableId, float valor, int tipoDatoId, int unidadId)
    {
        VariableId = variableId;
        Valor = valor;
        TipoDatoId = tipoDatoId;
        UnidadId = unidadId;
    }

}

[Serializable]
public class Variable
{
    public int Id;
    public string Nombre;
    public string Abrev;
}
[Serializable]
public class Unidad
{
    public int Id;
    public string Abrev;
}
[Serializable]
public class Historial
{
    public int Id;
    public int Puntaje;
    public int AlumnoId;
    public int CuestionarioId;
    public int NumeroIntento;
}
[Serializable]
public class Tema
{
    public int Id;
    public string Nombre;
}
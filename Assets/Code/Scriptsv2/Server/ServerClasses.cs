﻿using System.Collections;
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
    public int Id;
    public string Texto;
    public Dato[] Datos;
}
[Serializable]
public class Dato
{
    public int Id;
    public float Valor;
    public string Text;
    public int Segmento;
    public int VariableId;
    public int TipoDatoId;
    public int UnidadId;
    public Variable Variable;
    public Unidad Unidad;
    public bool IsAnswered = false;
    public DataPropertie dataPropertie;
    public void ChangeValor(float newValue)
    {
        Valor = newValue;
        if (dataPropertie != null) dataPropertie.ChangeText(newValue);
    }
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
[Serializable]
public class Reto
{
    public int Id;
    public int CodigoId;
    public string Titulo;
    public RetoDato[] RetoDatos;
}
[Serializable]
public class RetoDato
{
    public float Valor;
    public bool EsDato;
    public Variable Variable;
    // public string Descripcion;
}
[Serializable]
public class LogInfo
{
    public int UserId;
    public int ActivePregunta;
    public LogPregunta[] Preguntas;
}
[Serializable]
public class LogPregunta
{
    public int PreguntaId;
    public int Tiempo;
    public LogDato[] Datos;

}
[Serializable]
public class LogDato
{
    public int DatoId;
    public float[] Respuestas;
}
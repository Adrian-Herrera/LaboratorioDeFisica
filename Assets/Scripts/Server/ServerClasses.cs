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
    public string Titulo;
    public float TiempoLimite;
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
    public string VarName;
    public float Valor;
    public int VariableId;
    public int TipoDatoId;
    public Var Variable;
    public bool IsAnswered = false;
}

[Serializable]
public class Var
{
    public int Id;
    public string Nombre;
    public string Abrev;
}
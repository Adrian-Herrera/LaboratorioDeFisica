using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlertMessages
{

    public TimeAlerts timeAlerts = new TimeAlerts();
    public DistanceAlerts distanceAlerts = new DistanceAlerts();
    public FieldAlerts fieldAlerts = new FieldAlerts();
}

public class TimeAlerts
{
    public string NotEqual = "El tiempo final introducido no corresponde con la suma de los tiempos.";
    public string BadInput = "No se pudo calcular el tiempo faltante ya que la suma de los anteriores tiempos ya llego al tiempo final.";
}
public class DistanceAlerts
{
    public string NotEqual = "La distancia final introducida no corresponde con la suma de las distancias.";
    public string BadInput = "No se pudo calcular la distancia faltante ya que la suma de las anteriores distancias ya llego a la distancia final.";
}
public class FieldAlerts
{
    public string ThreeInputRequired = "Debe ingresar un minimo de 3 variables en las casillas pintadas para poder resolver el ejercicio.";
}


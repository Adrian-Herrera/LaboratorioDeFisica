using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MainObjectConfig", menuName = "ScriptableObject/MainObjectConfig")]
public class MainObjectConfig : ScriptableObject //Posible ScriptableObject
{
    public Variables[] names = {
        new Variables(0, "Vo", "Velocidad Inicial"),
        new Variables(1, "Vf", "Velocidad Final"),
        new Variables(2, "a", "Aceleración"),
        new Variables(3, "x", "Distancia"),
        new Variables(4, "t", "Tiempo")
        };
    public Variables[] extraNames = {
        new Variables(0, "Xt", "Distancia Total"),
        new Variables(1, "Tt", "Tiempo Total"),
        };
}
[System.Serializable]
public class Variables
{
    public int id;
    public string longName;
    public string shortName;
    public Variables(int nid, string sname, string lname)
    {
        id = nid;
        shortName = sname;
        longName = lname;
    }
}

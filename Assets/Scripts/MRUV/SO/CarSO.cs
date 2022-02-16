﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[CreateAssetMenu(fileName = "CarSO", menuName = "ScriptableObject/ CarSO")]
public class CarSO : ScriptableObject
{
    public Sprite _sprite;
    public Field[,] Datos = new Field[3, 5]; // { Vo, Vf, a, x, t }
    public Field TotalTime, TotalDistance;
    public int numberOfSegments;
    private void OnEnable()
    {
        numberOfSegments = 1;
    }
    public void ShowDatos()
    {
        string data = "";
        for (int i = 0; i < Datos.GetLength(0); i++)
        {
            for (int j = 0; j < Datos.GetLength(1); j++)
            {
                data += Datos[i, j].value.ToString() + " ";
            }
            data += "\n";
        }
        Debug.Log(data);
        Debug.Log("TotalTime: " + TotalTime.value);
        Debug.Log("TotalDistance: " + TotalDistance.value);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[CreateAssetMenu(fileName = "CarSO", menuName = "ScriptableObject/ CarSO")]
public class CarSO : ScriptableObject
{
    public Sprite _sprite;
    public Field[,] Datos = new Field[3, 5];

    public Field TotalTime, TotalDistance;

    private enum Variables { Vo, Vf, a, x, t };
    public int numberOfSegments;
    public int selectedSegment;
    private void OnEnable()
    {
        // initializeData();
        numberOfSegments = 1;
        selectedSegment = 0;
    }
    // private void initializeData()
    // {
    //     for (int i = 0; i < Datos.GetLength(0); i++)
    //     {
    //         for (int j = 0; j < Datos.GetLength(1); j++)
    //         {
    //             Datos[i, j] = new Item(i, j);
    //         }
    //     }
    // }

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

    // public float getVelocity(int segment)
    // {
    //     // Calculate();
    //     return Datos[segment, (int)Variables.Vo].itemValue;
    // }
    // public float getAcc(int segment)
    // {
    //     return Datos[segment, (int)Variables.a].itemValue;
    // }
    // public float getTime(int segment)
    // {
    //     return Datos[segment, (int)Variables.t].itemValue;
    // }
    // public float getDist(int segment)
    // {
    //     return Datos[segment, (int)Variables.x].itemValue;
    // }


}


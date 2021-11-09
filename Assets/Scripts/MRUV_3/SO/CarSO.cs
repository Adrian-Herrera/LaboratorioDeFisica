using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "CarSO", menuName = "ScriptableObject/ CarSO")]

public class CarSO : ScriptableObject
{
    public Sprite _sprite;
    public float[,] Datos = new float[3, 5];
    public int numberSegments;
    private void OnEnable()
    {
        numberSegments = 1;
    }

    public void ShowDatos()
    {
        string data = "";
        for (int i = 0; i < Datos.GetLength(0); i++)
        {
            for (int j = 0; j < Datos.GetLength(1); j++)
            {
                data += Datos[i, j].ToString() + " ";
            }
            data += "\n";
        }
        Debug.Log(data);
    }
}

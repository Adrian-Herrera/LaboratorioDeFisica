using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePointSO : ScriptableObject
{
    public Sprite _sprite;
    public Field[,] Datos;
    public Dictionary<string, Field> ExtraFields = new Dictionary<string, Field>();
    public int numberOfSegments;
    protected virtual void OnEnable()
    {
        numberOfSegments = 1;
        ExtraFields.Clear();
        // setDefaultValues(Datos);
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
        // Debug.Log("TotalTime: " + TotalTime.value);
        // Debug.Log("TotalDistance: " + TotalDistance.value);

        foreach (var item in ExtraFields)
        {
            Debug.Log(item.Key + ": " + item.Value.value);
        }
    }
    public virtual void AddExtraFields(string name, Field field)
    {
        ExtraFields.Add(name, field);
    }

    public abstract string[] getNames();
    public abstract void setDefaultValues();
}

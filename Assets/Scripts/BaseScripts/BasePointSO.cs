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
    public virtual void ChangeFieldValue(int segmentId, int fieldId, float value)
    {
        Datos[segmentId, fieldId].value = value;
        Datos[segmentId, fieldId].SetInteractable(false);
    }
    public virtual void ChangeFieldInteractable(int segmentId, int fieldId, bool value)
    {
        Datos[segmentId, fieldId].SetInteractable(value);
    }
    public virtual void ResetValues()
    {
        int iLength = Datos.GetLength(0);
        int jLength = Datos.GetLength(1);
        for (int i = 0; i < iLength; i++)
        {
            for (int j = 0; j < jLength; j++)
            {
                Datos[i, j].SetDefaultValues();
            }
        }
        foreach (Field item in ExtraFields.Values)
        {
            item.SetDefaultValues();
        }
    }
    public Field GetField(int i, int j)
    {
        return Datos[i, j];
    }
}

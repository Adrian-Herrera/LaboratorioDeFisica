using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRUVFormulary : BasicFormulary
{
    public void checkTime(BasePointSO carSO)
    {
        int ValidData = 0;
        float FinalTime = 0;
        Field field = carSO.ExtraFields["Tiempo Total"];
        field.error = false;
        for (int i = 0; i < carSO.numberOfSegments; i++)
        {
            if (carSO.Datos[i, 4].status)
            {
                ValidData++;
                FinalTime += carSO.Datos[i, 4].value;
            }
        }
        if (ValidData == (carSO.numberOfSegments - 1) && field.status)
        {
            for (int i = 0; i < carSO.numberOfSegments; i++)
            {
                if (!carSO.Datos[i, 4].status)
                {
                    if ((field.value - FinalTime) <= 0)
                    {
                        Debug.Log("alertMessages.timeAlerts.BadInput");
                    }
                    else
                    {
                        carSO.Datos[i, 4].value = field.value - FinalTime;
                    }
                }
            }
        }
        else if (ValidData == carSO.numberOfSegments && !field.status)
        {
            field.value = FinalTime;
        }
        else if (ValidData == carSO.numberOfSegments && field.status)
        {
            if (field.value != FinalTime)
            {
                Debug.Log("alertMessages.timeAlerts.NotEqual");
            }
        }
    }
    public void checkDistance(CarSO carSO)
    {
        int ValidData = 0;
        float FinalDistance = 0;
        Field field = carSO.ExtraFields["Distancia Total"];
        field.error = false;
        for (int i = 0; i < carSO.numberOfSegments; i++)
        {
            if (carSO.Datos[i, 3].status)
            {
                ValidData++;
                FinalDistance += carSO.Datos[i, 3].value;
            }
        }
        if (ValidData == (carSO.numberOfSegments - 1) && field.status)
        {
            for (int i = 0; i < carSO.numberOfSegments; i++)
            {
                if (!carSO.Datos[i, 3].status)
                {
                    if ((field.value - FinalDistance) <= 0)
                    {
                        Debug.Log("Revise las distancias");  // Añadir error
                    }
                    else
                    {
                        carSO.Datos[i, 3].value = field.value - FinalDistance;
                    }
                }
            }
        }
        else if (ValidData == carSO.numberOfSegments && !field.status)
        {
            field.value = FinalDistance;
        }
        else if (ValidData == carSO.numberOfSegments && field.status)
        {
            if (field.value != FinalDistance)
            {
                Debug.Log("Las distancias no son iguales"); //añadir error
            }
        }
    }
}

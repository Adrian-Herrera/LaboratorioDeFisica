using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraInfo : MonoBehaviour
{
    [SerializeField] private CarSO carSO;
    private Field TotalTimeField, TotalDistanceField;
    private AlertMessages alertMessages = new AlertMessages();
    public Field[] fields;

    private void Awake()
    {
        fields = GetComponentsInChildren<Field>();
        TotalTimeField = fields[0];
        TotalDistanceField = fields[1];
    }
    private void Start()
    {
        carSO.TotalTime = TotalTimeField;
        carSO.TotalDistance = TotalDistanceField;
    }

    public void checkTime()
    {
        // Debug.Log("CheckTime");
        int ValidData = 0;
        float FinalTime = 0;
        TotalTimeField.error = false;
        for (int i = 0; i < carSO.numberOfSegments; i++)
        {
            if (carSO.Datos[i, 4].status)
            {
                ValidData++;
                FinalTime += carSO.Datos[i, 4].value;
            }
        }
        // Debug.Log("ValidData: " + ValidData);
        if (ValidData == (carSO.numberOfSegments - 1) && carSO.TotalTime.status)
        {
            for (int i = 0; i < carSO.numberOfSegments; i++)
            {
                if (!carSO.Datos[i, 4].status)
                {
                    if ((carSO.TotalTime.value - FinalTime) <= 0)
                    {
                        Debug.Log("alertMessages.timeAlerts.BadInput");
                        TotalTimeField.ShowError(alertMessages.timeAlerts.BadInput);
                    }
                    else
                    {
                        carSO.Datos[i, 4].value = carSO.TotalTime.value - FinalTime;
                    }
                }
            }
        }
        else if (ValidData == carSO.numberOfSegments && !carSO.TotalTime.status)
        {
            carSO.TotalTime.value = FinalTime;
        }
        else if (ValidData == carSO.numberOfSegments && carSO.TotalTime.status)
        {
            if (carSO.TotalTime.value != FinalTime)
            {
                Debug.Log("alertMessages.timeAlerts.NotEqual");
                TotalTimeField.ShowError(alertMessages.timeAlerts.NotEqual);
                // StartCoroutine(TotalTimeField.ShowMessage(alertMessages.timeAlerts.NotEqual));
                // _carSO.TotalTime.ShowMessage(alertMessages.timeAlerts.NotEqual);
            }
        }
    }
    public void checkDistance()
    {
        // Debug.Log("CheckDistance");
        int ValidData = 0;
        float FinalDistance = 0;
        for (int i = 0; i < carSO.numberOfSegments; i++)
        {
            if (carSO.Datos[i, 3].status)
            {
                ValidData++;
                // Debug.Log("_carSO.Datos[" + i + ", 3].itemValue: " + _carSO.Datos[i, 3].itemValue);
                FinalDistance += carSO.Datos[i, 3].value;
                // Debug.Log("FinalDistance: " + FinalDistance);
            }
        }
        // Debug.Log("ValidData: " + ValidData);
        if (ValidData == (carSO.numberOfSegments - 1) && carSO.TotalDistance.status)
        {
            for (int i = 0; i < carSO.numberOfSegments; i++)
            {
                if (!carSO.Datos[i, 3].status)
                {
                    if ((carSO.TotalDistance.value - FinalDistance) <= 0)
                    {
                        Debug.Log("Revise las distancias");  // Añadir error
                    }
                    else
                    {
                        carSO.Datos[i, 3].value = carSO.TotalDistance.value - FinalDistance;
                    }
                }
            }
        }
        else if (ValidData == carSO.numberOfSegments && !carSO.TotalDistance.status)
        {
            carSO.TotalDistance.value = FinalDistance;
        }
        else if (ValidData == carSO.numberOfSegments && carSO.TotalDistance.status)
        {
            if (carSO.TotalDistance.value != FinalDistance)
            {
                Debug.Log("Las distancias no son iguales"); //añadir error
            }
        }
    }
}

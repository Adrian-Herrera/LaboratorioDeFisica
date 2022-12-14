using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaLibreFormulary : BasicFormulary
{
    public void AlturaInicial(BasePointSO basePointSO)
    {
        Field field = basePointSO.ExtraFields["Ho"];
        field.error = false;
        if (((BallSO)basePointSO).isRising())
        {
            Debug.Log("esta subiendo");
            if (basePointSO.ExtraFields["h-max"].status && basePointSO.Datos[0, 3].status)
            {
                field.value = basePointSO.ExtraFields["h-max"].value - basePointSO.Datos[0, 3].value;
            }
        }
        else
        {
            Debug.Log("esta bajando");
        }
    }
    public void AlturaMaxima(BasePointSO basePointSO)
    {
        Field field = basePointSO.ExtraFields["h-max"];
        field.error = false;
        if (((BallSO)basePointSO).isRising())
        {
            Debug.Log("esta subiendo");
            if (basePointSO.ExtraFields["Ho"].status && basePointSO.Datos[0, 3].status)
            {
                field.value = basePointSO.ExtraFields["Ho"].value + basePointSO.Datos[0, 3].value;
            }
        }
        else
        {
            Debug.Log("esta bajando");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicFormulary : BasicFormulary
{
    string[] test = { "Angulo", "y-max", "tv", "x-max" }; // { Voy, Vy, g, y, t, Vox, x, V }
    public float Angulo(BasePointSO basePointSO)
    {
        Debug.Log("Se uso angulo");
        // Field field = basePointSO.ExtraFields["Angulo"];
        // field.error = false;
        return Mathf.Sqrt(Mathf.Pow(basePointSO.Datos[0, 0].value, 2) + Mathf.Pow(basePointSO.Datos[0, 5].value, 2));
    }
    public float AlturaMaxima(BasePointSO basePointSO)
    {
        Debug.Log("Se uso AlturaMaxima");
        float num, dem;
        dem = 2 * basePointSO.Datos[0, 2].value;
        Debug.Log($"basePointSO.Datos[0, 7].status: {basePointSO.Datos[0, 7].status} - {basePointSO.Datos[0, 7].value}");
        Debug.Log($"basePointSO.Datos[0, 8].status: {basePointSO.Datos[0, 8].status} - {basePointSO.Datos[0, 8].value}");
        if (basePointSO.Datos[0, 7].status == true && basePointSO.Datos[0, 8].status == true)
        {
            num = Mathf.Pow(basePointSO.Datos[0, 7].value * (Mathf.Sin(basePointSO.Datos[0, 8].value * Mathf.PI / 180)), 2);
            Debug.Log($"Seno: {Mathf.Sin(basePointSO.Datos[0, 8].value * Mathf.PI / 180)}");
            Debug.Log($"num: {num}, dem: {dem}");
            return num / dem;
        }
        else if (basePointSO.Datos[0, 0].status == true)
        {
            num = Mathf.Pow(basePointSO.Datos[0, 0].value, 2);
            Debug.Log($"num: {num}, dem: {dem}");
            return num / dem;
        }
        Debug.Log("Altura Maxima - No se resolvio");
        return 0;
    }
    public float TiempoVuelo(BasePointSO basePointSO)
    {
        Debug.Log("Se uso TiempoVuelo");
        float num, dem;
        dem = basePointSO.Datos[0, 2].value;
        if (basePointSO.Datos[0, 7].status == true && basePointSO.Datos[0, 8].status == true)
        {
            num = 2 * basePointSO.Datos[0, 7].value * (Mathf.Sin(basePointSO.Datos[0, 8].value * Mathf.PI / 180));
            return num / dem;
        }
        else if (basePointSO.Datos[0, 0].status == true)
        {
            num = 2 * basePointSO.Datos[0, 0].value;
            return num / dem;
        }
        Debug.Log("TiempoVuelo - No se resolvio");
        return 0;
    }
    public float AlcanceMaximo(BasePointSO basePointSO)
    {
        Debug.Log("Se uso AlcanceMaximo");
        float num, dem;
        dem = basePointSO.Datos[0, 2].value;
        if (basePointSO.Datos[0, 7].status == true && basePointSO.Datos[0, 8].status == true)
        {
            num = Mathf.Pow(basePointSO.Datos[0, 7].value, 2) * Mathf.Sin(2 * basePointSO.Datos[0, 8].value * Mathf.PI / 180);
            return num / dem;
        }
        else if (basePointSO.Datos[0, 5].status == true && basePointSO.Datos[0, 4].status)
        {
            return basePointSO.Datos[0, 5].value * basePointSO.Datos[0, 4].value;
        }
        Debug.Log("AlcanceMaximo - No se resolvio");
        return 0;
    }
}

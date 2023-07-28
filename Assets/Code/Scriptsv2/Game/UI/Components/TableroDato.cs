using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TableroDato : MonoBehaviour
{
    [SerializeField] private TMP_Text NombreText;
    [SerializeField] private TMP_Text ValorText;
    [SerializeField] private TMP_Text MagnitudText;
    public void Init(Dato dato)
    {
        NombreText.text = dato.TipoVariable.Nombre;
        ValorText.text = dato.Valor.ToString();
        MagnitudText.text = dato.Magnitud is not null ? dato.Magnitud.Nombre : "";
    }
    public void Init(string nombre, float value, string mag)
    {
        NombreText.text = nombre;
        ValorText.text = value.ToString();
        MagnitudText.text = mag;
    }
    public void UpdateValue(float newValue)
    {
        ValorText.text = newValue.ToString();
    }
    public void UpdateValue(string newValue)
    {
        ValorText.text = newValue;
    }
}

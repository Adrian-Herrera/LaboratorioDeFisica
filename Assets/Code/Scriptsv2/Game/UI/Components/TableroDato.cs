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
        // MagnitudText.text = dato.
    }
}

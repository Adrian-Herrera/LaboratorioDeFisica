using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineInfo : MonoBehaviour
{
    private TMP_Text _text;
    private Dato _dato;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
    // Start is called before the first frame update
    public void Init(Dato dato)
    {
        string variable = QuizManager.Current._variables[dato.VariableId - 1].Abrev;
        string unidad = QuizManager.Current._unidades[dato.UnidadId - 1].Abrev;
        string segmento = $"<sub>{dato.Segmento}</sub>";
        string newText = variable;
        if (dato.Segmento > 0) newText += segmento;
        if (dato.TipoDatoId == 1) newText += $"={dato.Valor}{unidad}";
        else newText += $"=?";

        _text.text = newText;
    }
}

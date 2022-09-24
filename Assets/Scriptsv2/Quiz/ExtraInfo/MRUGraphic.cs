using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MRUGraphic : MonoBehaviour
{
    [SerializeField] private HorizontalLine[] _lines;
    [SerializeField] private GameObject _totalLine;
    [SerializeField] private TMP_Text _totalSize;
    [SerializeField] private TMP_Text _totalTime;
    private void Awake()
    {
        _lines = GetComponentsInChildren<HorizontalLine>();
    }
    public void Init(Pregunta pregunta)
    {

        // Dato[] datos = pregunta.info
        List<Dato> segmento0 = new();
        List<Dato> segmento1 = new();
        List<Dato> segmento2 = new();
        List<Dato> segmento3 = new();
        _lines[1].gameObject.SetActive(false);
        _lines[2].gameObject.SetActive(false);
        _totalSize.gameObject.SetActive(false);
        _totalTime.gameObject.SetActive(false);
        _totalLine.SetActive(false);
        foreach (Dato dato in pregunta.Datos)
        {
            switch (dato.Segmento)
            {
                case 1:
                    segmento1.Add(dato);
                    break;
                case 2:
                    segmento2.Add(dato);
                    break;
                case 3:
                    segmento3.Add(dato);
                    break;
                default:
                    segmento0.Add(dato);
                    break;
            }
        }

        _lines[0].Init(segmento1.ToArray());

        if (segmento2.Count > 0)
        {
            _lines[1].gameObject.SetActive(true);
            _lines[1].Init(segmento2.ToArray());
        }
        if (segmento3.Count > 0)
        {
            _lines[2].gameObject.SetActive(true);
            _lines[2].Init(segmento3.ToArray());
        }
        if (segmento0.Count > 0)
        {
            _totalLine.SetActive(true);
            string unidad;
            for (int i = 0; i < segmento0.Count; i++)
            {
                switch (segmento0[i].VariableId)
                {
                    case 7: // Tiempo Total
                        _totalTime.gameObject.SetActive(true);
                        // unidad = QuizManager.Current._unidades[segmento0[i].UnidadId - 1].Abrev;
                        _totalTime.text = GetDatoInfo(segmento0[i]);
                        break;
                    case 8: // Distancia Total
                        _totalSize.gameObject.SetActive(true);
                        // unidad = QuizManager.Current._unidades[segmento0[i].UnidadId - 1].Abrev;
                        _totalSize.text = GetDatoInfo(segmento0[i]);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public string GetDatoInfo(Dato dato)
    {
        string variable = QuizManager.Current._variables[dato.VariableId - 1].Abrev;
        string unidad = QuizManager.Current._unidades[dato.UnidadId - 1].Abrev;
        string segmento = $"<sub>{dato.Segmento}</sub>";
        string newText = variable;
        if (dato.Segmento > 0) newText += segmento;
        if (dato.TipoDatoId == 1) newText += $"={dato.Valor}{unidad}";
        else newText += $"=?";

        return newText;
    }
}
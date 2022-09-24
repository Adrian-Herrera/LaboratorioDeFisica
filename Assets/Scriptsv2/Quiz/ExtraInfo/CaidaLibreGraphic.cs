using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CaidaLibreGraphic : MonoBehaviour
{
    [SerializeField] private CurveLine line;

    [SerializeField] private TMP_Text _VoText;
    [SerializeField] private TMP_Text _VfText;
    [SerializeField] private TMP_Text _hInicialText;
    [SerializeField] private TMP_Text _hMaxText;
    [SerializeField] private TMP_Text _HText;
    [SerializeField] private TMP_Text _TvText;

    [SerializeField] private GameObject _arrow;
    public void Init(Pregunta pregunta)
    {
        line.Init();
        _VoText.gameObject.SetActive(false);
        _VfText.gameObject.SetActive(false);
        _hInicialText.gameObject.SetActive(false);
        _hMaxText.gameObject.SetActive(false);
        _HText.gameObject.SetActive(false);
        _TvText.gameObject.SetActive(false);
        for (int i = 0; i < pregunta.Datos.Length; i++)
        {
            switch (pregunta.Datos[i].VariableId)
            {
                case 4: // Velocidad Inicial
                    _VoText.gameObject.SetActive(true);
                    _VoText.text = GetDatoInfo(pregunta.Datos[i]);
                    break;
                case 5: // Velocidad Final
                    _VfText.gameObject.SetActive(true);
                    _VfText.text = GetDatoInfo(pregunta.Datos[i]);
                    break;
                // case 6: // Aceleración
                //     _aText.gameObject.SetActive(true);
                //     _aText.text = GetDatoInfo(pregunta.Datos[i]);
                //     break;
                case 11: // Altura Maxima
                    _hMaxText.gameObject.SetActive(true);
                    _hMaxText.text = GetDatoInfo(pregunta.Datos[i]);
                    break;
                case 12: // Altura Inicial
                    _hInicialText.gameObject.SetActive(true);
                    _hInicialText.text = GetDatoInfo(pregunta.Datos[i]);
                    break;
                case 13: // Altura Total
                    _HText.gameObject.SetActive(true);
                    _HText.text = GetDatoInfo(pregunta.Datos[i]);
                    break;
                case 15: // Tiempo de vuelo
                    _TvText.gameObject.SetActive(true);
                    _TvText.text = GetDatoInfo(pregunta.Datos[i]);
                    break;
                default:
                    break;
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

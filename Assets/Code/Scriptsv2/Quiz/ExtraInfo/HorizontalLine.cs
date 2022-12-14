using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HorizontalLine : MonoBehaviour
{
    // [SerializeField] private GameObject _lineInfo;
    [SerializeField] private TMP_Text _VoText;
    [SerializeField] private TMP_Text _VfText;
    [SerializeField] private TMP_Text _aText;
    [SerializeField] private TMP_Text _tText;
    [SerializeField] private TMP_Text _xText;

    [SerializeField] private TMP_Text _vText;
    [SerializeField] private GameObject _infoContainer;

    public void Init(Dato[] datos)
    {
        _VoText.gameObject.SetActive(false);
        _VfText.gameObject.SetActive(false);
        _aText.gameObject.SetActive(false);
        _tText.gameObject.SetActive(false);
        _xText.gameObject.SetActive(false);
        _vText.gameObject.SetActive(false);
        for (int i = 0; i < datos.Length; i++)
        {
            switch (datos[i].VariableId)
            {
                case 4: // Velocidad Inicial
                    _VoText.gameObject.SetActive(true);
                    _VoText.text = GetDatoInfo(datos[i]);
                    break;
                case 5: // Velocidad Final
                    _VfText.gameObject.SetActive(true);
                    _VfText.text = GetDatoInfo(datos[i]);
                    break;
                case 6: // Aceleración
                    _aText.gameObject.SetActive(true);
                    _aText.text = GetDatoInfo(datos[i]);
                    break;
                case 3: // Tiempo
                    _tText.gameObject.SetActive(true);
                    _tText.text = GetDatoInfo(datos[i]);
                    break;
                case 2: // Distancia
                    _xText.gameObject.SetActive(true);
                    _xText.text = GetDatoInfo(datos[i]);
                    break;
                case 1: // Velocidad
                    _vText.gameObject.SetActive(true);
                    _vText.text = GetDatoInfo(datos[i]);
                    break;


                default:
                    break;
            }
        }
        // LayoutRebuilder.ForceRebuildLayoutImmediate(_infoContainer.GetComponent<RectTransform>());
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

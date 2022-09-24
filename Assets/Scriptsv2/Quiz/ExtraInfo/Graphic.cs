using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Graphic : MonoBehaviour
{
    [SerializeField] private MRUGraphic _mruGraphic;
    [SerializeField] private CaidaLibreGraphic _caidaLibreGraphic;

    public void Init(Pregunta pregunta)
    {
        switch (LevelManager.Instance.temaId)
        {
            case 1:
                _mruGraphic.gameObject.SetActive(true);
                _mruGraphic.Init(pregunta);
                break;
            case 2:
                _mruGraphic.gameObject.SetActive(true);
                _mruGraphic.Init(pregunta);
                break;
            case 3:
                _caidaLibreGraphic.gameObject.SetActive(true);
                _caidaLibreGraphic.Init(pregunta);
                break;
            default:
                break;
        }
    }

}

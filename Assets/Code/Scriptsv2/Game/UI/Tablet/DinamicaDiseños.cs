using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinamicaDiseños : MonoBehaviour, ITab
{
    [SerializeField] Transform _buttonContainer;
    [SerializeField] DiseñoPolea[] _diseños;
    [SerializeField] List<Button> _buttons = new();
    [SerializeField] private DinamicData _dinamicData;
    public void Init()
    {
        int index = 0;
        foreach (Button item in _buttons)
        {
            Debug.Log($"Al boton {item.name} asignar {index}");
            item.onClick.AddListener(() =>
            {
                ActivateDiseño(item);
            });
            index++;
        }
        // for (int i = 0; i < _buttons.Length; i++)
        // {
        // }
    }
    private void ActivateDiseño(Button button)
    {
        int index = _buttons.FindIndex(e => e == button);
        Debug.Log("Activar " + index);
        for (int i = 0; i < _diseños.Length; i++)
        {
            if (i == index)
            {
                // _diseños[i].Diseño.SetActive(true);
                _diseños[i].Modelo.SetActive(true);
                _dinamicData.Init(_diseños[i].Objetos);
            }
            else
            {
                // _diseños[i].Diseño.SetActive(false);
                _diseños[i].Modelo.SetActive(false);
            }
        }
    }


}
[Serializable]
public struct DiseñoPolea
{
    public GameObject Diseño;
    public GameObject Modelo;
    public DynamicObject[] Objetos;
}
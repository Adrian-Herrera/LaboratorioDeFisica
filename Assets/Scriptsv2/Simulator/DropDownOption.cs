using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DropDownOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private int _segmentId;
    [SerializeField] private TMP_Text _optionText;
    [SerializeField] private Button addVariable;
    [SerializeField] private Button removeVariable;
    [SerializeField] private Button resolveVariable;
    [SerializeField] private Image _background;

    public void Init(int segmentId, Variable variable)
    {
        _background = GetComponent<Image>();
        _segmentId = segmentId;
        _optionText.text = variable.Nombre;

        addVariable.onClick.AddListener(delegate
        {
            print($"Añadido {variable.Nombre} con id {variable.Id} del segmento {_segmentId}");
            SimulatorManager._selectedObject.addDato(_segmentId, variable.Id);
            removeVariable.gameObject.SetActive(true);
            addVariable.gameObject.SetActive(false);
        });
        removeVariable.onClick.AddListener(delegate
        {
            print($"Eliminado {variable.Nombre} del segmento {_segmentId}");
            SimulatorManager._selectedObject.removeDato(_segmentId, variable.Id);
            addVariable.gameObject.SetActive(true);
            removeVariable.gameObject.SetActive(false);
        });
        resolveVariable.onClick.AddListener(delegate
        {
            print($"Resolver {variable.Nombre} del segmento {_segmentId}");
        });
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _background.color = new Color(1, 1, 1, .5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _background.color = new Color(1, 1, 1, 0);
    }



}

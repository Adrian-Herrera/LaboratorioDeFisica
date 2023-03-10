using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TabController _controller;
    public Image Background;
    private void Awake()
    {
        Background = GetComponent<Image>();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        _controller.OnTabEnter(this);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        _controller.OnTabExit(this);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        _controller.OnTabSelected(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverCall : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string msg;
    [SerializeField] private HoverMessage.Positions _pos;
    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverManager.Current.show(gameObject, msg, _pos);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        HoverManager.Current.hide(gameObject);
    }
}

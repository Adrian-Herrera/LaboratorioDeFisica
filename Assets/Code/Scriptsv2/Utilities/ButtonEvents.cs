using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvents : Button
{
    public delegate void CustomEvent();
    public CustomEvent onDownEvent;
    public CustomEvent onUpEvent;
    // Button is Pressed
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        onDownEvent?.Invoke();
    }

    // Button is released
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        onUpEvent?.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;
    private void Awake()
    {
        current = this;
    }
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    public static void ChangeOptions()
    {
        if (OnClicked != null)
        {
            OnClicked();
        }
    }

    // Fields Events
    public event Action onSelectType;
    public void SelectType()
    {
        if (onSelectType != null)
        {
            onSelectType();
        }
    }
}

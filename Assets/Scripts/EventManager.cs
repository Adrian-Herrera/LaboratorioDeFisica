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
    public delegate void voidDelegate();
    public static event voidDelegate onChangeType;

    public static void ChangeType()
    {
        if (onChangeType != null)
        {
            onChangeType();
        }
    }

    // Fields Events
    public event voidDelegate onChangeProblem;
    public void ChangeProblem()
    {
        if (onChangeProblem != null)
        {
            onChangeProblem();
        }
    }
}

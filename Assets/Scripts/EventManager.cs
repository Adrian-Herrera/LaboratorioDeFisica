using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Current;
    private void Awake()
    {
        Current = this;
    }
    public delegate void voidDelegate();
    public delegate void ChangeOnData(int i, int j, string s);
    public delegate void SelectNewField(int segment);
    public event voidDelegate onChangeType;

    public void ChangeType()
    {
        if (onChangeType != null)
        {
            onChangeType();
        }
    }

    public event voidDelegate onChangeProblem;
    public void ChangeProblem()
    {
        if (onChangeProblem != null)
        {
            onChangeProblem();
        }
    }

    public event ChangeOnData onChangeFieldData;
    public void ChangeFieldData(int i, int j, string s)
    {
        if (onChangeFieldData != null)
        {
            onChangeFieldData(i, j, s);
        }
    }

    public event SelectNewField onSelectField;
    public void SelectField(int segment)
    {
        if (onSelectField != null)
        {
            onSelectField(segment);
        }
    }
}

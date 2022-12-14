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
    public event Action onChangeProblem;
    public void ChangeProblem()
    {
        if (onChangeProblem != null)
        {
            onChangeProblem();
        }
    }
    public event Action<float> onChangeZoom;
    public void ChangeZoom(float cameraScale)
    {
        if (onChangeZoom != null)
        {
            onChangeZoom(cameraScale);
        }
    }
}

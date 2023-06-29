using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class View : MonoBehaviour
{
    protected virtual void Init() { }
    public void Show()
    {
        gameObject.SetActive(true);
        Init();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SwitchView()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}

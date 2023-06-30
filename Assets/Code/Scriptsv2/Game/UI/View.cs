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
        PlayerUI.Instance.isMenuOpen?.Invoke(true);
        Init();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        PlayerUI.Instance.isMenuOpen?.Invoke(false);
    }
    public void SwitchView()
    {
        PlayerUI.Instance.isMenuOpen?.Invoke(!gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }
}

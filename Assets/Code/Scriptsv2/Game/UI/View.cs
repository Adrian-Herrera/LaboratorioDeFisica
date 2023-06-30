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
    }
    public void Show(bool ShowCursor = true)
    {
        gameObject.SetActive(true);
        PlayerUI.Instance.isMenuOpen?.Invoke(ShowCursor);
        Init();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        PlayerUI.Instance.isMenuOpen?.Invoke(false);
        Init();
    }
    public void SwitchView()
    {
        if (gameObject.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }

    }
}

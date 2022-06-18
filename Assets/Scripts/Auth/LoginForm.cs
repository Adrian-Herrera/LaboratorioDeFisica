using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class LoginForm : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private Button submitBtn;

    [SerializeField] GameObject mainMenu, loginMenu;
    IEnumerator Upload()
    {
        bool isLogin = false;
        WWWForm form = new WWWForm();
        form.AddField("LoginUser", username.text);
        form.AddField("LoginPassword", password.text);
        StartCoroutine(CredentialManager.Current.Login(form, (callback) =>
        {
            isLogin = callback;
        }));
        while (isLogin == false)
        {
            yield return null;
        }
        if (isLogin)
        {
            mainMenu.SetActive(true);
            loginMenu.SetActive(false);
        }
        yield return null;

    }
    public void Submit()
    {
        StartCoroutine(Upload());
    }
}

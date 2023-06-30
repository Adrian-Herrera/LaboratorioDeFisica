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

    private void Awake()
    {
        submitBtn.onClick.AddListener(Submit);
    }
    public IEnumerator Login(string username, string password)
    {
        bool Auth = false;
        yield return StartCoroutine(ServerMethods.Current.Login(username, password, (isAuth) =>
        {
            Auth = isAuth;
        }));
        if (MainMenuCanvasSelector.Instance != null) MainMenuCanvasSelector.Instance.GoToCanvas(MainMenuCanvasSelector.SelectCanvas.MainMenu);
        if (WsClient.Instance != null) WsClient.Instance.Init();
    }
    public void Submit()
    {
        StartCoroutine(Login(username.text, password.text));
    }
}
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
    public static IEnumerator Login(string username, string password)
    {
        WWWForm form = new();
        form.AddField("LoginUser", username);
        form.AddField("LoginPassword", password);

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost:4000/ingresar", form);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Helpers.LogNetworkError(www);
            yield break;
        }
        CredentialManager.Current.JwtCredential = JsonUtility.FromJson<JwtCredential>(www.downloadHandler.text);

        using UnityWebRequest www2 = UnityWebRequest.Get("http://localhost:4000/usuario/" + CredentialManager.Current.JwtCredential.id);
        www2.SetRequestHeader("Authorization", "Bearer " + CredentialManager.Current.JwtCredential.token);
        yield return www2.SendWebRequest();
        if (www2.result != UnityWebRequest.Result.Success)
        {
            Helpers.LogNetworkError(www2);
            yield break;
        }
        Debug.Log(www2.downloadHandler.text);
        CredentialManager.Current.UserInfo = JsonUtility.FromJson<UserInfo>(www2.downloadHandler.text);
        CredentialManager.Current.IsAuth = true;
        Debug.Log("Login with " + CredentialManager.Current.JwtCredential.id);
        if (MainMenuCanvasSelector.Instance != null) MainMenuCanvasSelector.Instance.GoToCanvas(MainMenuCanvasSelector.SelectCanvas.MainMenu);
        if (WsClient.Instance != null) WsClient.Instance.Init();
    }
    public void Submit()
    {
        StartCoroutine(Login(username.text, password.text));
    }
}
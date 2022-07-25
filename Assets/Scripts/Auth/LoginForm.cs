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
    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("LoginUser", username.text);
        form.AddField("LoginPassword", password.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:4000/ingresar", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            CredentialManager.Current.JwtCredential = JsonUtility.FromJson<JwtCredential>(www.downloadHandler.text);
            UnityWebRequest www2 = UnityWebRequest.Get("http://localhost:4000/usuario/" + CredentialManager.Current.JwtCredential.userId);
            www2.SetRequestHeader("Authorization", "Bearer " + CredentialManager.Current.JwtCredential.token);
            yield return www2.SendWebRequest();
            if (www2.isNetworkError || www2.isHttpError)
            {
                Debug.Log(www2.error);
                Debug.Log(www2.downloadHandler.text);
            }
            else
            {
                CredentialManager.Current.UserInfo = JsonUtility.FromJson<UserInfo>(www2.downloadHandler.text);
                mainMenu.SetActive(true);
                loginMenu.SetActive(false);
            }
        }
    }
    public void Submit()
    {
        StartCoroutine(Login());
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private Button registerBtn;
    [SerializeField] private Button loginBtn;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button logoutBtn;

    private void OnEnable()
    {
        if (CredentialManager.Current == null)
        {
            return;
        }
        ShowName();
    }
    private void ShowName()
    {
        Debug.Log("ShowName");
        if (CredentialManager.Current.JwtCredential != null)
        {
            nameText.gameObject.SetActive(true);
            nameText.text = "Bienvenido " + CredentialManager.Current.UserInfo.User;
            logoutBtn.gameObject.SetActive(true);
            registerBtn.gameObject.SetActive(false);
            loginBtn.gameObject.SetActive(false);
        }
        else
        {
            nameText.gameObject.SetActive(false);
            logoutBtn.gameObject.SetActive(false);
            registerBtn.gameObject.SetActive(true);
            loginBtn.gameObject.SetActive(true);
        }
    }

    public void Logout()
    {
        Debug.Log("Logout");
        CredentialManager.Current.JwtCredential = null;
        nameText.gameObject.SetActive(false);
        logoutBtn.gameObject.SetActive(false);
        registerBtn.gameObject.SetActive(true);
        loginBtn.gameObject.SetActive(true);
    }
}

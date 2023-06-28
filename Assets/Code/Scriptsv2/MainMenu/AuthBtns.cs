using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AuthBtns : MonoBehaviour
{
    [SerializeField] private GameObject LoginBtn;
    [SerializeField] private GameObject RegisterBtn;
    [SerializeField] private GameObject LogoutBtn;
    [SerializeField] private TMP_Text UserName;
    private void Awake()
    {
        LogoutBtn.GetComponent<Button>().onClick.AddListener(Logout);
    }
    private void OnEnable()
    {
        CheckAuth();
    }
    private void Logout()
    {
        CredentialManager.Current.CleanData();
        CheckAuth();
    }
    private void CheckAuth()
    {
        bool isAuth = CredentialManager.Current ? CredentialManager.Current.isAuth : false;
        LoginBtn.SetActive(!isAuth);
        // RegisterBtn.SetActive(!isAuth);
        LogoutBtn.SetActive(isAuth);
        UserName.gameObject.SetActive(isAuth);
        if (isAuth)
        {
            UserName.text = CredentialManager.Current.UserInfo.NombreCompleto();
        }
    }
}

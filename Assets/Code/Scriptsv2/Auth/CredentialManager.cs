using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class CredentialManager : MonoBehaviour
{
    public static CredentialManager Current;
    public int SendId;
    public JwtCredential JwtCredential;
    public UserInfo UserInfo;
    public bool IsAuth = false;
    private void Awake()
    {
        if (Current == null)
        {
            Current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CleanData()
    {
        JwtCredential = null;
        UserInfo = null;
        IsAuth = false;
    }
}
[Serializable]
public class JwtCredential
{
    public string token;
    public int id;
    public int userId;
}
[Serializable]
public class UserInfo
{
    public string Nombre;
    public string ApellidoPaterno;
    public string ApellidoMaterno;
    public string User;
    public string Correo;
    public string Celular;
    public string NombreCompleto()
    {
        string name = Nombre;
        if (!string.IsNullOrEmpty(ApellidoPaterno))
        {
            name += " " + ApellidoPaterno;
        }
        if (!string.IsNullOrEmpty(ApellidoMaterno))
        {
            name += " " + ApellidoMaterno;
        }
        return name;
    }
}

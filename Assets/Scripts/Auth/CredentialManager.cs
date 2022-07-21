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
}
[Serializable]
public class JwtCredential
{
    public string token;
    public int id;
    public int userId;
}
[Serializable]
public struct UserInfo
{
    public string Nombre;
    public string ApellidoPaterno;
    public string ApellidoMaterno;
    public string User;
    public string Correo;
    public string Celular;
}

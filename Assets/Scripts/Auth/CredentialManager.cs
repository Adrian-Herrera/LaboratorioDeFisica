using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class CredentialManager : MonoBehaviour
{
    public static CredentialManager Current;
    public int SendId;
    private JwtCredential _jwtCredential;
    public JwtCredential JwtCredential
    {
        get { return _jwtCredential; }
        set
        {
            _jwtCredential = value;
            Debug.Log(_jwtCredential.token);
            Debug.Log(_jwtCredential.id);
            Debug.Log(_jwtCredential.completeName);
        }
    }
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

    public IEnumerator Login(WWWForm form, Action<bool> callback)
    {
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:4000/ingresar", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            JwtCredential = JsonUtility.FromJson<JwtCredential>(www.downloadHandler.text);
            callback(true);
        }
    }
}
public struct JwtCredential
{
    public string token;
    public int id;
    public string completeName;
    public bool activo;
}

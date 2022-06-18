using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class ServerMethods : MonoBehaviour
{
    public static ServerMethods Current;
    private void Awake()
    {
        Current = this;
    }
    public IEnumerator getData(int id, Action<Cuestionario> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:4000/cuestionario/" + id);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            string res = www.downloadHandler.text;
            Debug.Log(res);
            Cuestionario Data = JsonUtility.FromJson<Cuestionario>(res);
            callback(Data);
        }
    }
}

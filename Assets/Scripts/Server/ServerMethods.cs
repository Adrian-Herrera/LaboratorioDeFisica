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
    private string baseUrl = "http://localhost:4000";
    private IEnumerator GetJson(string url, Action<string> res)
    {
        Debug.Log("GetRequest");
        UnityWebRequest www = UnityWebRequest.Get(baseUrl + url);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            if (www.downloadHandler.text.StartsWith("["))
            {
                string wp = "{\"Items\":" + www.downloadHandler.text + "}";
                res(wp);

            }
            else
            {
                res(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetCuestionario(int id, Action<Cuestionario> res)
    {
        yield return StartCoroutine(GetJson("/cuestionario/" + id, (json) =>
        {
            res(JsonUtility.FromJson<Cuestionario>(json));
        }));
    }
    public IEnumerator GetCuestionarios(Action<Cuestionario[]> res)
    {
        Debug.Log("GetCuestionarios");
        string url = "/alumno/" + CredentialManager.Current.JwtCredential.id + "/cuestionario?temaId=" + LevelManager.Instance.temaId;
        yield return StartCoroutine(GetJson(url, (json) =>
        {
            res(JsonHelper.FromJson<Cuestionario>(json));
        }));
    }
    public IEnumerator GetUnidades(Action<Unidad[]> res)
    {
        Debug.Log("GetUnidades");
        string url = "/unidad";
        yield return StartCoroutine(GetJson(url, (json) =>
        {
            res(JsonHelper.FromJson<Unidad>(json));
        }));
    }
    public IEnumerator GetVariables(Action<Variable[]> res)
    {
        Debug.Log("GetVariables");
        string url = "/variable";
        yield return StartCoroutine(GetJson(url, (json) =>
        {
            res(JsonHelper.FromJson<Variable>(json));
        }));
    }
    public IEnumerator GetAlumnoHistorial(int CuestionarioID, Action<Historial[]> res)
    {
        Debug.Log("GetAlumnoHistorial");
        string url = "/alumno/" + CredentialManager.Current.JwtCredential.id + "/historial?cuestionarioId=" + CuestionarioID;
        yield return StartCoroutine(GetJson(url, (json) =>
        {
            res(JsonHelper.FromJson<Historial>(json));
        }));
    }
    public IEnumerator CreateHistorial(Historial historial)
    {
        WWWForm form = new WWWForm();
        form.AddField("Puntaje", historial.Puntaje);
        form.AddField("AlumnoId", historial.AlumnoId);
        form.AddField("CuestionarioId", historial.CuestionarioId);
        form.AddField("NumeroIntento", historial.NumeroIntento);

        UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/historial", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Historial creado correctamente");
        }
    }
}

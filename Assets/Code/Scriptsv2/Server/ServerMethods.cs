using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
public class ServerMethods : MonoBehaviour
{
    public static ServerMethods Current;
    private void Awake()
    {
        Current = this;
        // GetLocalIPAddress();
    }
    private const string BASEURL = "http://localhost:4000";
    private IEnumerator GetJson(string url, Action<string> res)
    {
        Debug.Log("GetRequest");
        using UnityWebRequest www = UnityWebRequest.Get(BASEURL + url);
        www.SetRequestHeader("Authorization", "Bearer " + CredentialManager.Current.JwtCredential.token);
        // www.SetRequestHeader("Authorization", "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MiwiaWF0IjoxNjczMDUzMTc2LCJleHAiOjE2NzMxMzk1NzZ9.sUuINdj0SAAyTEMgZmaclZdEpLShMcuHRUshlNY3k04");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
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

    public IEnumerator Login(string username, string password, Action<bool> res)
    {
        WWWForm form = new();
        form.AddField("LoginUser", username);
        form.AddField("LoginPassword", password);

        using UnityWebRequest www = UnityWebRequest.Post(BASEURL + "/ingresar", form);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Helpers.LogNetworkError(www);
            res(false);
            yield break;
        }
        CredentialManager.Current.JwtCredential = JsonUtility.FromJson<JwtCredential>(www.downloadHandler.text);
        CredentialManager.Current.IsAuth = true;

        yield return StartCoroutine(SetActiveUser());
        res(true);
    }

    public IEnumerator SetActiveUser()
    {
        Debug.Log("/usuario/" + CredentialManager.Current.JwtCredential.id);
        yield return StartCoroutine(GetJson("/usuario/" + CredentialManager.Current.JwtCredential.id, (json) =>
        {
            CredentialManager.Current.UserInfo = JsonUtility.FromJson<UserInfo>(json);
        }));
    }

    public IEnumerator GetCuestionario(int id, Action<Cuestionario> res)
    {
        Debug.Log("/cuestionario/" + id);
        yield return StartCoroutine(GetJson("/cuestionario/" + id, (json) =>
        {
            res(JsonUtility.FromJson<Cuestionario>(json));
        }));
    }
    public IEnumerator GetCuestionarios(Action<Cuestionario[]> res)
    {
        Debug.Log("GetCuestionarios");
        string url = "/cuestionarios?colegioId=1&gestion=2023&tipo=1&usuario=" + CredentialManager.Current.JwtCredential.id + "&temaId=" + LevelManager.Instance.temaId;
        Debug.Log(url);
        yield return StartCoroutine(GetJson(url, (json) =>
        {
            res(JsonHelper.FromJson<Cuestionario>(json));
        }));
    }
    public IEnumerator GetRetos(int codigoId, int temaId, Action<Cuestionario[]> res)
    {
        Debug.Log("GetRetos");
        string url = "/cuestionarios?colegioId=1&gestion=2023&tipo=3&usuario=" + CredentialManager.Current.JwtCredential.id + "&temaId=" + temaId;
        yield return StartCoroutine(GetJson(url, (json) =>
        {
            res(JsonHelper.FromJson<Cuestionario>(json));
        }));
    }
    public IEnumerator GetReto(int id, Action<Cuestionario> res)
    {
        yield return StartCoroutine(GetJson("/cuestionario/" + id, (json) =>
        {
            res(JsonUtility.FromJson<Cuestionario>(json));
        }));
    }
    public IEnumerator GetUnidades(Action<Unidad[]> res)
    {
        Debug.Log("GetMagnitudes");
        string url = "/magnitud";
        yield return StartCoroutine(GetJson(url, (json) =>
        {
            res(JsonHelper.FromJson<Unidad>(json));
        }));
    }
    public IEnumerator GetVariables(Action<Variable[]> res)
    {
        Debug.Log("GetVariables");
        string url = "/tipoVariable";
        yield return StartCoroutine(GetJson(url, (json) =>
        {
            res(JsonHelper.FromJson<Variable>(json));
        }));
    }
    public IEnumerator GetAlumnoHistorial(int CuestionarioID, Action<Historial[]> res)
    {
        Debug.Log("GetAlumnoHistorial");
        string url = "/historial?usuarioId=" + CredentialManager.Current.JwtCredential.id + "&cuestionarioId=" + CuestionarioID;
        yield return StartCoroutine(GetJson(url, (json) =>
        {
            res(JsonHelper.FromJson<Historial>(json));
        }));
    }
    public IEnumerator CreateHistorial(Historial historial)
    {
        WWWForm form = new();
        form.AddField("Puntaje", historial.Puntaje);
        form.AddField("UsuarioId", historial.UsuarioId);
        form.AddField("EjercicioId", historial.EjercicioId);
        form.AddField("NumeroIntento", historial.NumeroIntento);
        form.AddField("TiempoEmpleado", historial.TiempoEmpleado);

        using UnityWebRequest www = UnityWebRequest.Post(BASEURL + "/historial", form);
        www.SetRequestHeader("Authorization", "Bearer " + CredentialManager.Current.JwtCredential.token);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Historial creado correctamente");
        }
    }
    public IEnumerator GetAlumnoEjercicios(int TemaId, Action<Ejercicio[]> res)
    {
        Debug.Log($"GetAlumnoEjercicios alumno Id = {CredentialManager.Current.JwtCredential.id} temaId = {TemaId}");
        string url = "/cuestionarios?colegioId=1&gestion=2023&tipo=1&usuario=" + CredentialManager.Current.JwtCredential.id + "&temaId=" + TemaId;
        // string url = "/alumno/" + CredentialManager.Current.JwtCredential.id + "/ejercicios?temaId=" + TemaId;
        yield return StartCoroutine(GetJson(url, (json) =>
        {
            res(JsonHelper.FromJson<Ejercicio>(json));
        }));
    }
}

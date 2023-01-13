using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GlobalInfo : MonoBehaviour
{
    public static GlobalInfo Instance;
    public static Variable[] Variables;
    public Dictionary<string, Variable> VarDict = new();
    public static Unidad[] Unidades;
    private static string fileText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void Start()
    {
        StartCoroutine(Init());
    }
    public IEnumerator Init()
    {
        print("Global info start");
        // string filePath = Application.streamingAssetsPath + "/Variables.json";
        // fileText = File.ReadAllText(Application.streamingAssetsPath + "/Variables.json");
        yield return StartCoroutine(LoadStreamingAsset("Variables.json", res =>
        {
            fileText = res;
        }));
        Variables = JsonUtility.FromJson<ResourceData<Variable>>(fileText).Data;
        for (int i = 0; i < Variables.Length; i++)
        {
            VarDict.Add(Variables[i].Nombre, Variables[i]);
        }
        // VarDict = Dictionary<Variable>(Variables);
        yield return StartCoroutine(LoadStreamingAsset("Unidades.json", res =>
        {
            fileText = res;
        }));
        // fileText = File.ReadAllText(Application.streamingAssetsPath + "/Unidades.json");
        Unidades = JsonUtility.FromJson<ResourceData<Unidad>>(fileText).Data;
    }
    IEnumerator LoadStreamingAsset(string fileName, Action<string> res)
    {
        string filePath = Application.streamingAssetsPath + $"/{fileName}";

        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            using UnityWebRequest www = UnityWebRequest.Get(filePath);
            // WWW www = new WWW(filePath);
            yield return www;
            res(www.downloadHandler.text);
        }
        else
            res(File.ReadAllText(filePath));
    }
    // private IEnumerator GetServerData()
    // {
    //     yield return StartCoroutine(ServerMethods.Current.GetUnidades((res) =>
    //     {
    //         Unidades = res;
    //     }));
    //     yield return StartCoroutine(ServerMethods.Current.GetVariables((res) =>
    //     {
    //         Variables = res;
    //     }));
    // }
}
[Serializable]
public class ResourceData<T>
{
    public string Version;
    public T[] Data;
}

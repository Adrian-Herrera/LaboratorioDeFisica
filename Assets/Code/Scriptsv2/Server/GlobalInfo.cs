using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInfo : MonoBehaviour
{
    public static GlobalInfo Instance;
    public static Variable[] Variables;
    public static List<Variable> VarList;
    public static Unidad[] Unidades;
    private static string fileText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // public void Init()
    // {
    //     StartCoroutine(GetData());
    // }
    public IEnumerator Init()
    {
        print("Global info start");
        // string filePath = Application.streamingAssetsPath + "/Variables.json";
        // fileText = File.ReadAllText(Application.streamingAssetsPath + "/Variables.json");
        yield return StartCoroutine(loadStreamingAsset("Variables.json", res =>
        {
            fileText = res;
        }));
        Variables = JsonUtility.FromJson<ResourceData<Variable>>(fileText).Data;
        VarList = new List<Variable>(Variables);
        yield return StartCoroutine(loadStreamingAsset("Unidades.json", res =>
        {
            fileText = res;
        }));
        // fileText = File.ReadAllText(Application.streamingAssetsPath + "/Unidades.json");
        Unidades = JsonUtility.FromJson<ResourceData<Unidad>>(fileText).Data;
    }
    IEnumerator loadStreamingAsset(string fileName, Action<string> res)
    {
        string filePath = Application.streamingAssetsPath + $"/{fileName}";

        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            WWW www = new WWW(filePath);
            yield return www;
            res(www.text);
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

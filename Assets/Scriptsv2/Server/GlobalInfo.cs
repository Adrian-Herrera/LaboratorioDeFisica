using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInfo : MonoBehaviour
{
    public static Variable[] Variables;
    public static List<Variable> VarList;
    public static Unidad[] Unidades;
    private static string fileText;
    public static void Init()
    {
        print("Global info start");
        fileText = File.ReadAllText(Application.dataPath + "/Resources/Variables.json");
        Variables = JsonUtility.FromJson<ResourceData<Variable>>(fileText).Data;
        VarList = new List<Variable>(Variables);
        fileText = File.ReadAllText(Application.dataPath + "/Resources/Unidades.json");
        Unidades = JsonUtility.FromJson<ResourceData<Unidad>>(fileText).Data;

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

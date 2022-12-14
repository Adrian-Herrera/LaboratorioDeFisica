using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Helpers : MonoBehaviour
{
    public static void LogNetworkError(UnityWebRequest www)
    {
        Debug.Log("Error type: " + www.result + "\nError message: " + www.error + "\nServer message: " + www.downloadHandler.text);
    }
    public static void ClearListContent<T>(List<T> list) where T : MonoBehaviour
    {
        foreach (T item in list)
        {
            Destroy(item.gameObject);
        }
        list.Clear();
    }
    public static Color HexColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color newColor)) return newColor;
        else return Color.black;
    }
}

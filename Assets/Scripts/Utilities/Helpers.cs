using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    public static void ClearListContent<T>(List<T> list) where T : MonoBehaviour
    {
        foreach (T item in list)
        {
            Destroy(item.gameObject);
        }
        list.Clear();
    }
}

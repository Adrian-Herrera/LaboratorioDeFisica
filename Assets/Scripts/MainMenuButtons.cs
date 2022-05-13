using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    public void ChangePanel(string name)
    {
        foreach (var item in items)
        {
            if (item.name != name)
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }
        }
    }
}

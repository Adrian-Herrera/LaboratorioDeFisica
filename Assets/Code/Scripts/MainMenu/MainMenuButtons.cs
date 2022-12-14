using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    public void ChangePanel(string name)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].name == name)
            {
                panels[i].SetActive(true);

            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }
    public void ChangeToExercises(int temaId)
    {
        LevelManager.Instance.temaId = temaId;
        ChangePanel("Exercises");
    }
}


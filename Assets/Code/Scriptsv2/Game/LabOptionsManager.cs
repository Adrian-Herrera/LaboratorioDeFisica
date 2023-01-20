using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabOptionsManager : MonoBehaviour
{
    public static LabOptionsManager Instance;
    public StationMode ModeSelected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
public enum StationMode
{
    Reto, Libre, Prueba
}

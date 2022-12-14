using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class DinamicaUIManager : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        Cursor.lockState = _input.menu ? CursorLockMode.Locked : CursorLockMode.None;
        // Debug.Log(Cursor.lockState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeSizeInfo : MonoBehaviour
{
    [SerializeField] private Object3d _object;
    [SerializeField] private TMP_Text _pesoTxt;
    private void Update()
    {
        _pesoTxt.text = _object.Masa.ToString() + " Kg.";
    }
}

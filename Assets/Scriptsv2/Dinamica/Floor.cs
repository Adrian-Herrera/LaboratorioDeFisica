using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private int _size;
    public int Degree;
    [Range(0.0f, 1.0f)]
    public float RozamientoEstatico = 0;
    [Range(0.0f, 1.0f)]
    public float RozamientoDinamico = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Degree = Mathf.RoundToInt(transform.eulerAngles.z);
    }
}

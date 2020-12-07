using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "soCar", menuName = "ScriptableObject/ Car")]
public class soCar : ScriptableObject
{
    public float speed;
    public float acceleration;
    public float initialPosition;
    public float finalPosition;
    public float timeMov;

    public bool onMove = false;
}
